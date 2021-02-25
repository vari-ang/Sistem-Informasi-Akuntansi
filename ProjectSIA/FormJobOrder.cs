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
    public partial class FormJobOrder : Form
    {
        public List<NotaJual> listDataJual = new List<NotaJual>();
        public List<Barang> listBarang = new List<Barang>();

        public FormJobOrder()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormJobOrder_Load(object sender, EventArgs e)
        {
            try
            {
                listDataJual.Clear();

                string bacaNotaJual = NotaJual.BacaData("", "", listDataJual);

                if (bacaNotaJual == "1")
                {
                    comboBoxNoNota.Items.Clear();
                    for (int i = 0; i < listDataJual.Count; i++)
                    {
                        comboBoxNoNota.Items.Add(listDataJual[i].NoNota);
                    }
                }
                else
                {
                    MessageBox.Show("Data Nota Jual gagal ditampilkan. Pesan kesalahan: " + bacaNotaJual);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }       

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                NotaJual nota = new NotaJual();
                if (comboBoxNoNota.Text == "")
                {
                    nota.NoNota = "-";
                }
                else {
                    int indexDipilihUser = comboBoxNoNota.SelectedIndex;
                    nota = listDataJual[indexDipilihUser];
                }               

                Barang b = new Barang();
                b.Id = textBoxIDBarang.Text;

                JobOrder job = new JobOrder();
                job.Nomor = textBoxNoJobOrder.Text;
                job.NotaJual = nota;
                job.Barang = b;
                job.Quantity = (int)(numericUpDownQuantity.Value);
                job.TglMulai = dateTimePickerTglMulai.Value;
                job.TglSelesai = dateTimePickerTglSelesai.Value;
                job.DirectMaterial = (int)(numericUpDownDirectMaterial.Value);
                job.DirectLabor = (int)(numericUpDownDirectLabor.Value);
                job.FactoryOverhead = (int)(numericUpDownFactoryOverhead.Value);               

                string hasilTambah = JobOrder.TambahData(job);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Job Order telah tersimpan.", "Informasi");

                    // kosongi textbox
                    textBoxNoJobOrder.Text = "";
                    textBoxIDBarang.Text = "";
                    numericUpDownQuantity.Value = 0;
                    dateTimePickerTglMulai.Value = DateTime.Now;
                    dateTimePickerTglSelesai.Value = DateTime.Now;
                    numericUpDownDirectLabor.Value = 0;
                    numericUpDownDirectMaterial.Value = 0;
                    numericUpDownFactoryOverhead.Value = 0;           
                }
                else
                {
                    MessageBox.Show("Gagal menambah Job Order. Pesan kesalahan: " + hasilTambah);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
