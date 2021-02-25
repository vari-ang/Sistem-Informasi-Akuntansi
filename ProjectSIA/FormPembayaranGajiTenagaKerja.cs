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
    public partial class FormPembayaranGajiTenagaKerja : Form
    {
        FormUtama frmUtama;
        Periode periode;
        List<JobOrder> listJobOrder = new List<JobOrder>();

        public FormPembayaranGajiTenagaKerja()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBayarGajiKaryawan_Load(object sender, EventArgs e)
        {
            frmUtama = (FormUtama)this.Owner;
            periode = frmUtama.periode;
        }

        private void textBoxNoJobOrder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNoJobOrder.Text.Length == textBoxNoJobOrder.MaxLength)
                {
                    listJobOrder.Clear(); 

                    // car nama barang sesuai barcode yang diinputkan user
                    int totalGaji;
                    string hasil = JobOrder.TampilTotalGaji("nomor_job_order", textBoxNoJobOrder.Text, out totalGaji);

                    if (hasil == "1")
                    {
                        
                        numericUpDownNominal.Value = totalGaji;
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
                Transaksi t = new Transaksi("00006", textBoxKeterangan.Text);

                int idJurnalBaru = Jurnal.GetIdJurnalTerbaru();

                Jurnal jur = new Jurnal();

                jur.IdJurnal = idJurnalBaru;
                jur.Tanggal = DateTime.Now;
                jur.NomorBukti = textBoxNoJobOrder.Text;
                jur.Jenis = "JU";
                jur.Periode = periode;
                jur.Transaksi = t;

                jur.TambahDetilJurnalTrans000006((int)numericUpDownNominal.Value);

                string hasilPostingJurnal = Jurnal.TambahData(jur);

                if (hasilPostingJurnal == "1")
                {
                    MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                    // UPDATE DIRECT LABOR DI JOB ORDER
                    string bacaUbahJobOrder = JobOrder.UbahData(textBoxNoJobOrder.Text, "direct_labor", (int)numericUpDownNominal.Value);
                    if (bacaUbahJobOrder == "1")
                    {
                        MessageBox.Show("Berhasil mengupdate data Direct Labor pada Job Order " + textBoxNoJobOrder.Text);
                        
                        // Kosongi Data
                        textBoxNoJobOrder.Text = "";
                        numericUpDownNominal.Value = 0;
                        textBoxKeterangan.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengupdate data Direct Labor pada Job Order " + textBoxNoJobOrder.Text + ". Pesan kesalahan: " + bacaUbahJobOrder);
                    }            
                }
                else
                {
                    MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
