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
    public partial class FormSuratPermintaan : Form
    {
        FormUtama frmUtama;
        private List<Barang> listDataBarang = new List<Barang>();

        public FormSuratPermintaan()
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
            dataGridViewBarang.Columns.Add("Satuan", "Satuan");

            // agar lebar dapat menyesuaikan panjang/ isi data
            dataGridViewBarang.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jenis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Satuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // agar harga jual dan sub Jumlah rata kanan
            dataGridViewBarang.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    
            // agar user tidak bisa menambahkan data langsung di datagridview (harus melalui tombol Tambah)
            dataGridViewBarang.AllowUserToAddRows = false;
        }

        private void FormSuratPermintaan_Load(object sender, EventArgs e)
        {
            try
            {
                frmUtama = (FormUtama)this.Owner;        
                FormatDataGrid();

                textBoxIdBarang.MaxLength = 4;
                textBoxNamaBarang.Enabled = false;
                textBoxJenisBarang.Enabled = false;
                textBoxSatuanBarang.Enabled = false;     
                numericUpDownJumlah.Enabled = true;              
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonTambahBarang_Click(object sender, EventArgs e)
        {
            dataGridViewBarang.Rows.Add(textBoxIdBarang.Text, textBoxNamaBarang.Text, textBoxJenisBarang.Text, numericUpDownJumlah.Value.ToString(), textBoxSatuanBarang.Text);

            textBoxIdBarang.Text = "";
            textBoxNamaBarang.Text = "";
            textBoxJenisBarang.Text = "";
            textBoxSatuanBarang.Text = "";
            numericUpDownJumlah.Text = "";
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
                            textBoxSatuanBarang.Text = listDataBarang[0].Satuan;
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

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                JobOrder jo = new JobOrder();
                jo.Nomor = textBoxNoJobOrder.Text;

                SuratPermintaan sp = new SuratPermintaan();
                sp.Nomor = textBoxNomor.Text;
                sp.Tanggal = dateTimePickerTgl.Value;
                sp.Keterangan = textBoxKeterangan.Text;
                sp.JobOrder = jo;

                // data barang diperoleh dari dataGridView
                for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
                {
                    Barang brg = new Barang();
                    brg.Id = dataGridViewBarang.Rows[i].Cells["IdBarang"].Value.ToString();
                    brg.Nama = dataGridViewBarang.Rows[i].Cells["Nama"].Value.ToString();            
                    int jumlah = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString());

                    SuratPermintaanDetil spd = new SuratPermintaanDetil(brg, jumlah);

                    // simpan detil barang ke SP
                    sp.TambahDetilBarang(brg, jumlah);
                }

                string hasilTambah = SuratPermintaan.TambahData(sp);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Data Surat Permintaan telah tersimpan", "Info");

                    // kosongi data
                    textBoxNomor.Text = "";
                    dateTimePickerTgl.Value = DateTime.Now;
                    textBoxNoJobOrder.Text = "";
                    textBoxKeterangan.Text = "";

                    textBoxIdBarang.Text = "";
                    textBoxNamaBarang.Text = "";
                    textBoxJenisBarang.Text = "";
                    textBoxSatuanBarang.Text = "";

                    listDataBarang.Clear();

                    dataGridViewBarang.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Data Surat Permintaan gagal tersimpan. Pesan Kesalahan : " + hasilTambah, "Kesalahan");
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }       
    }
}
