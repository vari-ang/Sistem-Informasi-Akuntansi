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
    public partial class FormSuratJalan : Form
    {
        List<SuratPermintaan> listSP = new List<SuratPermintaan>();
        List<Barang> listBarang= new List<Barang>();

        public FormSuratJalan()
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

            dataGridViewBarang.Columns.Add("IdBarang", "Id Barang");
            dataGridViewBarang.Columns.Add("NamaBarang", "Nama Barang");
            dataGridViewBarang.Columns.Add("JenisBarang", "Jenis Barang");
            dataGridViewBarang.Columns.Add("JumlahBarang", "Jumlah Barang");
           
            dataGridViewBarang.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["NamaBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["JenisBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["JumlahBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void textBoxNoSuratPermintaan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNoSuratPermintaan.Text.Length == textBoxNoSuratPermintaan.MaxLength)
                {
                    FormatDataGrid();
                    listSP.Clear();
                    dataGridViewBarang.Rows.Clear();

                    // car nama barang sesuai barcode yang diinputkan user
                    string hasil = SuratPermintaan.BacaData("nomor", textBoxNoSuratPermintaan.Text, listSP);

                    if (hasil == "1")
                    {
                        if (listSP.Count > 0)
                        {
                            for (int i = 0; i < listSP.Count; i++)
                            {
                                for (int j = 0; j < listSP[i].ListSuratPermintaanDetil.Count; j++)
                                {
                                    dataGridViewBarang.Rows.Add(listSP[i].ListSuratPermintaanDetil[j].Barang.Id,
                                       listSP[i].ListSuratPermintaanDetil[j].Barang.Nama, listSP[i].ListSuratPermintaanDetil[j].Barang.Jenis,
                                       listSP[i].ListSuratPermintaanDetil[j].Jumlah);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Barang tidak ditemukan.");
                            dataGridViewBarang.Rows.Clear();
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

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {         
                SuratPermintaan sp = new SuratPermintaan();
                sp.Nomor = textBoxNoSuratPermintaan.Text;

                SuratJalan sj = new SuratJalan();
                sj.Nomor = textBoxNomor.Text;
                sj.Tanggal = dateTimePickerTgl.Value;
                sj.Jenis = comboBoxJenis.Text;
                sj.Keterangan = textBoxKeterangan.Text;
                sj.SuratPermintaan = sp;

                // data barang diperoleh dari dataGridView
                for (int i = 0; i < dataGridViewBarang.Rows.Count - 1; i++)
                {
                    Barang brg = new Barang();
                    brg.Id = dataGridViewBarang.Rows[i].Cells["IdBarang"].Value.ToString();
                    brg.Nama = dataGridViewBarang.Rows[i].Cells["NamaBarang"].Value.ToString();
                    brg.Jenis = dataGridViewBarang.Rows[i].Cells["JenisBarang"].Value.ToString();
                    int jumlah = int.Parse(dataGridViewBarang.Rows[i].Cells["JumlahBarang"].Value.ToString());

                    // Cari HPP barang
                    listBarang.Clear();
                    Barang.BacaData("id", brg.Id, listBarang);
                    int hpp = listBarang[0].HargaBeli;

                    SuratJalanDetil sjd = new SuratJalanDetil(brg, jumlah, hpp);

                    // simpan detil barang ke SP
                    sj.TambahDetilBarang(brg, jumlah, hpp);
                }

                string hasilTambah = SuratJalan.TambahData(sj);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Data Surat Jalan telah tersimpan", "Info");

                    // kosongi data
                    textBoxNoSuratPermintaan.Text = "";
                    textBoxNomor.Text = "";
                    dateTimePickerTgl.Value = DateTime.Now;
                    comboBoxJenis.SelectedIndex = -1;
                    textBoxKeterangan.Text = "";

                    listSP.Clear();
                    listBarang.Clear();

                    dataGridViewBarang.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Data Surat Jalan gagal tersimpan. Pesan Kesalahan : " + hasilTambah, "Kesalahan");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void FormSuratJalan_Load(object sender, EventArgs e)
        {

        }
    }
}
