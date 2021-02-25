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
    public partial class FormPenjualan : Form
    {
        FormUtama frmUtama;
        Periode periode;
        private List<Pelanggan> listDataPelanggan = new List<Pelanggan>();
        private List<Barang> listDataBarang = new List<Barang>();

        int totalHPP = 0;

        public FormPenjualan()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatDataGrid()
        {
            dataGridViewBarang.Columns.Clear();

            // menambah kolom di datagridview
            dataGridViewBarang.Columns.Add("IdBarang", "Id Barang");
            dataGridViewBarang.Columns.Add("Nama", "Nama");
            dataGridViewBarang.Columns.Add("Jenis", "Jenis");
            dataGridViewBarang.Columns.Add("Jumlah", "Jumlah");
            //dataGridViewBarang.Columns.Add("HargaBeli", "Harga Beli");
            dataGridViewBarang.Columns.Add("HargaJual", "Harga Jual");
            dataGridViewBarang.Columns.Add("Satuan", "Satuan");

            // agar lebar dapat menyesuaikan panjang/ isi data
            dataGridViewBarang.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jenis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dataGridViewBarang.Columns["HargaBeli"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["HargaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Satuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // agar harga jual dan sub Jumlah rata kanan
            dataGridViewBarang.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dataGridViewBarang.Columns["HargaBeli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["HargaJual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // agar harga jual dan sub total ditampilkan dengan format pemisah ribuan (1000 delimiter)
            //dataGridViewBarang.Columns["HargaBeli"].DefaultCellStyle.Format = "0,###";
            dataGridViewBarang.Columns["HargaJual"].DefaultCellStyle.Format = "0,###";

            // agar user tidak bisa menambahkan data langsung di datagridview (harus melalui tombol Tambah)
            dataGridViewBarang.AllowUserToAddRows = false;
        }

        private void FormPenjualan_Load(object sender, EventArgs e)
        {
            try
            {
                frmUtama = (FormUtama)this.Owner;
                periode = frmUtama.periode;

                FormatDataGrid();

                textBoxIdBarang.MaxLength = 4;
                textBoxNamaBarang.Enabled = false;
                textBoxJenisBarang.Enabled = false;
                textBoxSatuanBarang.Enabled = false;
                numericUpDownHargaSatuan.Enabled = true;
                numericUpDownJumlah.Enabled = true;

                listDataPelanggan.Clear();

                string hasilBaca = Pelanggan.BacaData("", "", listDataPelanggan);

                if (hasilBaca == "1")
                {
                    comboBoxPelanggan.Items.Clear();
                    for (int i = 0; i < listDataPelanggan.Count; i++)
                    {
                        // Tampilkan dengan format kode kategori - nama kategori
                        comboBoxPelanggan.Items.Add(listDataPelanggan[i].Id + " -- " + listDataPelanggan[i].Nama);
                    }
                }
                else
                {
                    MessageBox.Show("Data Customer gagal ditampilkan. Pesan kesalahan: " + hasilBaca);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private int HitungGrandTotal()
        {
            int grandTotal = 0;
            for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
            {
                int subTotal = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString()) * int.Parse(dataGridViewBarang.Rows[i].Cells["HargaJual"].Value.ToString());
                grandTotal = grandTotal + subTotal;
            }
            return grandTotal;
        }

        private void textBoxIdBarang_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxIdBarang.Text.Length == textBoxIdBarang.MaxLength)
                {
                    listDataBarang.Clear();

                    // cari nama barang sesuai barcode yang diinputkan user
                    string hasil = Barang.BacaData("Id", textBoxIdBarang.Text, listDataBarang);

                    if (hasil == "1")
                    {
                        if (listDataBarang.Count > 0)
                        {
                            textBoxJenisBarang.Text = listDataBarang[0].Jenis;
                            textBoxNamaBarang.Text = listDataBarang[0].Nama;
                            //numericUpDownHargaSatuan.Value = listDataBarang[0].HargaJual;
                            textBoxSatuanBarang.Text = listDataBarang[0].Satuan;
                            //numericUpDownJumlah.Value = listDataBarang[0].Stok;                                              
                        }
                        else
                        {
                            MessageBox.Show("Barang tidak ditemukan.");
                            textBoxIdBarang.Text = "";
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

        private void buttonTambahBarang_Click(object sender, EventArgs e)
        {
            try
            {
                listDataBarang.Clear();
                string hasil = Barang.BacaData("Id", textBoxIdBarang.Text, listDataBarang);
                totalHPP = totalHPP + (listDataBarang[0].HargaBeli * (int)numericUpDownJumlah.Value);

                dataGridViewBarang.Rows.Add(textBoxIdBarang.Text, textBoxNamaBarang.Text, textBoxJenisBarang.Text, numericUpDownJumlah.Value.ToString(), numericUpDownHargaSatuan.Value.ToString(), textBoxSatuanBarang.Text);

                textBoxIdBarang.Text = "";
                textBoxNamaBarang.Text = "";
                textBoxJenisBarang.Text = "";
                textBoxSatuanBarang.Text = "";
                numericUpDownHargaSatuan.Text = "";
                numericUpDownJumlah.Text = "";

                // Hitung biaya total
                //labelBiayaTotal.Text = HitungGrandTotal().ToString("0,###");
                labelBiayaTotal.Text = HitungGrandTotal().ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                int indexDipilihUser = comboBoxPelanggan.SelectedIndex;
                Pelanggan p = listDataPelanggan[indexDipilihUser];
         
                NotaJual nota = new NotaJual();

                nota.NoNota = textBoxNoNota.Text;
                nota.Tgl = dateTimePickerTgl.Value;
                nota.HargaTotal = int.Parse(labelBiayaTotal.Text);
                nota.TglBatasPelunasan = dateTimePickerTglBatasPelunasan.Value;
                nota.TglBatasDiskon = dateTimePickerTglBatasDiskon.Value;
                nota.DiskonPelunasan = (int)(numericUpDownDiskon.Value);
                nota.Status = comboBoxStatus.Text;
                nota.Keterangan = textBoxKeterangan.Text;
                nota.Pelanggan = p;

                // data barang diperoleh dari dataGridView
                for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
                {
                    Barang brg = new Barang();
                    brg.Id = dataGridViewBarang.Rows[i].Cells["IdBarang"].Value.ToString();
                    brg.Nama = dataGridViewBarang.Rows[i].Cells["Nama"].Value.ToString();
                    int harga = int.Parse(dataGridViewBarang.Rows[i].Cells["HargaJual"].Value.ToString());
                    int jumlah = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString());             

                    NotaJualDetil notaDetil = new NotaJualDetil(brg, jumlah, harga);

                    // simpan detil barang ke nota
                    nota.TambahDetilBarang(brg, jumlah, harga);                  
                }

                string hasilTambah = NotaJual.TambahData(nota);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Data nota jual telah tersimpan", "Info");

                    // AKUNTANSI - POSTING KE JURNAL

                    // periode ambil dari form utama
                    FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;

                    // buat transaksi
                    Transaksi t = new Transaksi("00008", textBoxKeterangan.Text);

                    // buat objek bertipe Jurnal
                    int idJurnalBaru = Jurnal.GetIdJurnalTerbaru(); // digenerate otomatis

                    Jurnal jurnal = new Jurnal();
                    jurnal.IdJurnal = idJurnalBaru;
                    jurnal.Tanggal = nota.Tgl;
                    jurnal.NomorBukti = nota.NoNota;
                    jurnal.Jenis = "JU";
                    jurnal.Periode = periode;
                    jurnal.Transaksi = t;

                    // tambahkan detil jurnal
                    jurnal.TambahDetilJurnalTrans000008(int.Parse(labelBiayaTotal.Text), totalHPP);

                    // simpan ke database
                    string hasilPostingJurnal = Jurnal.TambahData(jurnal);

                    if (hasilPostingJurnal == "1")
                    {
                        MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                        // kosongi data
                        labelBiayaTotal.Text = "0";
                        textBoxNoNota.Text = "";
                        comboBoxPelanggan.Text = "";
                        comboBoxStatus.Text = "";
                        numericUpDownDiskon.Value = 0;
                        textBoxKeterangan.Text = "";

                        FormPenjualan_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                    }             
                }
                else
                {
                    MessageBox.Show("Data nota jual gagal tersimpan. Pesan kesalahan: " + hasilTambah, "Kesalahan");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }       
    }
}
