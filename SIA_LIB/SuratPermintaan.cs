using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Transactions;

namespace SIA_LIB
{
    public class SuratPermintaan
    {
        private string nomor;
        private DateTime tanggal;
        private string keterangan;
        private JobOrder jobOrder; // aggregation
        private List<SuratPermintaanDetil> listSuratPermintaanDetil; // composition

        #region CONSTRUCTORS
        public SuratPermintaan()
        {
            Nomor = "";
            Tanggal = DateTime.Now;
            Keterangan = "";
            ListSuratPermintaanDetil = new List<SuratPermintaanDetil>();
        }

        public SuratPermintaan(string pNomor, DateTime pTanggal, string pKeterangan, JobOrder pJobOrder)
        {
            Nomor = pNomor;
            Tanggal = pTanggal;
            Keterangan = pKeterangan;
            JobOrder = pJobOrder;
            ListSuratPermintaanDetil = new List<SuratPermintaanDetil>();
        }

        #endregion

        #region PROPERTIES
        public string Nomor { get => nomor; set => nomor = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string Keterangan { get => keterangan; set => keterangan = value; }
        public JobOrder JobOrder { get => jobOrder; set => jobOrder = value; }
        public List<SuratPermintaanDetil> ListSuratPermintaanDetil { get => listSuratPermintaanDetil; set => listSuratPermintaanDetil = value; }
        #endregion

        #region METHODS
        public static string GenerateNoBaru(out string pNoBaru)
        {
            // sql untuk mendapatkan nomor urut transaksi terakhir di tanggal hari ini (tanggal komputer)
            string sql = "SELECT SUBSTRING(nomor, 3, 3) AS noUrut " +
                         "FROM surat_permintaan " +
                         "ORDER BY noUrut DESC LIMIT 1";

            pNoBaru = "";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                string noUrutTerbaru = "";

                if (hasilData.Read() == true)
                {
                    int noUrutSekarang = int.Parse(hasilData.GetValue(0).ToString()) + 1;
                    noUrutTerbaru = noUrutSekarang.ToString().PadLeft(3, '0'); // jika noUrutTrans < 3
                }
                else
                {
                    noUrutTerbaru = "001";
                }

                pNoBaru = "SP" + noUrutTerbaru.ToString();

                return "1";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

        public static string BacaData(string pKriteria, string pNilaiKriteria, List<SuratPermintaan> listHasilData)
        {
            string sql1 = "";
            if(pKriteria == "")
            {
                sql1 = "SELECT nomor, nomor_job_order, tanggal, keterangan FROM surat_permintaan";
            }
            else {
                sql1 = "SELECT nomor, nomor_job_order, tanggal, keterangan FROM surat_permintaan" +
                       " WHERE " + pKriteria + " LIKE '%" + pNilaiKriteria + "%'";
            }
            
            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql1);
                listHasilData.Clear(); //kosongi isi list lebih dulu

                while (hasilData.Read() == true)  //selama masih ada data atau selama masih bisa membaca data
                {
                    JobOrder jo = new JobOrder();
                    jo.Nomor = hasilData.GetValue(1).ToString();

                    SuratPermintaan s = new SuratPermintaan();
                    s.Nomor = hasilData.GetValue(0).ToString();
                    s.JobOrder = jo;
                    s.Tanggal = DateTime.Parse(hasilData.GetValue(2).ToString());
                    s.Keterangan = hasilData.GetValue(3).ToString();

                    string sql2 = "SELECT dsp.id_barang, b.nama, b.jenis, dsp.jumlah " +
                                  "FROM surat_permintaan sp INNER JOIN detil_surat_permintaan dsp ON sp.nomor = dsp.nomor_surat_permintaan " +
                                  "INNER JOIN barang b ON dsp.id_barang = b.id " +
                                  "WHERE sp.nomor  = '" + s.Nomor + "'";

                    MySqlDataReader hasilData2 = Koneksi.JalankanPerintahQuery(sql2);

                    while (hasilData2.Read() == true)
                    {       
                        Barang brg = new Barang();
                        brg.Id = hasilData2.GetValue(0).ToString();
                        brg.Nama = hasilData2.GetValue(1).ToString();
                        brg.Jenis = hasilData2.GetValue(2).ToString();

                        int jumlah = int.Parse(hasilData2.GetValue(3).ToString());

                        SuratPermintaanDetil spd = new SuratPermintaanDetil(brg, jumlah);

                        // simpan detil barang ke sp
                        s.TambahDetilBarang(brg, jumlah);
                    }

                    listHasilData.Add(s);
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message;
            }
        }

        public void TambahDetilBarang(Barang pBarang, int pJumlah)
        {
            SuratPermintaanDetil spd = new SuratPermintaanDetil(pBarang, pJumlah);
            this.ListSuratPermintaanDetil.Add(spd);
        }

        public static string TambahData(SuratPermintaan pSuratPermintaan)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // tuliskan peritnah sql 1: menambahkan data nota ke tabel SuratPermintaan
                string sql1 = "INSERT INTO surat_permintaan(nomor, tanggal, keterangan, nomor_job_order) " +
                              "VALUES ('" + pSuratPermintaan.Nomor + "','" + pSuratPermintaan.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "','" + 
                              pSuratPermintaan.Keterangan + "','" + pSuratPermintaan.JobOrder.Nomor + "')";

                try
                {
                    // jalankan perintah untuk menambahkan ke tabel SuratPermintaan
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang yg terjual dalam nota (surat permintaan detail)
                    for (int i = 0; i < pSuratPermintaan.ListSuratPermintaanDetil.Count; i++)
                    {
                        // perintah sql 2: menambahkan barang2 yg terjual ke tabel NotaJualDetil
                        string sql2 = "INSERT INTO detil_surat_permintaan(nomor_surat_permintaan, id_barang, jumlah) " +
                                      "VALUES ('" + pSuratPermintaan.Nomor + "','" + pSuratPermintaan.ListSuratPermintaanDetil[i].Barang.Id + "','" +
                                       pSuratPermintaan.ListSuratPermintaanDetil[i].Jumlah + "')";

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
        #endregion
    }
}
