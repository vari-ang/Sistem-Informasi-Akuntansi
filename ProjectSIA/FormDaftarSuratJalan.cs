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
    public partial class FormDaftarSuratJalan : Form
    {
        List<SuratJalan> listHasil = new List<SuratJalan>();

        public FormDaftarSuratJalan()
        {
            InitializeComponent();
        }

        private void FormatDataGrid()
        {
            dataGridViewSuratJalan.Columns.Clear();

            dataGridViewSuratJalan.Columns.Add("NoSuratJalan", "Surat Jalan No.");
            dataGridViewSuratJalan.Columns.Add("NoSuratPenerimaan", "Surat Penerimaan No.");
            dataGridViewSuratJalan.Columns.Add("Tanggal", "Tanggal");
            dataGridViewSuratJalan.Columns.Add("Jenis", "Jenis");
            dataGridViewSuratJalan.Columns.Add("Keterangan", "Keterangan");
            dataGridViewSuratJalan.Columns.Add("IdBarang", "Id Barang");
            dataGridViewSuratJalan.Columns.Add("NamaBarang", "Nama Barang");
            dataGridViewSuratJalan.Columns.Add("JenisBarang", "Jenis Barang");
            dataGridViewSuratJalan.Columns.Add("JumlahBarang", "Jumlah Barang");
            dataGridViewSuratJalan.Columns.Add("HPP", "HPP");

            dataGridViewSuratJalan.Columns["NoSuratJalan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["NoSuratPenerimaan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["Jenis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["Keterangan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["NamaBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["JenisBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["JumlahBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratJalan.Columns["HPP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDaftarSuratJalan_Load(object sender, EventArgs e)
        {
            try
            {
                FormatDataGrid();
                listHasil.Clear();
                dataGridViewSuratJalan.Rows.Clear();

                string hasilBaca = SuratJalan.BacaData("", "", listHasil);

                if (hasilBaca == "1")
                {
                    dataGridViewSuratJalan.Rows.Clear();

                    for (int i = 0; i < listHasil.Count; i++)
                    {
                        for (int j = 0; j < listHasil[i].ListSuratJalanDetil.Count; j++)
                        {
                            dataGridViewSuratJalan.Rows.Add(listHasil[i].Nomor, listHasil[i].SuratPermintaan.Nomor,
                               listHasil[i].Tanggal, listHasil[i].Jenis, listHasil[i].Keterangan, listHasil[i].ListSuratJalanDetil[j].Barang.Id,
                               listHasil[i].ListSuratJalanDetil[j].Barang.Nama, listHasil[i].ListSuratJalanDetil[j].Barang.Jenis,
                               listHasil[i].ListSuratJalanDetil[j].Jumlah, listHasil[i].ListSuratJalanDetil[j].HPP);

                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
