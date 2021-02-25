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
    public partial class FormPerhitunganDanPembebananBiayaTenagaKerja : Form
    {
        FormUtama frmUtama;
        Periode periode;
        private List<Karyawan> listKaryawan = new List<Karyawan>();

        public FormPerhitunganDanPembebananBiayaTenagaKerja()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatDataGrid()
        {
            dataGridViewTenagaKerja.Columns.Clear();

            // menambah kolom di datagridview
            dataGridViewTenagaKerja.Columns.Add("Id", "Id");
            dataGridViewTenagaKerja.Columns.Add("NamaKaryawan", "Nama Karyawan");
            dataGridViewTenagaKerja.Columns.Add("JenisKelamin", "Jenis Kelamin");
            dataGridViewTenagaKerja.Columns.Add("JumlahKerja", "Jumlah Kerja");
            dataGridViewTenagaKerja.Columns.Add("SatuanKerja", "Satuan Kerja");
            dataGridViewTenagaKerja.Columns.Add("RateGaji", "Rate Gaji");

            // agar lebar dapat menyesuaikan panjang/ isi data
            dataGridViewTenagaKerja.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTenagaKerja.Columns["NamaKaryawan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTenagaKerja.Columns["JenisKelamin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTenagaKerja.Columns["SatuanKerja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTenagaKerja.Columns["SatuanKerja"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewTenagaKerja.Columns["RateGaji"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // agar harga jual dan sub Jumlah rata kanan
            dataGridViewTenagaKerja.Columns["JumlahKerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewTenagaKerja.Columns["SatuanKerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewTenagaKerja.Columns["RateGaji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            // agar user tidak bisa menambahkan data langsung di datagridview (harus melalui tombol Tambah)
            dataGridViewTenagaKerja.AllowUserToAddRows = false;
        }

        private void FormPerhitunganDanPembebananBiayaTenagaKerja_Load(object sender, EventArgs e)
        {
            try
            {
                frmUtama = (FormUtama)this.Owner;
                periode = frmUtama.periode;

                FormatDataGrid();                             
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBoxIdTenagaKerja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxIdTenagaKerja.Text.Length == textBoxIdTenagaKerja.MaxLength)
                {
                    listKaryawan.Clear();

                    // car nama barang sesuai barcode yang diinputkan user
                    string hasil = Karyawan.BacaData("id", textBoxIdTenagaKerja.Text, listKaryawan);

                    if (hasil == "1")
                    {
                        if (listKaryawan.Count > 0)
                        {
                            textBoxIdTenagaKerja.Text = listKaryawan[0].Id;
                            textBoxNamaTenagaKerja.Text = listKaryawan[0].Nama;
                            textBoxJenisKelaminTenagaKerja.Text = listKaryawan[0].JenisKelamin;           
                        }
                        else
                        {
                            MessageBox.Show("Karyawan tidak ditemukan.");
                            textBoxIdTenagaKerja.Text = "";
                            textBoxNamaTenagaKerja.Text = "";
                            textBoxJenisKelaminTenagaKerja.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Perintah SQL gagal dijalankan. Pesan kesalahan = " + hasil);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonTambahTenagaKerja_Click(object sender, EventArgs e)
        {
            try
            {               
                if (klikAllKaryawan)
                {
                    dataGridViewTenagaKerja.Rows.Clear();
                    listKaryawan.Clear();
                    string baca = Karyawan.BacaData("", "", listKaryawan);
                    if (baca == "1")
                    {                      
                        for (int i = 0; i < listKaryawan.Count; i++)
                        {
                            dataGridViewTenagaKerja.Rows.Add(listKaryawan[i].Id, listKaryawan[i].Nama, listKaryawan[i].JenisKelamin,
                                numericUpDownSatuanKerja.Value, comboBoxSatuanKerja.Text, numericUpDownRateGaji.Value);
                        }           
                    }
                }           
                else
                {
                    dataGridViewTenagaKerja.Rows.Add(textBoxIdTenagaKerja.Text, textBoxNamaTenagaKerja.Text, textBoxJenisKelaminTenagaKerja.Text,
                        numericUpDownSatuanKerja.Value, comboBoxSatuanKerja.Text, numericUpDownRateGaji.Value);

                    textBoxIdTenagaKerja.Text = "";
                    textBoxNamaTenagaKerja.Text = "";
                    textBoxJenisKelaminTenagaKerja.Text = "";
                    numericUpDownSatuanKerja.Text = "";
                    numericUpDownRateGaji.Text = "";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        bool klikAllKaryawan = false;
        private void checkBoxPilihSemuaTenagaKerja_CheckedChanged(object sender, EventArgs e)
        {           
            if(!klikAllKaryawan) {
                textBoxIdTenagaKerja.Enabled = false;
                textBoxIdTenagaKerja.Text = "";
                textBoxNamaTenagaKerja.Text = "";
                textBoxJenisKelaminTenagaKerja.Text = "";

                klikAllKaryawan = true;
            }
            else
            {
                textBoxIdTenagaKerja.Enabled = true;

                klikAllKaryawan = false;
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Karyawan k1 = new Karyawan();

                string noJobOrder = textBoxNoJobOrder.Text;
                int totalUpah = 0;

                // data barang diperoleh dari dataGridView
                for (int i = 0; i < dataGridViewTenagaKerja.Rows.Count; i++)
                {
                    Karyawan k2 = new Karyawan();
                    k2.Id = dataGridViewTenagaKerja.Rows[i].Cells["Id"].Value.ToString();
                    k2.Nama = dataGridViewTenagaKerja.Rows[i].Cells["NamaKaryawan"].Value.ToString();
                    k2.Nama = dataGridViewTenagaKerja.Rows[i].Cells["JenisKelamin"].Value.ToString();

                    k2.Jumlah = int.Parse(dataGridViewTenagaKerja.Rows[i].Cells["JumlahKerja"].Value.ToString());
                    k2.Satuan = dataGridViewTenagaKerja.Rows[i].Cells["SatuanKerja"].Value.ToString();
                    k2.Upah = int.Parse(dataGridViewTenagaKerja.Rows[i].Cells["RateGaji"].Value.ToString());

                    totalUpah += k2.Jumlah * k2.Upah;
                    
                    // simpan data ke list k1
                    k1.ListKaryawan.Add(k2);
                }

                string hasilTambah = Karyawan.TambahDataKaryawanHasJobOrder(k1, noJobOrder);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Data Perhitungan Gaji Tenaga Kerja telah tersimpan", "Info");                    

                    // AKUNTANSI - POSTING KE JURNAL

                    // periode ambil dari form utama
                    FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;

                    // buat transaksi
                    Transaksi t = new Transaksi("00005", textBoxKeterangan.Text);

                    // buat objek bertipe Jurnal
                    int idJurnalBaru = Jurnal.GetIdJurnalTerbaru(); // digenerate otomatis

                    Jurnal jurnal = new Jurnal();
                    jurnal.IdJurnal = idJurnalBaru;
                    jurnal.Tanggal = DateTime.Now;
                    jurnal.NomorBukti = noJobOrder;
                    jurnal.Jenis = "JU";
                    jurnal.Periode = periode;
                    jurnal.Transaksi = t;

                    // tambahkan detil jurnal
                    jurnal.TambahDetilJurnalTrans000005(totalUpah);

                    // simpan ke database
                    string hasilPostingJurnal = Jurnal.TambahData(jurnal);

                    if (hasilPostingJurnal == "1")
                    {
                        MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                        // kosongi data
                        dataGridViewTenagaKerja.Rows.Clear();
                        textBoxNoJobOrder.Text = "";
                        textBoxIdTenagaKerja.Text = "";
                        textBoxNamaTenagaKerja.Text = "";
                        textBoxJenisKelaminTenagaKerja.Text = "";
                        numericUpDownSatuanKerja.Value = 0;
                        numericUpDownRateGaji.Value = 0;
                    }
                    else
                    {
                        MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                    }
                }
                else
                {
                    MessageBox.Show("Data gagal tersimpan. Pesan kesalahan: " + hasilTambah, "Kesalahan");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
