using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Transactions;

namespace SIA_LIB
{
    public class NotaJual
    {
        private string noNota;
        private DateTime tgl;
        private int hargaTotal;
        private DateTime tglBatasPelunasan;
        private DateTime tglBatasDiskon;
        private int diskonPelunasan;
        private string status;
        private string keterangan;
        private Pelanggan pelanggan; // aggregation
        private List<NotaJualDetil> listNotaJualDetil; // composition

        #region CONSTRUCTORS
        public NotaJual()
        {
            this.NoNota = "";
            this.Tgl = DateTime.Now;
            this.HargaTotal = 0;
            this.TglBatasPelunasan = DateTime.Now;
            this.tglBatasDiskon = DateTime.Now;
            this.DiskonPelunasan = 0;
            this.Status = "";
            this.Keterangan = "";
            this.ListNotaJualDetil = new List<NotaJualDetil>();
        }

        public NotaJual(string noNota, DateTime tgl, int hargaTotal, DateTime tglBatasPelunasan, DateTime tglBatasDiskon, int diskonPelunasan, string status, string keterangan, Pelanggan pelanggan)
        {
            this.NoNota = noNota;
            this.Tgl = tgl;
            this.HargaTotal = hargaTotal;
            this.TglBatasPelunasan = tglBatasPelunasan;
            this.tglBatasDiskon = tglBatasDiskon;
            this.DiskonPelunasan = diskonPelunasan;
            this.Status = status;
            this.Keterangan = keterangan;
            this.Pelanggan = pelanggan;
            this.ListNotaJualDetil = new List<NotaJualDetil>();
        }
        #endregion

        #region PROPERTIES
        public string NoNota { get => noNota; set => noNota = value; }
        public DateTime Tgl { get => tgl; set => tgl = value; }
        public int HargaTotal { get => hargaTotal; set => hargaTotal = value; }
        public DateTime TglBatasPelunasan { get => tglBatasPelunasan; set => tglBatasPelunasan = value; }
        public DateTime TglBatasDiskon { get => tglBatasDiskon; set => tglBatasDiskon = value; }
        public int DiskonPelunasan { get => diskonPelunasan; set => diskonPelunasan = value; }
        public string Status { get => status; set => status = value; }
        public string Keterangan { get => keterangan; set => keterangan = value; }
        public Pelanggan Pelanggan { get => pelanggan; set => pelanggan = value; }
        public List<NotaJualDetil> ListNotaJualDetil { get => listNotaJualDetil; set => listNotaJualDetil = value; }
        #endregion

        #region METHODS
        public static string BacaData(string kriteria, string nilaiKriteria, List<NotaJual> listHasilData)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "SELECT nomor, tanggal, harga_total, diskon_pelunasan, keterangan" +
                      " FROM nota_jual";
            }
            else
            {
                sql = "SELECT nomor, tanggal, harga_total, diskon_pelunasan, keterangan" +
                      " FROM nota_jual" +
                      " WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();
                while (hasilData.Read() == true)
                {
                    NotaJual nj = new NotaJual();
                    nj.NoNota = hasilData.GetValue(0).ToString();
                    nj.Tgl = DateTime.Parse(hasilData.GetValue(1).ToString());
                    nj.HargaTotal = int.Parse(hasilData.GetValue(2).ToString());
                    nj.DiskonPelunasan = int.Parse(hasilData.GetValue(3).ToString());
                    nj.keterangan = hasilData.GetValue(4).ToString();

                    listHasilData.Add(nj);
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public void TambahDetilBarang(Barang pBarang, int pJumlah, int pHarga)
        {
            NotaJualDetil njd = new NotaJualDetil(pBarang, pJumlah, pHarga);
            this.ListNotaJualDetil.Add(njd);
        }

        public static string TambahData(NotaJual pNotaJual)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // tuliskan peritnah sql 1: menambahkan data nota ke tabel NotaJual
                string sql1 = "INSERT INTO nota_jual VALUES ('" + pNotaJual.NoNota + "','" + pNotaJual.Tgl.ToString("yyyy-MM-dd hh:mm:ss") + "','" + pNotaJual.HargaTotal + "','" +
                               pNotaJual.TglBatasPelunasan.ToString("yyyy-MM-dd hh:mm:ss") + "','" + pNotaJual.TglBatasDiskon.ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                               pNotaJual.DiskonPelunasan + "','" + pNotaJual.Status + "','" + pNotaJual.Keterangan + "','" + pNotaJual.Pelanggan.Id + "')";

                try
                {
                    // jalankan perintah untuk menambahkan ke tabel NotaJual
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang yg terjual dalam nota (nota jual detail)
                    for (int i = 0; i < pNotaJual.ListNotaJualDetil.Count; i++)
                    {
                        // perintah sql 2: menambahkan barang2 yg terjual ke tabel NotaJualDetil
                        string sql2 = "INSERT INTO detil_nota_jual VALUES ('" + pNotaJual.NoNota + "','" + pNotaJual.ListNotaJualDetil[i].Barang.Id + "','" + 
                                       pNotaJual.ListNotaJualDetil[i].Jumlah + "','" + pNotaJual.ListNotaJualDetil[i].Harga + "')";

                        Koneksi.JalankanPerintahDML(sql2);

                        string hasilUpdateBrg = Barang.UbahStokTerjual(pNotaJual.ListNotaJualDetil[i].Barang.Id, pNotaJual.ListNotaJualDetil[i].Jumlah);
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
        #endregion
    }
}
