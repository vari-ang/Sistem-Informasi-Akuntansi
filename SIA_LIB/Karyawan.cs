using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Transactions;

namespace SIA_LIB
{
    public class Karyawan
    {
        // Data Member
        private string id;
        private string nama;
        private string jenisKelamin;
        private int gaji;

        // karyawan has job order
        private int jumlah; private string satuan; private int upah;
        private List<Karyawan> listKaryawan;

        #region CONSTRUCTORS
        public Karyawan()
        {
            this.Id = "";
            this.Nama = "";
            this.JenisKelamin = "";
            this.Gaji = 0;

            this.Jumlah = 0; this.Satuan = ""; this.Upah = 0;
            this.ListKaryawan = new List<Karyawan>();
        }

        public Karyawan(string id, string nama, string jenis_kelamin, int gaji)
        {
            this.Id = id;
            this.Nama = nama;
            this.JenisKelamin = jenis_kelamin;
            this.Gaji = gaji;
            this.ListKaryawan = new List<Karyawan>();
        }
        #endregion

        #region PROPERTIES
        public string Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string JenisKelamin { get => jenisKelamin; set => jenisKelamin = value; }
        public int Gaji { get => gaji; set => gaji = value; }

        public int Jumlah { get => jumlah; set => jumlah = value; }
        public string Satuan { get => satuan; set => satuan = value; }
        public int Upah { get => upah; set => upah = value; }
        public List<Karyawan> ListKaryawan { get => listKaryawan; set => listKaryawan = value; }
        #endregion

        #region METHODS
        public static string BacaData(string kriteria, string nilaiKriteria, List<Karyawan> listHasilData)
        {
            string sql = "";

            // JIka tidak ada kriteria yang diisikan
            if (kriteria == "")
            {
                sql = "SELECT id, nama, jenis_kelamin, gaji FROM karyawan";
            }
            else
            {
                sql = "SELECT id, nama, jenis_kelamin, gaji FROM karyawan WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                while (hasilData.Read() == true) // selama masih ada data atau selama masih bisa membaca data
                {
                    // Baca hasil dari MySqlDataReader dan simpan di objek
                    Karyawan k = new Karyawan();
                    k.Id = hasilData.GetValue(0).ToString();
                    k.Nama = hasilData.GetValue(1).ToString();
                    k.JenisKelamin = hasilData.GetValue(2).ToString();
                    k.Gaji = int.Parse(hasilData.GetValue(3).ToString());

                    // Simpan ke list
                    listHasilData.Add(k);
                }

                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TambahDataKaryawanHasJobOrder(Karyawan pKaryawan, string pNoJobOrder)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    for (int i = 0; i < pKaryawan.ListKaryawan.Count; i++)
                    {
                        string sql = "INSERT INTO karyawan_has_job_order(id_karyawan, nomor_job_order, jumlah, satuan, upah) " +
                            "         VALUES ('" + pKaryawan.ListKaryawan[i].Id + "','" + pNoJobOrder + "','" + pKaryawan.ListKaryawan[i].Jumlah + "','" + 
                                      pKaryawan.ListKaryawan[i].Satuan + "','" + pKaryawan.ListKaryawan[i].Upah + "')";

                        Koneksi.JalankanPerintahDML(sql);
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
