using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace SIA_LIB
{
    public class Barang
    {
        private string id;
        private string nama;
        private string jenis;       
        private int stok;
        private int hargaBeli;
        private int hargaJual;
        private string satuan;

        #region CONTRUCTORS
        public Barang()
        {
            this.Id = "";
            this.Nama = "";
            this.Jenis = "";
            this.Stok = 0;
            this.HargaBeli = 0;
            this.HargaJual = 0;
            this.Satuan = "";
        }

        public Barang(string id, string nama, string jenis, int stok, int hargaBeli, int hargaJual, string satuan)
        {
            this.Id = id;
            this.Nama = nama;
            this.Jenis = jenis;
            this.Stok = stok;
            this.HargaBeli = hargaBeli;
            this.HargaJual = hargaJual;
            this.Satuan = satuan;
        }
        #endregion

        #region PROPERTIES
        public string Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Jenis { get => jenis; set => jenis = value; }
        public int Stok { get => stok; set => stok = value; }
        public int HargaBeli { get => hargaBeli; set => hargaBeli = value; }
        public int HargaJual { get => hargaJual; set => hargaJual = value; }
        public string Satuan { get => satuan; set => satuan = value; }
        #endregion

        #region METHODS
        public static string BacaData(string kriteria, string nilaiKriteria, List<Barang> listHasilData)
        {
            string sql = "";

            // JIka tidak ada kriteria yang diisikan
            if (kriteria == "")
            {
                sql = "SELECT * FROM barang";
            }
            else
            {
                sql = "SELECT * FROM barang WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                while (hasilData.Read() == true) // selama masih ada data atau selama masih bisa membaca data
                {
                    // Baca hasil dari MySqlDataReader dan simpan di objek
                    Barang b = new Barang();
                    b.Id = hasilData.GetValue(0).ToString();
                    b.Nama = hasilData.GetValue(1).ToString();
                    b.Jenis = hasilData.GetValue(2).ToString();
                    b.Stok = int.Parse(hasilData.GetValue(3).ToString());
                    b.HargaBeli = int.Parse(hasilData.GetValue(4).ToString());
                    b.HargaJual = int.Parse(hasilData.GetValue(5).ToString());
                    b.Satuan = hasilData.GetValue(6).ToString();

                    // Simpan ke list
                    listHasilData.Add(b);
                }

                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }          
        }

        public static string UbahStokTerjual(string pKodeBarang, int pJumlahTerjual)
        {
            string sql = "UPDATE barang SET stok = stok - " + pJumlahTerjual +
                         " WHERE id = '" + pKodeBarang + "'";

            try
            {
                Koneksi.JalankanPerintahDML(sql);

                return "1";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

        public static string UbahStokTerbeli(string pKodeBarang, int pJumlahTerbeli)
        {
            string sql = "UPDATE barang SET stok = stok + " + pJumlahTerbeli +
                         " WHERE id = '" + pKodeBarang + "'";

            try
            {
                Koneksi.JalankanPerintahDML(sql);

                return "1";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }

        // Metode Average
        public static string UbahHargaSatuan(string pKodeBarang, int pJumlah, int pHarga)
        {
            string sql = "UPDATE barang SET harga_beli = ((SELECT totalHrg FROM (" +
                         "SELECT((stok - " + pJumlah + ") * harga_beli) AS totalHrg FROM barang WHERE id = '" + pKodeBarang + "') " +
                         "AS a) + " + (pJumlah * pHarga) + ") / (SELECT stok FROM (" +
                         "SELECT stok FROM barang WHERE id = '" + pKodeBarang + "') AS b) WHERE id = '" + pKodeBarang + "'";

            try
            {
                Koneksi.JalankanPerintahDML(sql);

                return "1";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }
        #endregion
    }
}
