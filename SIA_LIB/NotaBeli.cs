using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using MySql.Data.MySqlClient;

namespace SIA_LIB
{
    public class NotaBeli
    {
        // Data Member
        private string noNota;
        private DateTime tgl;
        private int hargaTotal;
        private DateTime tglBatasPelunasan;
        private DateTime tglBatasDiskon;
        private double diskonPelunasan;
        private string status;
        private string keterangan;
        private Supplier suplier;
        private List<NotaBeliDetil> listNotaBeliDetil;

        #region CONSTRUCTORS
        public NotaBeli()
        {
            NoNota = "";
            Tgl = DateTime.Now;
            HargaTotal = 0;
            TglBatasPelunasan = DateTime.Now;
            TglBatasDiskon = DateTime.Now;
            DiskonPelunasan = 0;
            Status = "";
            Keterangan = "";
            this.ListNotaBeliDetil = new List<NotaBeliDetil>();
        }
        public NotaBeli(string pnonota, DateTime ptgl, int phargatotal, DateTime ptglbataspelunasan, DateTime ptglbatasdiskon,
            double pdiskon, string pstatus, string pketerangan, Supplier psupplier)
        {
            NoNota = pnonota;
            Tgl = ptgl;
            HargaTotal = phargatotal;
            TglBatasPelunasan = ptglbataspelunasan;
            TglBatasDiskon = ptglbatasdiskon;
            DiskonPelunasan = pdiskon;
            Status = pstatus;
            Keterangan = pketerangan;
            Suplier = psupplier;
            this.ListNotaBeliDetil = new List<NotaBeliDetil>();
        }
        #endregion

        #region PROPERTIES
        public string NoNota
        {
            get { return noNota; }
            set { noNota = value; }
        }
        public DateTime Tgl
        {
            get { return tgl; }
            set { tgl = value; }
        }
        public int HargaTotal
        {
            get { return hargaTotal; }
            set { hargaTotal = value; }
        }
        public DateTime TglBatasPelunasan
        {
            get { return tglBatasPelunasan; }
            set { tglBatasPelunasan = value; }
        }
        public DateTime TglBatasDiskon
        {
            get { return tglBatasDiskon; }
            set { tglBatasDiskon = value; }
        }
        public double DiskonPelunasan
        {
            get { return diskonPelunasan; }
            set { diskonPelunasan = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Keterangan
        {
            get { return keterangan; }
            set { keterangan = value; }
        }
        public Supplier Suplier
        {
            get { return suplier; }
            set { suplier = value; }
        }
        public List<NotaBeliDetil> ListNotaBeliDetil
        {
            get { return listNotaBeliDetil; }
            set { listNotaBeliDetil = value; }
        }
        #endregion

        #region METHODS
        public static string BacaData(string kriteria, string nilaiKriteria, List<NotaBeli> listHasilData)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "SELECT nomor, tanggal, harga_total, diskon_pelunasan, keterangan" +
                      " FROM nota_beli";
            }
            else
            {
                sql = "SELECT nomor, tanggal, harga_total, diskon_pelunasan, keterangan" +
                      " FROM nota_beli" +
                      " WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();
                while (hasilData.Read() == true)
                {
                    NotaBeli nb = new NotaBeli();
                    nb.NoNota = hasilData.GetValue(0).ToString();
                    nb.Tgl = DateTime.Parse(hasilData.GetValue(1).ToString());
                    nb.HargaTotal = int.Parse(hasilData.GetValue(2).ToString());
                    nb.DiskonPelunasan = int.Parse(hasilData.GetValue(3).ToString());
                    nb.keterangan = hasilData.GetValue(4).ToString();

                    listHasilData.Add(nb);
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
            NotaBeliDetil nbd = new NotaBeliDetil(pBarang, pJumlah, pHarga);
            this.ListNotaBeliDetil.Add(nbd);
        }

        public static string TambahData(NotaBeli pNotabeli)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // tuliskan peritnah sql 1: menambahkan data nota ke tabel NotaBeli
                string sql1 = "INSERT INTO nota_beli VALUES ('" + pNotabeli.NoNota + "','" + pNotabeli.Tgl.ToString("yyyy-MM-dd hh:mm:ss") + "','" + pNotabeli.HargaTotal + "','" +
                               pNotabeli.TglBatasPelunasan.ToString("yyyy-MM-dd hh:mm:ss") + "','" + pNotabeli.TglBatasDiskon.ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                               pNotabeli.DiskonPelunasan + "','" + pNotabeli.Status + "','" + pNotabeli.Keterangan + "','" + pNotabeli.Suplier.Id + "')";
                try
                {
                    // jalankan perintah untuk menambahkan ke tabel NotaBeli
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang yg terbeli dalam nota (nota jual detail)
                    for (int i = 0; i < pNotabeli.ListNotaBeliDetil.Count; i++)
                    {
                        // perintah sql 2: menambahkan barang2 yg terbeli ke tabel NotaBeliDetil
                        string sql2 = "INSERT INTO detil_nota_beli VALUES ('" + pNotabeli.NoNota + "','" + pNotabeli.ListNotaBeliDetil[i].Barang.Id + "','" +
                                       pNotabeli.ListNotaBeliDetil[i].Jumlah + "','" + pNotabeli.ListNotaBeliDetil[i].Harga + "')";

                        Koneksi.JalankanPerintahDML(sql2);

                        // Menggunakan metode Average --> Atur harga beli baru & update stok
                        string hasilUpdateStok = Barang.UbahStokTerbeli(pNotabeli.ListNotaBeliDetil[i].Barang.Id, pNotabeli.ListNotaBeliDetil[i].Jumlah);

                        if(hasilUpdateStok == "1")
                        {
                            string hasilUbahHarga = Barang.UbahHargaSatuan(pNotabeli.ListNotaBeliDetil[i].Barang.Id, pNotabeli.ListNotaBeliDetil[i].Jumlah, pNotabeli.ListNotaBeliDetil[i].Harga);     
                        }                        
                    }
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
