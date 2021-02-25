using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Transactions;

namespace SIA_LIB
{
    public class SuratJalan
    {
        // Data Member
        private string nomor;
        private DateTime tanggal;
        private string jenis;
        private string keterangan;
        private SuratPermintaan suratPermintaan; // aggregation
        private List<SuratJalanDetil> listSuratJalanDetil; // composition

        #region CONSTRUCTORS
        public SuratJalan()
        {
            this.Nomor = "";
            this.Tanggal = DateTime.Now;
            this.Jenis = "";
            this.Keterangan = "";
            this.ListSuratJalanDetil = new List<SuratJalanDetil>();
        }

        public SuratJalan(string nomor, DateTime tanggal, string jenis, string keterangan, SuratPermintaan pSuratPermintaan)
        {
            this.Nomor = nomor;
            this.Tanggal = tanggal;
            this.Jenis = jenis;
            this.Keterangan = keterangan;
            this.SuratPermintaan = pSuratPermintaan;
            this.ListSuratJalanDetil = new List<SuratJalanDetil>();
        }
        #endregion

        #region PROPERTIES
        public string Nomor { get => nomor; set => nomor = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string Jenis { get => jenis; set => jenis = value; }
        public string Keterangan { get => keterangan; set => keterangan = value; }
        public SuratPermintaan SuratPermintaan { get => suratPermintaan; set => suratPermintaan = value; }
        public List<SuratJalanDetil> ListSuratJalanDetil { get => listSuratJalanDetil; set => listSuratJalanDetil = value; }
        #endregion

        #region METHODS
        public static string GenerateNoBaru(out string pNoBaru)
        {
            // sql untuk mendapatkan nomor urut transaksi terakhir di tanggal hari ini (tanggal komputer)
            string sql = "SELECT SUBSTRING(nomor, 3, 3) AS noUrut " +
                         "FROM surat_jalan " +
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

                pNoBaru = "SJ" + noUrutTerbaru.ToString();

                return "1";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

        public static string BacaData(string pKriteria, string pNilaiKriteria, List<SuratJalan> listHasilData)
        {
            string sql1 = "";
            if (pKriteria == "")
            {
                sql1 = "SELECT nomor, nomor_surat_permintaan, tanggal, jenis, keterangan FROM surat_jalan";
            }
            else
            {
                sql1 = "SELECT nomor, nomor_surat_permintaan, tanggal, jenis, keterangan FROM surat_jalan" +
                       " WHERE " + pKriteria + " LIKE '%" + pNilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql1);
                listHasilData.Clear(); //kosongi isi list lebih dulu

                while (hasilData.Read() == true)  //selama masih ada data atau selama masih bisa membaca data
                {
                    SuratPermintaan sp = new SuratPermintaan();
                    sp.Nomor = hasilData.GetValue(1).ToString();

                    SuratJalan sj = new SuratJalan();
                    sj.Nomor = hasilData.GetValue(0).ToString();
                    sj.Tanggal = DateTime.Parse(hasilData.GetValue(2).ToString());
                    sj.Jenis = hasilData.GetValue(3).ToString();
                    sj.Keterangan = hasilData.GetValue(4).ToString();
                    sj.SuratPermintaan = sp;

                    string sql2 = "SELECT dsj.id_barang, b.nama, b.jenis, dsj.jumlah, dsj.hpp " +
                                  "FROM surat_jalan sj INNER JOIN detil_surat_jalan dsj ON sj.nomor = dsj.nomor_surat_jalan " +
                                  "INNER JOIN barang b ON dsj.id_barang = b.id " +
                                  "WHERE sj.nomor  = '" + sj.Nomor + "'";

                    MySqlDataReader hasilData2 = Koneksi.JalankanPerintahQuery(sql2);

                    while (hasilData2.Read() == true)
                    {
                        Barang brg = new Barang();
                        brg.Id = hasilData2.GetValue(0).ToString();
                        brg.Nama = hasilData2.GetValue(1).ToString();
                        brg.Jenis = hasilData2.GetValue(2).ToString();

                        int jumlah = int.Parse(hasilData2.GetValue(3).ToString());
                        int hpp = int.Parse(hasilData2.GetValue(4).ToString());

                        SuratJalanDetil sjd = new SuratJalanDetil(brg, jumlah, hpp);

                        // simpan detil barang ke sp
                        sj.TambahDetilBarang(brg, jumlah, hpp);
                    }

                    listHasilData.Add(sj);
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message;
            }
        }

        public void TambahDetilBarang(Barang pBarang, int pJumlah, int pHPP)
        {
            SuratJalanDetil sjd = new SuratJalanDetil(pBarang, pJumlah, pHPP);
            this.ListSuratJalanDetil.Add(sjd);
        }

        public static string TambahData(SuratJalan pSuratJalan)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // tuliskan peritnah sql 1: menambahkan data nota ke tabel SuratJalan
                string sql1 = "INSERT INTO surat_jalan(nomor, tanggal, jenis, keterangan, nomor_surat_permintaan) " +
                              "VALUES ('" + pSuratJalan.Nomor + "','" + pSuratJalan.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                              pSuratJalan.Jenis + "','" + pSuratJalan.Keterangan + "','" + pSuratJalan.SuratPermintaan.Nomor + "')";

                try
                {
                    // jalankan perintah untuk menambahkan ke tabel SuratJalan
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang dalam surat jalan detail
                    for (int i = 0; i < pSuratJalan.ListSuratJalanDetil.Count; i++)
                    {
                        // perintah sql 2: menambahkan barang2 yg terjual ke tabel SuratJalanDetil
                        string sql2 = "INSERT INTO detil_surat_jalan(nomor_surat_jalan, id_barang, jumlah, hpp) " +
                                      "VALUES ('" + pSuratJalan.Nomor + "','" + pSuratJalan.ListSuratJalanDetil[i].Barang.Id + "','" +
                                       pSuratJalan.ListSuratJalanDetil[i].Jumlah + "','"+ pSuratJalan.ListSuratJalanDetil[i].HPP + "')";

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
