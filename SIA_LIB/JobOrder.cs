using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace SIA_LIB
{
    public class JobOrder
    {
        // Data Member
        private string nomor;
        private NotaJual notaJual; // aggregation  
        private Barang barang; // aggregation    
        private int quantity;
        private DateTime tglMulai;
        private DateTime tglSelesai;
        private int directMaterial;
        private int directLabor;
        private int factoryOverhead;              

        #region CONSTRUCTORS
        public JobOrder()
        {
            Nomor = "";
            TglMulai = DateTime.Now;
            TglSelesai = DateTime.Now;
            Quantity = 0;
            DirectMaterial = 0;
            DirectLabor = 0;
            FactoryOverhead = 0;            
        }
        public JobOrder(string pNomor, NotaJual pNotaJual, Barang pBrg, int pQuantity, DateTime pTglMulai, DateTime pTglSelesai, int pDirectMaterial, int pDirectLabor, int pFactoyOverhead)
        {
            Nomor = pNomor;
            NotaJual = pNotaJual;
            Barang = pBrg; 
            TglMulai = pTglMulai;
            TglSelesai = pTglSelesai;
            Quantity = pQuantity;
            DirectMaterial = pDirectMaterial;
            DirectLabor = pDirectLabor;
            FactoryOverhead = pFactoyOverhead;          
        }
        #endregion 

        #region PROPERTIES
        public string Nomor
        {
            get { return nomor; }
            set { nomor = value; }
        }
        public DateTime TglMulai
        {
            get { return tglMulai; }
            set { tglMulai = value; }
        }
        public DateTime TglSelesai
        {
            get { return tglSelesai; }
            set { tglSelesai = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int DirectMaterial
        {
            get { return directMaterial; }
            set { directMaterial = value; }
        }
        public int DirectLabor
        {
            get { return directLabor; }
            set { directLabor = value; }
        }
        public int FactoryOverhead
        {
            get { return factoryOverhead; }
            set { factoryOverhead = value; }
        }
        public Barang Barang
        {
            get { return barang; }
            set { barang = value; }
        }
        public NotaJual NotaJual
        {
            get { return notaJual; }
            set { notaJual = value; }
        }        
        #endregion

        #region METHODS
        public static string TambahData(JobOrder pJobOrder)
        {
            string sql = "INSERT INTO job_order(nomor, nomor_nota_jual, id_barang, quantity, tanggal_mulai, tanggal_selesai, direct_material, direct_labor, factory_overhead) " +
                         "VALUES('" + pJobOrder.Nomor + "','" + pJobOrder.NotaJual.NoNota + "','" + pJobOrder.Barang.Id + "','" + pJobOrder.Quantity + "','" + pJobOrder.TglMulai.ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                          pJobOrder.TglSelesai.ToString("yyyy-MM-dd hh:mm:ss") + "','" + pJobOrder.DirectMaterial + "','" + pJobOrder.DirectLabor + "','" + pJobOrder.FactoryOverhead + "')";

            try
            {
                Koneksi.JalankanPerintahDML(sql);
                return "1";
            }
            catch (Exception exc)
            {
                return exc.Message + ". Perintah sql: " + sql;
            }
        }

        public static string BacaData(string kriteria, string nilaiKriteria, List<JobOrder> listHasilData)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "SELECT jo.nomor, jo.nomor_nota_jual, b.id, b.nama, jo.quantity, jo.tanggal_mulai, jo.tanggal_selesai, jo.direct_material, jo.direct_labor, jo.factory_overhead " + 
                      "FROM job_order jo INNER JOIN barang b ON b.id = jo.id_barang";
            }
            else
            {
                sql = "SELECT jo.nomor, jo.nomor_nota_jual, b.id, b.nama, jo.quantity, jo.tanggal_mulai, jo.tanggal_selesai, jo.direct_material, jo.direct_labor, jo.factory_overhead " +
                      "FROM job_order jo INNER JOIN barang b ON b.id = jo.id_barang " +
                      " WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();
                while (hasilData.Read() == true)
                {
                    NotaJual nj = new NotaJual();
                    nj.NoNota = hasilData.GetValue(1).ToString();

                    Barang b = new Barang();
                    b.Id = hasilData.GetValue(2).ToString();
                    b.Nama = hasilData.GetValue(3).ToString();

                    JobOrder jo = new JobOrder();
                    jo.Nomor = hasilData.GetValue(0).ToString();
                    jo.NotaJual = nj;
                    jo.Barang = b;
                    jo.Quantity = int.Parse(hasilData.GetValue(4).ToString());
                    jo.TglMulai = DateTime.Parse(hasilData.GetValue(5).ToString());
                    jo.TglSelesai = DateTime.Parse(hasilData.GetValue(6).ToString());
                    jo.DirectMaterial = int.Parse(hasilData.GetValue(7).ToString());
                    jo.DirectLabor = int.Parse(hasilData.GetValue(8).ToString());
                    jo.FactoryOverhead = int.Parse(hasilData.GetValue(9).ToString());

                    listHasilData.Add(jo);
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string UbahData(string pNomor, string pNamaKolom, int pNominalBaru)
        {
            string sql = "UPDATE job_order " +
                         "SET " + pNamaKolom + " = '" + pNominalBaru + "' " +
                         "WHERE nomor = '" + pNomor + "'";

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

        public static string TampilNoJobOrder(string pNoSuratJalan, out string pNoJobOrder)
        {
            pNoJobOrder = "";
            string sql = "SELECT jo.nomor FROM surat_jalan sj " +
                         "INNER JOIN surat_permintaan sp ON sp.nomor = sj.nomor_surat_permintaan " +
                         "INNER JOIN job_order jo ON sp.nomor_job_order = jo.nomor " +
                         "WHERE sj.nomor = '" + pNoSuratJalan + "'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    pNoJobOrder = hasilData.GetValue(0).ToString();
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilTotalDirectMaterial(string kriteria, string nilaiKriteria, out int pTotalBiaya)
        {
            pTotalBiaya = 0;
            string sql = "SELECT SUM(jumlah * hpp) AS totalBiaya FROM detil_surat_jalan " +
                         "WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    pTotalBiaya = int.Parse(hasilData.GetValue(0).ToString());
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilTotalGaji(string kriteria, string nilaiKriteria, out int pTotalGaji)
        {
            pTotalGaji = 0;
            string sql = "SELECT SUM(jumlah * upah) AS totalGaji FROM karyawan_has_job_order " +
                         "WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                
                if (hasilData.Read() == true)
                {
                    pTotalGaji = int.Parse(hasilData.GetValue(0).ToString());                  
                }
                return "1";
            }
            catch (MySqlException exc)
            {
                return exc.Message + ". Perintah sql : " + sql;
            }
        }

        public static string TampilTotalBiaya(string kriteria, string nilaiKriteria, out int pTotalBiaya)
        {
            pTotalBiaya = 0;
            string sql = "SELECT (direct_material + direct_labor + factory_overhead) as TotalBiaya from job_order " +
                         "WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);

                if (hasilData.Read() == true)
                {
                    pTotalBiaya = int.Parse(hasilData.GetValue(0).ToString());
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
