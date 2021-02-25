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
   
    public partial class FormDaftarNeraca : Form
    {
        FormUtama frmUtama;
        List<Laporan> listlaporan = new List<Laporan>();
        Periode periode;

        public FormDaftarNeraca()
        {
            InitializeComponent();
        }
        private void FormatDataGrid()
        {
            dataGridViewNeraca.Columns.Clear();

            dataGridViewNeraca.Columns.Add("nomorakun", "No.Akun");
            dataGridViewNeraca.Columns.Add("nama", "Nama");
            dataGridViewNeraca.Columns.Add("kelompok", "Kelompok");
            dataGridViewNeraca.Columns.Add("total", "Total");

            dataGridViewNeraca.Columns["nomorakun"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNeraca.Columns["nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNeraca.Columns["kelompok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewNeraca.Columns["total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewNeraca.Columns["nomorakun"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewNeraca.Columns["total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void FormDaftarNeraca_Load(object sender, EventArgs e)
        {
            frmUtama = (FormUtama)this.Owner;
            periode = frmUtama.periode;

            listlaporan.Clear();
            FormatDataGrid();
            string baca = Laporan.TampilNeraca("id_periode", periode.Id, listlaporan);
            if (baca == "1")
            {
                dataGridViewNeraca.Rows.Clear();
                for (int i = 0; i < listlaporan.Count; i++)
                {
                    if (listlaporan[i].Akun.Nomor == "31")
                    {
                        int saldoAkhir = listlaporan[i].Saldoakhir + frmUtama.HitungLabaRugi();
                        dataGridViewNeraca.Rows.Add(listlaporan[i].Akun.Nomor,
                            listlaporan[i].Akun.Nama, listlaporan[i].Akun.Kelompok, saldoAkhir.ToString("0,###"));
                    }
                    else
                    {
                        dataGridViewNeraca.Rows.Add(listlaporan[i].Akun.Nomor,
                            listlaporan[i].Akun.Nama, listlaporan[i].Akun.Kelompok, listlaporan[i].Saldoakhir.ToString("0,###"));
                    }
                }
            }

            int aktiva = 0, pasiva = 0;

            string bacaAktiva = Laporan.TampilAktiva("id_periode", periode.Id);
            if (bacaAktiva != "0")
            {
                aktiva = int.Parse(bacaAktiva);
                labelTotalAktiva.Text = aktiva.ToString("0,###");
            }

            string bacaPasiva = Laporan.TampilPasiva(frmUtama.HitungLabaRugi(), "id_periode", periode.Id);
            if (bacaPasiva != "0")
            {
                pasiva = int.Parse(bacaPasiva);
                labelTotalPasiva.Text = pasiva.ToString("0,###");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            string hasilCetak = Laporan.CetakNeraca("laporan_neraca.txt", frmUtama.HitungLabaRugi());

            if (hasilCetak == "1")
            {
                MessageBox.Show("Laporan neraca telah tercetak");
            }
            else
            {
                MessageBox.Show("Laporan neraca gagal dicetak. Pesan kesalahan: " + hasilCetak);
            }
        }
    }
}
