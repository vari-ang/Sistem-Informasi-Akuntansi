using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIA_LIB;

namespace ProjectSIA
{
    public partial class FormPenyelesaianJobOrder : Form
    {
        FormUtama frmUtama;
        Periode periode;
        List<JobOrder> listJobOrder = new List<JobOrder>();
        List<Barang> listBarang = new List<Barang>();

        public FormPenyelesaianJobOrder()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FormDataGrid()
        {
            dataGridViewJobOrder.Columns.Add("JobNo", "Job No.");
            dataGridViewJobOrder.Columns.Add("NoNotaJual", "NotaJual No.");
            dataGridViewJobOrder.Columns.Add("IDBarang", "ID Barang");
            dataGridViewJobOrder.Columns.Add("NamaBarang", "Nama Barang");
            dataGridViewJobOrder.Columns.Add("Quantity", "Quantity");
            dataGridViewJobOrder.Columns.Add("TglMulai", "Tgl Mulai");
            dataGridViewJobOrder.Columns.Add("TglSelesai", "Tgl Selesai");
            dataGridViewJobOrder.Columns.Add("DirectMaterial", "Direct Material");
            dataGridViewJobOrder.Columns.Add("DirectLabor", "Direct Labor");
            dataGridViewJobOrder.Columns.Add("FactoryOverhead", "Factory Overhead");

            dataGridViewJobOrder.Columns["JobNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["IDBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["NamaBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["NoNotaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["TglMulai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["DirectMaterial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["DirectLabor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["FactoryOverhead"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJobOrder.Columns["TglSelesai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void FormPenyelesaianJobOrder_Load(object sender, EventArgs e)
        {
            try
            {
                frmUtama = (FormUtama)this.Owner;
                periode = frmUtama.periode;               
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBoxNoJobOrder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNoJobOrder.Text.Length == textBoxNoJobOrder.MaxLength)
                {
                    FormDataGrid();
                    listJobOrder.Clear();

                    string hasilBaca = JobOrder.BacaData("nomor", textBoxNoJobOrder.Text, listJobOrder);

                    if (hasilBaca == "1")
                    {
                        dataGridViewJobOrder.Rows.Clear();

                        for (int i = 0; i < listJobOrder.Count; i++)
                        {
                            dataGridViewJobOrder.Rows.Add(listJobOrder[i].Nomor, listJobOrder[i].NotaJual.NoNota, 
                                listJobOrder[i].Barang.Id, listJobOrder[i].Barang.Nama,
                                listJobOrder[i].Quantity, listJobOrder[i].TglMulai, listJobOrder[i].TglSelesai,
                                listJobOrder[i].DirectMaterial.ToString("0,###"), listJobOrder[i].DirectLabor.ToString("0,###"), listJobOrder[i].FactoryOverhead.ToString("0,###"));
                        }
                    }
                }
                else
                {
                    dataGridViewJobOrder.Rows.Clear();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
       
        private void buttonSelesai_Click(object sender, EventArgs e)
        {
            DialogResult pesan = MessageBox.Show("Job Order " + textBoxNoJobOrder.Text + " akan diselesaikan. Apakah Anda yakin ingin melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pesan == DialogResult.Yes)
            {
                try
                {
                    Transaksi t = new Transaksi("00007", textBoxKeterangan.Text);

                    int idJurnalBaru = Jurnal.GetIdJurnalTerbaru();

                    Jurnal jur = new Jurnal();

                    jur.IdJurnal = idJurnalBaru;
                    jur.Tanggal = DateTime.Now;
                    jur.NomorBukti = textBoxNoJobOrder.Text;
                    jur.Jenis = "JU";
                    jur.Periode = periode;
                    jur.Transaksi = t;

                    // Tambah Stok Barang Jadi
                    string idBarang = listJobOrder[0].Barang.Id;
                    string namaBarang = listJobOrder[0].Barang.Nama;

                    string bacaUbahStok = Barang.UbahStokTerbeli(idBarang, listJobOrder[0].Quantity);
                    if (bacaUbahStok == "1")
                    {
                        MessageBox.Show("Berhasil menambah stok bahan jadi " + idBarang);
                    }
                    else
                    {
                        MessageBox.Show("Gagal menambah stok bahan jadi " + idBarang + ". Pesan kesalahan: " + bacaUbahStok);
                    }

                    int totalBiaya = 0;
                    JobOrder.TampilTotalBiaya("nomor", textBoxNoJobOrder.Text, out totalBiaya);

                    jur.TambahDetilJurnalTrans000007(totalBiaya);

                    string hasilPostingJurnal = Jurnal.TambahData(jur);

                    if (hasilPostingJurnal == "1")
                    {
                        MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                        DialogResult pesan2 = MessageBox.Show("Apakah Anda ingin dibuatkan Surat Jalan untuk bukti pengiriman " + idBarang + "(" + namaBarang + ") ke gudang?",
                            "Konfirmasi", MessageBoxButtons.YesNo);
                        if (pesan2 == DialogResult.Yes)
                        {
                            // Barang Jadi
                            Barang brg = new Barang();
                            brg.Id = listJobOrder[0].Barang.Id;
                            brg.Nama = listJobOrder[0].Barang.Nama;
                            brg.Jenis = "BJ";
                            int jumlah = listJobOrder[0].Quantity;

                            // Cari HPP barang
                            listBarang.Clear();
                            Barang.BacaData("id", brg.Id, listBarang);
                            int hpp = listBarang[0].HargaBeli;

                            // Buat surat permintaan
                            string noSPBaru = "";
                            string hasilGenerateNoSP = SuratPermintaan.GenerateNoBaru(out noSPBaru);

                            if(hasilGenerateNoSP == "1")
                            {
                                JobOrder jo = new JobOrder();
                                jo.Nomor = textBoxNoJobOrder.Text;

                                SuratPermintaan sp = new SuratPermintaan();
                                sp.Nomor = noSPBaru;
                                sp.Tanggal = DateTime.Now;
                                sp.Keterangan = "Pembuatan surat penerimaan barang jadi secara otomatis";
                                sp.JobOrder = jo;
                              
                                SuratPermintaanDetil spd = new SuratPermintaanDetil(brg, jumlah);

                                // simpan detil barang ke SP
                                sp.TambahDetilBarang(brg, jumlah);                                

                                string hasilTambah = SuratPermintaan.TambahData(sp);
                                if (hasilTambah == "1")
                                {
                                    // Buat surat jalan
                                    string noSJBaru = "";
                                    string hasilGenerateNoSJ = SuratJalan.GenerateNoBaru(out noSJBaru);

                                    if (hasilGenerateNoSJ == "1")
                                    {
                                        SuratJalan sj = new SuratJalan();
                                        sj.Nomor = noSJBaru;
                                        sj.Tanggal = DateTime.Now;
                                        sj.Jenis = "Keluar";
                                        sj.Keterangan = "Pembuatan surat jalan barang jadi ke gudang secara otomatis";
                                        sj.SuratPermintaan = sp;

                                        SuratJalanDetil sjd = new SuratJalanDetil(brg, jumlah, hpp);

                                        // simpan detil barang ke SP
                                        sj.TambahDetilBarang(brg, jumlah, hpp);

                                        string hasilTambah2 = SuratJalan.TambahData(sj);
                                        if (hasilTambah2 == "1")
                                        {
                                            MessageBox.Show("Data Surat Jalan " + noSJBaru + " telah tersimpan", "Info");

                                            // UPDATE HARGA PER UNIT BARANG JADI
                                            string hasilUbahHarga = Barang.UbahHargaSatuan(idBarang, jumlah, totalBiaya/jumlah);
                                            if (hasilUbahHarga == "1")
                                            {
                                                MessageBox.Show("Berhasil mengubah harga per unit barang jadi menggunakan Metode Average", "Info");
                                                listBarang.Clear();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Gagal mengubah harga per unit barang jadi. Pesan kesalahan: " + hasilUbahHarga, "Kesalahan");

                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Data Surat Jalan " + noSJBaru + " gagal tersimpan. Pesan Kesalahan : " + hasilTambah2, "Kesalahan");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Gagal membuat nomor Surat Jalan. Pesan kesalahan: " + hasilGenerateNoSJ, "Error");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Data Surat Permintaan gagal tersimpan. Pesan Kesalahan : " + hasilTambah, "Kesalahan");
                                }
                            }
                            else {
                                MessageBox.Show("Gagal membuat nomor Surat Permintaan. Pesan kesalahan: " + hasilGenerateNoSP, "Error");
                            }                                                                                
                        }
                        else
                        {
                            MessageBox.Show("Anda bisa menambahkan Surat Jalan secara manual");
                        }

                        // Kosongi Data
                        textBoxNoJobOrder.Text = "";
                        textBoxKeterangan.Text = "";
                        listJobOrder.Clear();
                        dataGridViewJobOrder.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
