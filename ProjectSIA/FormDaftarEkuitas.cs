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
    public partial class FormDaftarEkuitas : Form
    {
        FormUtama frmUtama;
        List<Laporan> listlaporan = new List<Laporan>();
        public FormDaftarEkuitas()
        {
            InitializeComponent();
        }

        private void FormatDataGrid()
        {
            dataGridViewEkuitas.Columns.Clear();

            dataGridViewEkuitas.Columns.Add("nomorakun", "No.Akun");
            dataGridViewEkuitas.Columns.Add("nama", "Nama");
            dataGridViewEkuitas.Columns.Add("kelompok", "Kelompok");
            dataGridViewEkuitas.Columns.Add("saldoakhir", "Saldo AKhir");

            dataGridViewEkuitas.Columns["nomorakun"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEkuitas.Columns["nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEkuitas.Columns["kelompok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewEkuitas.Columns["saldoakhir"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewEkuitas.Columns["nomorakun"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewEkuitas.Columns["saldoakhir"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void FormDaftarEkuitas_Load(object sender, EventArgs e)
        {
            try
            {
                frmUtama = (FormUtama)this.Owner;

                listlaporan.Clear();
                FormatDataGrid();

                string baca = Laporan.TampilPerubahanEkuitas("", "", listlaporan);
                if (baca == "1")
                {
                    dataGridViewEkuitas.Rows.Clear();
                    for (int i = 0; i < listlaporan.Count; i++)
                    {
                        dataGridViewEkuitas.Rows.Add(listlaporan[i].Akun.Nomor,
                            listlaporan[i].Akun.Nama, listlaporan[i].Akun.Kelompok, listlaporan[i].Saldoakhir.ToString("0,###"));
                    }

                    string bacaEkuitas = Laporan.TampilTotalEkuitasSebelum();
                    int ekuitasSebelum = 0;
                    if (bacaEkuitas != "0")
                    {
                        ekuitasSebelum = int.Parse(bacaEkuitas);
                        labelEkuitasSebelumLabaRugi.Text = ekuitasSebelum.ToString("0,###");
                    }

                    labelLabaRugi.Text = frmUtama.HitungLabaRugi().ToString("0,###");

                    int ekuitasSesudah = ekuitasSebelum + frmUtama.HitungLabaRugi();
                    labelEkuitasSesudahLabaRugi.Text = ekuitasSesudah.ToString("0,###");
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            string hasilCetak = Laporan.CetakEkuitas("laporan_ekuitas.txt", frmUtama.HitungLabaRugi());

            if (hasilCetak == "1")
            {
                MessageBox.Show("Laporan ekuitas telah tercetak");
            }
            else
            {
                MessageBox.Show("Laporan ekuitas gagal dicetak. Pesan kesalahan: " + hasilCetak);
            }
        }
    }
}
