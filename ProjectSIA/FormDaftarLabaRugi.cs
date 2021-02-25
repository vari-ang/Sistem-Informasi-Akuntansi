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
    public partial class FormDaftarLabaRugi : Form
    {
        FormUtama frmUtama;
        Laporan pendapatan = new Laporan();
        Laporan biaya = new Laporan();
        List<Laporan> listLaporan = new List<Laporan>();
        
        public FormDaftarLabaRugi()
        {
            InitializeComponent();
        }

        public int HitungLabaRugi(int pendapatan, int biaya)
        {
            return pendapatan - biaya;
        }

        private void FormatDataGrid()
        {
            dataGridViewLabaRugi.Columns.Clear();

            dataGridViewLabaRugi.Columns.Add("NomorAkun", "Nomor Akun");
            dataGridViewLabaRugi.Columns.Add("NamaAkun", "Nama Akun");
            dataGridViewLabaRugi.Columns.Add("Kelompok", "Kelompok");
            dataGridViewLabaRugi.Columns.Add("SaldoAkhir", "Saldo Akhir");

            dataGridViewLabaRugi.Columns["NomorAkun"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewLabaRugi.Columns["NamaAkun"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewLabaRugi.Columns["Kelompok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewLabaRugi.Columns["SaldoAkhir"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewLabaRugi.Columns["SaldoAkhir"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void FormDaftarLabaRugi_Load(object sender, EventArgs e)
        {
            try
            {
                    listLaporan.Clear();
                    FormatDataGrid();

                    frmUtama = (FormUtama)this.Owner;

                    string baca = Laporan.TampilLabaRugi(listLaporan);
                    if (baca == "1")
                    {
                        dataGridViewLabaRugi.Rows.Clear();
                        for (int i = 0; i < listLaporan.Count; i++)
                        {
                            dataGridViewLabaRugi.Rows.Add(listLaporan[i].Akun.Nomor,
                                listLaporan[i].Akun.Nama, listLaporan[i].Akun.Kelompok, listLaporan[i].Saldoakhir.ToString("0,###"));
                        }

                        labelTotalPendapatan.Text = frmUtama.HitungPendapatan().ToString("0,###");
                        labelTotalBiaya.Text = frmUtama.HitungBiaya().ToString("0,###");
                        labelLabaRugi.Text = frmUtama.HitungLabaRugi().ToString("0,###");
                }
            }
            catch (Exception exc)
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
            string hasilCetak = Laporan.CetakLabaRugi("laporan_labarugi.txt", frmUtama.HitungPendapatan(),
                frmUtama.HitungBiaya(), frmUtama.HitungLabaRugi());

            if (hasilCetak == "1")
            {
                MessageBox.Show("Laporan Laba Rugi telah tercetak");
            }
            else
            {
                MessageBox.Show("Laporan Laba Rugi gagal dicetak. Pesan kesalahan: " + hasilCetak);
            }
        }
    }
}
