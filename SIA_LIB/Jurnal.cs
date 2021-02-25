using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using MySql.Data.MySqlClient;
using System.IO;

namespace SIA_LIB
{
    public class Jurnal
    {
        private int idJurnal;
        private DateTime tanggal;
        private string nomorBukti;
        private string jenis;
        private Periode periode; // aggregation 
        private Transaksi transaksi; // aggregation
        private List<DetilJurnal> listDetilJurnal; // composition

        #region CONSTRUCTORS
        public Jurnal()
        {
            this.IdJurnal = 0;
            this.Tanggal = DateTime.Now;
            this.NomorBukti = "";
            this.Jenis = "";
            this.ListDetilJurnal = new List<DetilJurnal>();
        }

        public Jurnal(int idJurnal, DateTime tanggal, string nomorBukti, string jenis, Periode periode, Transaksi transaksi)
        {
            this.IdJurnal = idJurnal;
            this.Tanggal = tanggal;
            this.NomorBukti = nomorBukti;
            this.Jenis = jenis;
            this.Periode = periode;
            this.Transaksi = transaksi;
            this.ListDetilJurnal = new List<DetilJurnal>();
        }
        #endregion

        #region PROPERTIES
        public int IdJurnal { get => idJurnal; set => idJurnal = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string NomorBukti { get => nomorBukti; set => nomorBukti = value; }
        public string Jenis { get => jenis; set => jenis = value; }
        public Periode Periode { get => periode; set => periode = value; }
        public Transaksi Transaksi { get => transaksi; set => transaksi = value; }
        public List<DetilJurnal> ListDetilJurnal { get => listDetilJurnal; private set => listDetilJurnal = value; }
        #endregion

        #region METHODS
        // memperoleh id jurnal terakhir
        public static int GetIdJurnalTerbaru()
        {
            string sql = "SELECT MAX(id) FROM _jurnal";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            int idJurnalTerbaru = 1;

            if (hasil.Read() == true) // jika ada data
            {
                if (hasil.GetValue(0).ToString() != "")
                {
                    idJurnalTerbaru = int.Parse(hasil.GetValue(0).ToString()) + 1;
                }
            }

            return idJurnalTerbaru;
        }

        public static string BacaData(List<Jurnal> listHasilData)
        {
            string sql = "SELECT id, tanggal, keterangan, nama, debet, kredit, nomor_bukti, jenis, id_periode, id_transaksi FROM vlaporanjurnal";
         
            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();
                while (hasilData.Read() == true)
                {
                    int idJurnal = int.Parse(hasilData.GetValue(0).ToString());
                    DateTime tanggal = DateTime.Parse(hasilData.GetValue(1).ToString());
                    string ket = hasilData.GetValue(2).ToString();
                    string namaAkun = hasilData.GetValue(3).ToString();
                    int debet = int.Parse(hasilData.GetValue(4).ToString());
                    int kredit = int.Parse(hasilData.GetValue(5).ToString());
                    string noBukti = hasilData.GetValue(6).ToString();
                    string jenis = hasilData.GetValue(7).ToString();
                    string idPeriode = hasilData.GetValue(8).ToString();
                    string idTransaksi = hasilData.GetValue(9).ToString();

                    Periode p = new Periode();
                    p.Id = idPeriode;

                    Transaksi t = new Transaksi(idTransaksi, ket);

                    Akun akun = new Akun();
                    akun.Nama = namaAkun;

                    DetilJurnal dj = new DetilJurnal();
                    dj.Akun = akun;
                    dj.Debet = debet;
                    dj.Kredit = kredit;

                    Jurnal jurnal = new Jurnal(idJurnal, tanggal, noBukti, jenis, p, t);
                    jurnal.ListDetilJurnal.Add(dj);

                    listHasilData.Add(jurnal);
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TambahData(Jurnal pJurnal)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // tuliskan peritnah sql 1: menambahkan data nota ke tabel NotaJual
                string sql1 = "INSERT INTO _jurnal (id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi)" +
                              " VALUES ('" + pJurnal.IdJurnal + "','" + pJurnal.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "','" + pJurnal.NomorBukti + "','" +
                               pJurnal.Jenis + "','" + pJurnal.Periode.Id + "','" + pJurnal.Transaksi.IdTransaksi + "')";

                try
                {
                    // jalankan perintah untuk menambahkan ke tabel NotaJual
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang yg terjual dalam nota (nota jual detail)
                    for (int i = 0; i < pJurnal.ListDetilJurnal.Count; i++)
                    {
                        // perintah sql 2: menambahkan barang2 yg terjual ke tabel NotaJualDetil
                        string sql2 = "INSERT INTO _detil_jurnal (nomor_akun, id_jurnal, no_urut, debet, kredit) VALUES ('" + pJurnal.ListDetilJurnal[i].Akun.Nomor + "','" + pJurnal.IdJurnal + "','" +
                                       pJurnal.ListDetilJurnal[i].NoUrut + "','" + pJurnal.ListDetilJurnal[i].Debet + "','" + pJurnal.ListDetilJurnal[i].Kredit + "')";

                        Koneksi.JalankanPerintahDML(sql2);
                    }

                    // jika semua perintah DML berhasil dijalankan
                    tranScope.Complete();

                    return "1";
                }
                catch (Exception exc)
                {
                    tranScope.Dispose();

                    return exc.Message;
                }
            }
        }
      
        public static string Cetak(string pNamaFile)
        {
            try
            {
                List<Jurnal> listJurnal = new List<Jurnal>();

                // baca data nota tertentu yang akan dicetak
                string hasilBaca = Jurnal.BacaData(listJurnal);

                // simpan dulu isi nota yang akan ditampilkan ke objek file (StreamWrite)
                StreamWriter file = new StreamWriter(pNamaFile);

                // tampilkan info perusahaan
                file.WriteLine("Laporan Per Tanggal : " + DateTime.Now.ToShortDateString());
                file.WriteLine("");
                file.WriteLine("Bagoes Bangets");
                file.WriteLine("Jl. Raya Kalirungkut Surabaya");
                file.WriteLine("Telp. (031) - 12345678");
                file.WriteLine("**".PadRight(50, '*'));
                file.WriteLine("");

                for (int i = 0; i < listJurnal.Count; i++)
                {
                    // tampilkan header nota
                    file.WriteLine("Id : " + listJurnal[i].IdJurnal);
                    file.WriteLine("Tanggal : " + listJurnal[i].Tanggal);
                    file.WriteLine("Keterangan : " + listJurnal[i].Transaksi.Keterangan);
                    file.WriteLine("Nama : " + listJurnal[i].ListDetilJurnal[0].Akun.Nama);
                    file.WriteLine("Debet : Rp. " + listJurnal[i].ListDetilJurnal[0].Debet.ToString("0,###"));
                    file.WriteLine("Kredit : Rp. " + listJurnal[i].ListDetilJurnal[0].Kredit.ToString("0,###"));
                    file.WriteLine("No. Bukti : " + listJurnal[i].NomorBukti);
                    file.WriteLine("Jenis : " + listJurnal[i].Jenis);
                    file.WriteLine("Id Periode : " + listJurnal[i].Periode.Id);
                    file.WriteLine("Id Transaksi : " + listJurnal[i].Transaksi.IdTransaksi);
                    file.WriteLine("=".PadRight(50, '='));
                }
                file.Close();

                // cetak ke printer
                Cetak c = new Cetak(pNamaFile, "Courier New", 9, 10, 10, 10, 10);
                c.CetakKePrinter("tulisan");
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message;
            }
        }

        public void TambahDetilJurnalTrans000001(int pGrandTotal)
        {
            // Membeli bahan baku secara kredit (Destination Point)

            // akun 1 : Sediaan bahan baku bertambah (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "13";
            DetilJurnal de = new DetilJurnal(akun1, 1, pGrandTotal, 0);
            listDetilJurnal.Add(de);

            // akun 2 : Hutang bertambah (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "21";
            de = new DetilJurnal(akun2, 2, 0, pGrandTotal);
            listDetilJurnal.Add(de);
        }

        public void TambahDetilJurnalTrans000004(int pNominal)
        {
            // PPIC menerima bahan baku dari gudang

            // akun 1 : WIP bertambah (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "14";
            DetilJurnal de = new DetilJurnal(akun1, 1, pNominal, 0);
            listDetilJurnal.Add(de);

            // akun 2 : Sediaan bahan baku berkurang (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "13";
            de = new DetilJurnal(akun2, 2, 0, pNominal);
            listDetilJurnal.Add(de);
        }

        public void TambahDetilJurnalTrans000005(int pNominal)
        {
            // Menghitung dan membebankan biaya tenaga kerja langsung terhadap Job Order no 123

            // akun 1 : WIP bertambah (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "14";
            DetilJurnal de = new DetilJurnal(akun1, 1, pNominal, 0);
            listDetilJurnal.Add(de);

            // akun 2 : Hutang Gaji bertambah (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "22";
            de = new DetilJurnal(akun2, 2, 0, pNominal);
            listDetilJurnal.Add(de);
        }

        public void TambahDetilJurnalTrans000006(int pNominal)
        {
            // Membayar biaya / gaji tenaga kerja langsung secara tunai

            // akun 1 : Hutang Gaji berkurang (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "22";
            DetilJurnal de = new DetilJurnal(akun1, 1, pNominal, 0);
            listDetilJurnal.Add(de);

            // akun 2 : Kas berkurang (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "11";
            de = new DetilJurnal(akun2, 2, 0, pNominal);
            listDetilJurnal.Add(de);
        }

        public void TambahDetilJurnalTrans000007(int pNominal)
        {
            // Menyelesaikan job order no 123 dan mengirimkan barang jadi ke gudang

            // akun 1 : Sediaan barang jadi bertambah (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "15";
            DetilJurnal de = new DetilJurnal(akun1, 1, pNominal, 0);
            listDetilJurnal.Add(de);

            // akun 2 : WIP berkurang (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "14";
            de = new DetilJurnal(akun2, 2, 0, pNominal);
            listDetilJurnal.Add(de);
        }

        public void TambahDetilJurnalTrans000008(int pGrandTotal, int pTotalHPP)
        {
            // PENJUALAN TUNAI

            // akun 1 : kas bertambah (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "11";
            DetilJurnal dj = new DetilJurnal(akun1, 1, pGrandTotal, 0);
            ListDetilJurnal.Add(dj);

            // akun 2 : penjualan bertambah (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "41";
            dj = new DetilJurnal(akun2, 2, 0, pGrandTotal);
            ListDetilJurnal.Add(dj);

            // akun 3 : penjualan bertambah (kredit)
            Akun akun3 = new Akun();
            akun3.Nomor = "51";
            dj = new DetilJurnal(akun3, 3, pTotalHPP, 0);
            ListDetilJurnal.Add(dj);

            // akun 2 : penjualan bertambah (kredit)
            Akun akun4 = new Akun();
            akun4.Nomor = "15";
            dj = new DetilJurnal(akun4, 4, 0, pTotalHPP);
            ListDetilJurnal.Add(dj);          
        }

        public void TambahDetilJurnalTrans000009(int pHutang, int pDiskon)
        {
            // MELUNASI HUTANG SECARA TUNAI

            // akun 1 : hutang berkurang (debet)
            Akun akun1 = new Akun();
            akun1.Nomor = "21";
            DetilJurnal dj = new DetilJurnal(akun1, 1, pHutang, 0);
            ListDetilJurnal.Add(dj);

            // akun 2 : kas berkurang (kredit)
            Akun akun2 = new Akun();
            akun2.Nomor = "11";
            dj = new DetilJurnal(akun2, 2, 0, pHutang - pDiskon);
            ListDetilJurnal.Add(dj);

            // akun 3 : sediaan bahan baku berkurang (kredit)
            Akun akun3 = new Akun();
            akun3.Nomor = "13";
            dj = new DetilJurnal(akun3, 3, 0, pDiskon);
            ListDetilJurnal.Add(dj);
        }
        #endregion
    }
}
