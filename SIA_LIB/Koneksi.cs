using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SIA_LIB
{
    public class Koneksi
    {
        private MySqlConnection koneksiDB;
        private string namaServer;
        private string namaDatabase;
        private string username;
        private string password;

        #region CONSTRUCTORS
        public Koneksi()
        {
            this.KoneksiDB = new MySqlConnection();

            // Set connection string sesuai dengan yang ada di App.Config
            this.KoneksiDB.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

            // Panggil method Connect
            string hasilConnect = this.Connect();
        }

        public Koneksi(string server, string database, string user, string pwd)
        {
            this.NamaServer = server;
            this.NamaDatabase = database;
            this.Username = user;
            this.Password = pwd;

            string strCon = "Server=" + this.NamaServer + "; Database=" + this.NamaDatabase + "; Uid=" + "root" + "; Pwd=" + "080798";

            this.KoneksiDB = new MySqlConnection();

            // Set connection string sesuai dengan nama server, database, username, dan password yang dimasukkan user
            this.KoneksiDB.ConnectionString = strCon;

            // Panggil method Connect
            string hasilConnect = this.Connect();

            if (hasilConnect == "sukses")
            {
                // Ubah App config dengan memanggil method UpdateAppConfig
                this.UpdateAppConfig(strCon);
            }
        }
        #endregion

        #region PROPERTIES
        public MySqlConnection KoneksiDB
        {
            get { return koneksiDB; }
            set { koneksiDB = value; }
        }

        public string NamaServer
        {
            get { return namaServer; }
            set { namaServer = value; }
        }

        public string NamaDatabase
        {
            get { return namaDatabase; }
            set { namaDatabase = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        #endregion

        #region METHODS
        public string Connect()
        {
            try
            {
                if (this.KoneksiDB.State == System.Data.ConnectionState.Open)
                {
                    this.KoneksiDB.Close();
                }

                this.KoneksiDB.Open();
                return "sukses"; // Artinya sukses connect
            }
            catch (Exception err)
            {
                // Artinya gagal connect
                return "Koneksi gagal. Pesan kesalahan : " + err.Message;
            }
        }

        public void UpdateAppConfig(string connectionString)
        {
            // Buka konfigurasi App.Config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Set App.Config apda nama tag koneksi dengan string koneksi yg dimasukkan pengguna
            config.ConnectionStrings.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString = connectionString;

            // Simpan App.Config yang telah diperbarui
            config.Save(ConfigurationSaveMode.Modified, true);

            // Reload App.Config dengan pengaturan baru
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        public static void JalankanPerintahDML(string pSql)
        {
            Koneksi k = new Koneksi();
            k.Connect();

            // Buat MySqlCommand
            MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

            // Gunakan ExecuteNonQuery untuk menjalankan perintah INSERT/UPDATE/DELETE
            c.ExecuteNonQuery();
        }

        public static MySqlDataReader JalankanPerintahQuery(string pSql)
        {
            
            Koneksi k = new Koneksi();
            k.Connect();

            // Buat MySqlCommand
            MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);
            
            // Gunakan ExecuteReader untuk menjalankan perintah SELECT
            MySqlDataReader hasil = c.ExecuteReader();

            return hasil;
        }
        public static string GetNamaServer()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

            return con.DataSource;
        }
        public static string GetNamaDatabase()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

            return con.Database;
        }
        #endregion
    }
}
