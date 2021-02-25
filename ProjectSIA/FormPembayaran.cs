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
    public partial class FormPembayaran : Form
    {
        FormUtama frmUtama;
        Periode periode;
        List<NotaBeli> listNotaBeli = new List<NotaBeli>();

        public FormPembayaran()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPenerimaanPembayaran_Load(object sender, EventArgs e)
        {
            frmUtama = (FormUtama)this.Owner;
            periode = frmUtama.periode;

            comboBoxJenisPembayaran.Enabled = false;
            numericUpDownNominal.Enabled = false;
            textBoxKeterangan.Enabled = false;
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            string metodeBayar = "";
            if(comboBoxJenisPembayaran.Text == "Kredit")
            {
                metodeBayar = "K";
            }
            else if (comboBoxJenisPembayaran.Text == "Tunai")
            {
                metodeBayar = "T";
            }

            // Ciptakan objek yg akan ditambahkan
            NotaBeli nb = new NotaBeli();
            nb.NoNota = textBoxNoNotaBeli.Text;

            Pembayaran p = new Pembayaran();
            p.Id = textBoxIdPembayaran.Text;
            p.Tgl = dateTimePickerTgl.Value;
            p.MetodePembayaran = metodeBayar;
            p.Nominal = (int)numericUpDownNominal.Value;
            p.Keterangan = textBoxKeterangan.Text;
            p.NotaBeli = nb;

            // Panggil static method TambahData di class kategori
            string hasilTambah = Pembayaran.TambahData(p);

            if (hasilTambah == "1")
            {
                MessageBox.Show("Data Pembayaran Hutang telah tersimpan.", "Informasi");

                // AKUNTANSI - POSTING KE JURNAL

                // periode ambil dari form utama
                FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;

                // buat transaksi
                Transaksi t = new Transaksi("00009", textBoxKeterangan.Text);

                // buat objek bertipe Jurnal
                int idJurnalBaru = Jurnal.GetIdJurnalTerbaru(); // digenerate otomatis

                Jurnal jurnal = new Jurnal();
                jurnal.IdJurnal = idJurnalBaru;
                jurnal.Tanggal = dateTimePickerTgl.Value;
                jurnal.NomorBukti = textBoxNoNotaBeli.Text;
                jurnal.Jenis = "JU";
                jurnal.Periode = periode;
                jurnal.Transaksi = t;

                // tambahkan detil jurnal
                int jumlahDiskon = (((int)numericUpDownDiskon.Value) * ((int)numericUpDownNominal.Value)) / 100;
                jurnal.TambahDetilJurnalTrans000009((int)numericUpDownNominal.Value, jumlahDiskon);

                // simpan ke database
                string hasilPostingJurnal = Jurnal.TambahData(jurnal);

                if (hasilPostingJurnal == "1")
                {
                    MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                    // kosongi data
                    textBoxIdPembayaran.Text = "";
                    dateTimePickerTgl.Value = DateTime.Now;
                    comboBoxJenisPembayaran.SelectedIndex = -1;
                    numericUpDownNominal.Value = 0;
                    numericUpDownDiskon.Value = 0;
                    textBoxKeterangan.Text = "";
                    textBoxNoNotaBeli.Text = "";

                    comboBoxJenisPembayaran.Enabled = false;
                    textBoxKeterangan.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                }
            }
            else
            {
                MessageBox.Show("Gagal menambah Penerimaan Pembayaran. Pesan kesalahan: " + hasilTambah);
            }
        }

        private void textBoxNoNotaJual_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNoNotaBeli.Text.Length == textBoxNoNotaBeli.MaxLength)
            {
                listNotaBeli.Clear();

                string hasilBaca = NotaBeli.BacaData("nomor", textBoxNoNotaBeli.Text, listNotaBeli);
                if (hasilBaca == "1")
                {
                    if (listNotaBeli.Count > 0)
                    {
                        numericUpDownNominal.Value = listNotaBeli[0].HargaTotal;
                        numericUpDownDiskon.Value = (decimal)listNotaBeli[0].DiskonPelunasan;

                        comboBoxJenisPembayaran.Enabled = true;
                        textBoxKeterangan.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Nomor Nota tidak ditemukan. Proses Ubah Data tidak bisa dilakukan");
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan. Pesan kesalahan = " + hasilBaca);
                }
            }
        }
    }
}
