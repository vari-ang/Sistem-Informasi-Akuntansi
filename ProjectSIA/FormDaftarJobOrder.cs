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
    public partial class FormDaftarJobOrder : Form
    {
        public List<JobOrder> listHasilData = new List<JobOrder>();

        public FormDaftarJobOrder()
        {
            InitializeComponent();
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

        private void FormDaftarJobOrder_Load(object sender, EventArgs e)
        {
            try
            {
                FormDataGrid();
                listHasilData.Clear();

                string hasilBaca = JobOrder.BacaData("", "", listHasilData);


                if (hasilBaca == "1")
                {
                    dataGridViewJobOrder.Rows.Clear();

                    for (int i = 0; i < listHasilData.Count; i++)
                    {
                        dataGridViewJobOrder.Rows.Add(listHasilData[i].Nomor, listHasilData[i].NotaJual.NoNota, listHasilData[i].Barang.Id, listHasilData[i].Barang.Nama,
                            listHasilData[i].Quantity, listHasilData[i].TglMulai, listHasilData[i].TglSelesai,
                            listHasilData[i].DirectMaterial.ToString("0,###"), listHasilData[i].DirectLabor.ToString("0,###"), listHasilData[i].FactoryOverhead.ToString("0,###"));
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormJobOrder frm = new FormJobOrder();
            frm.Owner = this;
            frm.Show();

        }
    }
}
