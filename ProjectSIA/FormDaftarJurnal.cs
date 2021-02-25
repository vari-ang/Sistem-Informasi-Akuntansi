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
    public partial class FormDaftarJurnal : Form
    {
        List<Jurnal> listDataJurnal = new List<Jurnal>();

        public FormDaftarJurnal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatDataGrid()
        {
            dataGridViewJurnal.Columns.Clear();

            dataGridViewJurnal.Columns.Add("Id", "Id");
            dataGridViewJurnal.Columns.Add("Tanggal", "Tanggal");
            dataGridViewJurnal.Columns.Add("Keterangan", "Keterangan");
            dataGridViewJurnal.Columns.Add("NamaAkun", "Nama Akun");
            dataGridViewJurnal.Columns.Add("Debet", "Debet");
            dataGridViewJurnal.Columns.Add("Kredit", "Kredit");
            dataGridViewJurnal.Columns.Add("NomorBukti", "Nomor Bukti");
            dataGridViewJurnal.Columns.Add("Jenis", "Jenis");

            dataGridViewJurnal.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJurnal.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dataGridViewJurnal.Columns["Keterangan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJurnal.Columns["NamaAkun"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJurnal.Columns["Debet"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJurnal.Columns["Kredit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJurnal.Columns["NomorBukti"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJurnal.Columns["Jenis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewJurnal.Columns["Debet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewJurnal.Columns["Kredit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void FormDaftarJurnal_Load(object sender, EventArgs e)
        {
            FormatDataGrid();
            listDataJurnal.Clear();

            string hasilBaca = Jurnal.BacaData(listDataJurnal);

            if (hasilBaca == "1")
            {
                dataGridViewJurnal.Rows.Clear();

                for (int i = 0; i < listDataJurnal.Count; i++)
                {
                    dataGridViewJurnal.Rows.Add(listDataJurnal[i].IdJurnal, listDataJurnal[i].Tanggal.ToShortDateString(),
                        listDataJurnal[i].Transaksi.Keterangan, listDataJurnal[i].ListDetilJurnal[0].Akun.Nama, 
                        listDataJurnal[i].ListDetilJurnal[0].Debet.ToString("0,###"), listDataJurnal[i].ListDetilJurnal[0].Kredit.ToString("0,###"),
                        listDataJurnal[i].NomorBukti, listDataJurnal[i].Jenis);
                }
            }
        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            string hasilCetak = Jurnal.Cetak("laporan_jurnal.txt");

            if (hasilCetak == "1")
            {
                MessageBox.Show("Laporan jurnal telah tercetak");
            }
            else
            {
                MessageBox.Show("Laporan jurnal gagal dicetak. Pesan kesalahan: " + hasilCetak);
            }
        }
    }
}
