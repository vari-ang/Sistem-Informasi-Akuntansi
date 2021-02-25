using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace SIA_LIB
{
    public class Pelanggan
    {
        private string id;
        private string nama;
        private string alamat;
        private string tlp;

        #region CONSTRUCTORS
        public Pelanggan()
        {
            this.Id = "";
            this.Nama = "";
            this.Alamat = "";
            this.Tlp = "";
        }
          
        public Pelanggan(string id, string nama, string alamat, string telepon)
        {
            this.Id = id;
            this.Nama = nama;
            this.Alamat = alamat;
            this.Tlp = telepon;
        }
        #endregion

        #region PROPERTIES
        public string Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public string Tlp { get => tlp; set => tlp = value; }
        #endregion

        #region METHODS
        public static string BacaData(string kriteria, string nilaiKriteria, List<Pelanggan> listHasilData)
        {
            string sql = "";

            // JIka tidak ada kriteria yang diisikan
            if (kriteria == "")
            {
                sql = "SELECT * FROM pelanggan";
            }
            else
            {
                sql = "SELECT * FROM pelanggan WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                while (hasilData.Read() == true) // selama masih ada data atau selama masih bisa membaca data
                {
                    // Baca hasil dari MySqlDataReader dan simpan di objek
                    Pelanggan p = new Pelanggan();
                    p.Id = hasilData.GetValue(0).ToString();
                    p.Nama = hasilData.GetValue(1).ToString();
                    p.Alamat = hasilData.GetValue(2).ToString();
                    p.Tlp = hasilData.GetValue(3).ToString();

                    // Simpan ke list
                    listHasilData.Add(p);
                }

                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }
        #endregion
    }
}
