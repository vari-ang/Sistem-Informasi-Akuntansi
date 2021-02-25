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
    public partial class FormPembelian : Form
    {
        FormUtama frmUtama;
        Periode periode;
        private List<Supplier> listDataSupplier = new List<Supplier>();
        private List<Barang> listDataBarang = new List<Barang>();

        public FormPembelian()
        {
            InitializeComponent();
        }

        private void FormatDataGrid()
        {
            dataGridViewBarang.Columns.Clear();

            // menambah kolom di datagridview
            dataGridViewBarang.Columns.Add("IdBarang", "Id Barang");
            dataGridViewBarang.Columns.Add("Nama", "Nama");
            dataGridViewBarang.Columns.Add("Jenis", "Jenis");
            dataGridViewBarang.Columns.Add("Jumlah", "Jumlah");
            dataGridViewBarang.Columns.Add("HargaBeli", "Harga Beli");
            dataGridViewBarang.Columns.Add("Satuan", "Satuan");

            // agar lebar dapat menyesuaikan panjang/ isi data
            dataGridViewBarang.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jenis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["HargaBeli"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Satuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // agar harga jual dan sub Jumlah rata kanan
            dataGridViewBarang.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["HargaBeli"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // agar harga jual dan sub total ditampilkan dengan format pemisah ribuan (1000 delimiter)
            dataGridViewBarang.Columns["HargaBeli"].DefaultCellStyle.Format = "0,###";

            // agar user tidak bisa menambahkan data langsung di datagridview (harus melalui tombol Tambah)
            dataGridViewBarang.AllowUserToAddRows = false;
        }

        private int HitungGrandTotal()
        {
            int grandTotal = 0;
            for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
            {
                int subTotal = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString()) * int.Parse(dataGridViewBarang.Rows[i].Cells["HargaBeli"].Value.ToString());
                grandTotal = grandTotal + subTotal;
            }
            return grandTotal;
        }

        private void FormPembelian_Load(object sender, EventArgs e)
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

                listDataSupplier.Clear();

                string hasilBaca = Supplier.BacaData("", "", listDataSupplier);

                if (hasilBaca == "1")
                {
                    comboBoxSupplier.Items.Clear();
                    for (int i = 0; i < listDataSupplier.Count; i++)
                    {
                        // Tampilkan dengan format kode kategori - nama kategori
                        comboBoxSupplier.Items.Add(listDataSupplier[i].Id + " -- " + listDataSupplier[i].Nama);
                    }
                }
                else
                {
                    MessageBox.Show("Data Supplier gagal ditampilkan. Pesan kesalahan: " + hasilBaca);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBoxIdBarang_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxIdBarang.Text.Length == textBoxIdBarang.MaxLength)
                {
                    listDataBarang.Clear();

                    // car nama barang sesuai barcode yang diinputkan user
                    string hasil = Barang.BacaData("Id", textBoxIdBarang.Text, listDataBarang);

                    if (hasil == "1")
                    {
                        if (listDataBarang.Count > 0)
                        {
                            textBoxJenisBarang.Text = listDataBarang[0].Jenis;
                            textBoxNamaBarang.Text = listDataBarang[0].Nama;
                            //numericUpDownHargaSatuan.Value = listDataBarang[0].HargaBeli;
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
                dataGridViewBarang.Rows.Add(textBoxIdBarang.Text, textBoxNamaBarang.Text, textBoxJenisBarang.Text, numericUpDownJumlah.Value.ToString(), numericUpDownHargaSatuan.Value.ToString(), textBoxSatuanBarang.Text);

                textBoxIdBarang.Text = "";
                textBoxNamaBarang.Text = "";
                textBoxJenisBarang.Text = "";
                textBoxSatuanBarang.Text = "";
                numericUpDownHargaSatuan.Text = "";
                numericUpDownJumlah.Text = "";

                // Hitung biaya total
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
                int indexDipilihUser = comboBoxSupplier.SelectedIndex;
                Supplier s = listDataSupplier[indexDipilihUser];

                NotaBeli nota = new NotaBeli();

                nota.NoNota = textBoxNoNota.Text;
                nota.Tgl = dateTimePickerTgl.Value;
                nota.HargaTotal = int.Parse(labelBiayaTotal.Text);
                nota.TglBatasPelunasan = dateTimePickerTglBatasPelunasan.Value;
                nota.TglBatasDiskon = dateTimePickerTglBatasDiskon.Value;
                nota.DiskonPelunasan = (int)(numericUpDownDiskon.Value);
                nota.Status = comboBoxStatus.Text;
                nota.Keterangan = textBoxKeterangan.Text;
                nota.Suplier = s;

                // data barang diperoleh dari dataGridView
                for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
                {
                    Barang brg = new Barang();
                    brg.Id = dataGridViewBarang.Rows[i].Cells["IdBarang"].Value.ToString();
                    brg.Nama = dataGridViewBarang.Rows[i].Cells["Nama"].Value.ToString();
                    int harga = int.Parse(dataGridViewBarang.Rows[i].Cells["HargaBeli"].Value.ToString());
                    int jumlah = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString());

                    NotaBeliDetil notaDetil = new NotaBeliDetil(brg, jumlah, harga);

                    // simpan detil barang ke nota
                    nota.TambahDetilBarang(brg, jumlah, harga);
                }

                string hasilTambah = NotaBeli.TambahData(nota);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Data nota beli telah tersimpan", "Info");

                    // AKUNTANSI - POSTING KE JURNAL

                    // periode ambil dari form utama
                    FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;

                    // buat transaksi
                    Transaksi t = new Transaksi("00001", textBoxKeterangan.Text);

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
                    jurnal.TambahDetilJurnalTrans000001(int.Parse(labelBiayaTotal.Text));

                    // simpan ke database
                    string hasilPostingJurnal = Jurnal.TambahData(jurnal);

                    if (hasilPostingJurnal == "1")
                    {
                        MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                        // kosongi data
                        labelBiayaTotal.Text = "0";
                        textBoxNoNota.Text = "";
                        comboBoxSupplier.Text = "";
                        comboBoxStatus.Text = "";
                        numericUpDownDiskon.Value = 0;
                        textBoxKeterangan.Text = "";

                        FormPembelian_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                    }
                }
                else
                {
                    MessageBox.Show("Data nota beli gagal tersimpan. Pesan kesalahan: " + hasilTambah, "Kesalahan");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
