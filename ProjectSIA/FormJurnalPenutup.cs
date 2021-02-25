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
    public partial class FormJurnalPenutup : Form
    {
        FormUtama frmUtama;

        public FormJurnalPenutup()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormJurnalPenutup_Load(object sender, EventArgs e)
        {
            frmUtama = (FormUtama)this.Owner;
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            DialogResult pesan = MessageBox.Show("Periode akuntansi akan ditutup. Apakah Anda yakin ingin melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pesan == DialogResult.Yes)
            {
                try
                {
                    // tutup periode
                    string hasil = Periode.TutupPeriode(Jurnal.GetIdJurnalTerbaru(),
                        int.Parse(frmUtama.periode.Id), frmUtama.HitungPendapatan(), frmUtama.HitungBiaya(),
                        dateTimePickerTglAwal.Value, dateTimePickerTglAkhir.Value);

                    if (hasil == "1")
                    {
                        MessageBox.Show("Periode " + frmUtama.periode.Id + " telah ditutup.");
                        frmUtama.periode.Id = (int.Parse(frmUtama.periode.Id) + 1).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menutup periode. Pesan kesalahan: " + hasil);
                    }
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
