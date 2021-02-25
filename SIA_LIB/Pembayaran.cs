using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace SIA_LIB
{
    public class Pembayaran
    {
        // Data Member
        private string id;
        private DateTime tgl;
        private int nominal;
        private string metodePembayaran;
        private string keterangan;
        private NotaBeli notaBeli; // aggregation

        #region CONSTRUCTORS
        public Pembayaran()
        {
            Id = "";
            Tgl = DateTime.Now;
            Nominal = 0;
            MetodePembayaran = "";
            Keterangan = "";
        }
        public Pembayaran(string pid, DateTime ptgl, int pnominal, string pmetode, string pKeterangan, NotaBeli pnota)
        {
            Id = pid;
            Tgl = ptgl;
            Nominal = pnominal;
            MetodePembayaran = pmetode;
            NotaBeli = pnota;
            Keterangan = pKeterangan;
        }
        #endregion

        #region PROPERTIES
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime Tgl
        {
            get { return tgl; }
            set { tgl = value; }
        }

        public int Nominal
        {
            get { return nominal; }
            set { nominal = value; }
        }
       
        public string MetodePembayaran
        {
            get { return metodePembayaran; }
            set { metodePembayaran = value; }
        }

        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }

        public NotaBeli NotaBeli
        {
            get { return notaBeli; }
            set { notaBeli = value; }
        }
        #endregion
        
        #region METHODS
        public static string TambahData(Pembayaran pPembayaran)
        {
            // tuliskan peritnah sql 1: menambahkan data nota ke tabel pembayaran
            string sql = "INSERT INTO pembayaran(id, tanggal, nominal, metode_pembayaran, keterangan, nomor_nota_beli) " +
                         "VALUES ('" + pPembayaran.Id + "','" + pPembayaran.Tgl.ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                          pPembayaran.Nominal + "','" + pPembayaran.MetodePembayaran + "','" + pPembayaran.Keterangan + "','" + pPembayaran.NotaBeli.NoNota + "')";

            try
            {
                Koneksi.JalankanPerintahDML(sql);
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah SQL: " + sql;
            }
        }
        #endregion
    }
}
