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
    public partial class FormDaftarSuratPermintaan : Form
    {
        List<SuratPermintaan> listHasil = new List<SuratPermintaan>();
        
        public FormDaftarSuratPermintaan()
        {
            InitializeComponent();
        }

        private void FormatDataGrid()
        {
            dataGridViewSuratPermintaan.Columns.Clear();

            dataGridViewSuratPermintaan.Columns.Add("NoSurat", "Surat Permintaan No.");
            dataGridViewSuratPermintaan.Columns.Add("NoJob", "Job Order No.");
            dataGridViewSuratPermintaan.Columns.Add("Tanggal", "Tanggal");            
            dataGridViewSuratPermintaan.Columns.Add("Keterangan", "Keterangan");
            dataGridViewSuratPermintaan.Columns.Add("IdBarang", "Id Barang");
            dataGridViewSuratPermintaan.Columns.Add("NamaBarang", "Nama Barang");
            dataGridViewSuratPermintaan.Columns.Add("JenisBarang", "Jenis Barang");
            dataGridViewSuratPermintaan.Columns.Add("JumlahBarang", "Jumlah Barang");

            dataGridViewSuratPermintaan.Columns["NoSurat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["Keterangan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["NoJob"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["NamaBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["JenisBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewSuratPermintaan.Columns["JumlahBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void FormDaftarSuratPermintaan_Load(object sender, EventArgs e)
        {
            try
            {
                FormatDataGrid();
                listHasil.Clear();
                dataGridViewSuratPermintaan.Rows.Clear();

                string hasilBaca = SuratPermintaan.BacaData("", "", listHasil);

                if (hasilBaca == "1")
                {
                    dataGridViewSuratPermintaan.Rows.Clear();

                    for (int i = 0; i < listHasil.Count; i++)
                    {
                        for (int j = 0; j < listHasil[i].ListSuratPermintaanDetil.Count; j++)
                        {
                            dataGridViewSuratPermintaan.Rows.Add(listHasil[i].Nomor, listHasil[i].JobOrder.Nomor,
                               listHasil[i].Tanggal, listHasil[i].Keterangan, listHasil[i].ListSuratPermintaanDetil[j].Barang.Id,
                               listHasil[i].ListSuratPermintaanDetil[j].Barang.Nama, listHasil[i].ListSuratPermintaanDetil[j].Barang.Jenis,
                               listHasil[i].ListSuratPermintaanDetil[j].Jumlah);

                        }
                    }
                }
            }
            catch(Exception exc) {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
