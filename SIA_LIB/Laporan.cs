using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.IO;

namespace SIA_LIB
{
    public class Laporan
    {
        // Data Member
        private Akun akun;
        private int saldoakhir;

        #region CONSTRUCTORS
        public Laporan()
        {
            this.Saldoakhir = 0;
        }

        public Laporan(Akun pakun,int psaldoakhir)
        {
            this.Akun = pakun;
            this.Saldoakhir = psaldoakhir;
        }
        #endregion

        #region PROPERTIES      
        public Akun Akun { get => akun; set => akun = value; }
        public int Saldoakhir { get => saldoakhir; set => saldoakhir = value; }
        #endregion

        #region METHODS
        public static string TampilPendapatan()
        {
            string sql = "";     
            sql = "SELECT SUM(TotalTransaksi) AS TotalPendapatan " +
                  "FROM vSaldoAkhir " +
                  "WHERE kelompok = 'PENDAPATAN'; ";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilBiaya()
        {
            string sql = "";
            sql = "SELECT SUM(TotalTransaksi) AS TotalBiaya " +
                  "FROM vSaldoAkhir " +
                  "WHERE kelompok = 'BIAYA'; ";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilTotalEkuitasSebelum()
        {
            string sql = "";
            sql = "SELECT SUM(TotalTransaksi) FROM VLaporanPerubahanEkuitas";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilPerubahanEkuitas(string kriteria, string nilaiKriteria, List<Laporan> listHasilData)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "SELECT * FROM vLaporanPerubahanEkuitas";
            }
            else
            {
                sql = "SELECT * FROM vLaporanPerubahanEkuitas WHERE" + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                while (hasilData.Read() == true) // selama masih ada data atau selama masih bisa membaca data
                {
                    // Baca hasil dari MySqlDataReader dan simpan di objek
                    Laporan sup = new Laporan();
                    sup.Saldoakhir = int.Parse(hasilData.GetValue(3).ToString());

                    Akun a = new Akun();
                    a.Nomor = hasilData.GetValue(0).ToString();
                    a.Nama = hasilData.GetValue(1).ToString();
                    a.Kelompok = hasilData.GetValue(2).ToString();

                    sup.Akun = a;

                    // Simpan ke list
                    listHasilData.Add(sup);
                }

                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilLabaRugi(List<Laporan> listHasilData)
        {
            string sql = "";
            sql = "SELECT * FROM vSaldoAkhir " +
                  "WHERE kelompok IN('PENDAPATAN', 'BIAYA')";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();
                
                while (hasilData.Read() == true) // selama masih ada data atau selama masih bisa membaca data
                {
                    // Baca hasil dari MySqlDataReader dan simpan di objek
                    Laporan sup = new Laporan();
                    sup.Saldoakhir = int.Parse(hasilData.GetValue(3).ToString());

                    Akun a = new Akun();
                    a.Nomor = hasilData.GetValue(0).ToString();
                    a.Nama = hasilData.GetValue(1).ToString();
                    a.Kelompok = hasilData.GetValue(2).ToString();

                    sup.Akun = a;

                    // Simpan ke list
                    listHasilData.Add(sup);
                }

                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilAktiva()
        {
            string sql = "";
            sql = "SELECT SUM(TotalTransaksi) AS TotalAkitva " +
                  "FROM vLaporanNeraca " +
                  "WHERE kelompok = 'ASET'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilAktiva(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            sql = "SELECT SUM(TotalTransaksi) AS TotalAkitva " +
                  "FROM vLaporanNeraca " +
                  "WHERE kelompok = 'ASET' AND id_periode = '" + nilaiKriteria + "'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilPasiva(int pLabaRugi)
        {
            string sql = "";
            sql = "SELECT SUM(TotalTransaksi) + " + pLabaRugi + " AS TotalPasiva " +
                  "FROM vLaporanNeraca " +
                  "WHERE kelompok IN ('KEWAJIBAN', 'EKUITAS')";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilPasiva(int pLabaRugi, string kriteria, string nilaiKriteria)
        {
            string sql = "";
            sql = "SELECT SUM(TotalTransaksi) + " + pLabaRugi + " AS TotalPasiva " +
                  "FROM vLaporanNeraca " +
                  "WHERE kelompok IN ('KEWAJIBAN', 'EKUITAS') AND id_periode = '" + nilaiKriteria + "'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    return hasilData.GetValue(0).ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilNeraca(string kriteria, string nilaiKriteria, List<Laporan> listHasilData)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "SELECT * FROM vLaporanNeraca";
            }
            else
            {
                sql = "SELECT * FROM vLaporanNeraca WHERE " + kriteria + " = '" + nilaiKriteria + "'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                while (hasilData.Read() == true) // selama masih ada data atau selama masih bisa membaca data
                {
                    // Baca hasil dari MySqlDataReader dan simpan di objek
                    Laporan sup = new Laporan();
                    sup.Saldoakhir = int.Parse(hasilData.GetValue(3).ToString());

                    Akun a = new Akun();
                    a.Nomor = hasilData.GetValue(0).ToString();
                    a.Nama = hasilData.GetValue(1).ToString();
                    a.Kelompok = hasilData.GetValue(2).ToString();

                    sup.Akun = a;

                    // Simpan ke list
                    listHasilData.Add(sup);
                }

                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string CetakEkuitas(string pNamaFile, int pLabaRugi)
        {
            try
            {
                List<Laporan> listLaporan = new List<Laporan>();

                // baca data nota tertentu yang akan dicetak
                string hasilBaca = Laporan.TampilPerubahanEkuitas("", "", listLaporan);

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

                for (int i = 0; i < listLaporan.Count; i++)
                {
                    // tampilkan header nota
                    file.WriteLine("No. Akun : " + listLaporan[i].Akun.Nomor);
                    file.WriteLine("Nama Akun : " + listLaporan[i].Akun.Nama);
                    file.WriteLine("Kelompok : " + listLaporan[i].Akun.Kelompok);
                    file.WriteLine("Saldo Akhir : Rp. " + listLaporan[i].Saldoakhir.ToString("0,###"));                   
                    file.WriteLine("=".PadRight(50, '='));
                }

                string bacaEkuitas = Laporan.TampilTotalEkuitasSebelum();
                int ekuitasSebelum = 0;
                if (bacaEkuitas != "0")
                {
                    file.WriteLine("");
                    ekuitasSebelum = int.Parse(bacaEkuitas);
                    file.WriteLine("Total Ekuitas Sebelum Laga Rugi : Rp. " + ekuitasSebelum.ToString("0,###"));
                    file.WriteLine("");
                }

                file.WriteLine("Laga/ Rugi : " + pLabaRugi.ToString("0,###"));
                file.WriteLine("");

                int ekuitasSesudah = ekuitasSebelum + pLabaRugi;
                file.WriteLine("Total Ekuitas Setelah Laba Rugi : Rp. " + ekuitasSesudah.ToString("0,###"));
                file.WriteLine("");

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

        public static string CetakLabaRugi(string pNamaFile, int pPendapatan, int pBiaya, int pLabaRugi)
        {
            try
            {
                List<Laporan> listLaporan = new List<Laporan>();

                // baca data nota tertentu yang akan dicetak
                string hasilBaca = Laporan.TampilLabaRugi(listLaporan);

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

                for (int i = 0; i < listLaporan.Count; i++)
                {
                    // tampilkan header nota
                    file.WriteLine("No. Akun : " + listLaporan[i].Akun.Nomor);
                    file.WriteLine("Nama Akun : " + listLaporan[i].Akun.Nama);
                    file.WriteLine("Kelompok : " + listLaporan[i].Akun.Kelompok);
                    file.WriteLine("Saldo Akhir : Rp. " + listLaporan[i].Saldoakhir.ToString("0,###"));
                    file.WriteLine("=".PadRight(50, '='));
                }

                file.WriteLine("");
                file.WriteLine("Pendapatan : Rp. " + pPendapatan.ToString("0,###"));
                file.WriteLine("");

                file.WriteLine("Biaya : Rp. " + pBiaya.ToString("0,###"));
                file.WriteLine("");

                file.WriteLine("Laga/ Rugi : Rp. " + pLabaRugi.ToString("0,###"));
                file.WriteLine("");

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

        public static string CetakNeraca(string pNamaFile, int pLabaRugi)
        {
            try
            {
                List<Laporan> listLaporan = new List<Laporan>();

                // baca data nota tertentu yang akan dicetak
                string hasilBaca = Laporan.TampilNeraca("", "", listLaporan);

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

                for (int i = 0; i < listLaporan.Count; i++)
                {
                    // tampilkan header nota
                    file.WriteLine("No. Akun : " + listLaporan[i].Akun.Nomor);
                    file.WriteLine("Nama Akun : " + listLaporan[i].Akun.Nama);
                    file.WriteLine("Kelompok : " + listLaporan[i].Akun.Kelompok);
                    file.WriteLine("Total : Rp. " + listLaporan[i].Saldoakhir.ToString("0,###"));
                    file.WriteLine("=".PadRight(50, '='));
                }

                int aktiva = 0, pasiva = 0;

                string bacaAktiva = Laporan.TampilAktiva();
                if (bacaAktiva != "0")
                {
                    aktiva = int.Parse(bacaAktiva);

                    file.WriteLine("");
                    file.WriteLine("Total Aktiva: Rp." + aktiva.ToString("0,###"));
                    file.WriteLine("");
                }

                string bacaPasiva = Laporan.TampilPasiva(pLabaRugi);
                if (bacaPasiva != "0")
                {
                    pasiva = int.Parse(bacaPasiva);
                    file.WriteLine("Total Pasiva: Rp." + pasiva.ToString("0,###"));
                    file.WriteLine("");
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
        #endregion
    }
}
