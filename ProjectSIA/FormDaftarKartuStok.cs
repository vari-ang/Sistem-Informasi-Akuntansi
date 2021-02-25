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
    public partial class FormDaftarKartuStok : Form
    {
        private List<Barang> listHasilData = new List<Barang>();

        public FormDaftarKartuStok()
        {
            InitializeComponent();
        }

        public void FormatDataGrid()
        {
            dataGridViewBarang.Columns.Clear();

            dataGridViewBarang.Columns.Add("IdBarang", "Id Barang");
            dataGridViewBarang.Columns.Add("Nama", "Nama");
            dataGridViewBarang.Columns.Add("Jenis", "Jenis");
            dataGridViewBarang.Columns.Add("Jumlah", "Jumlah");
            dataGridViewBarang.Columns.Add("Satuan", "Satuan");
            dataGridViewBarang.Columns.Add("HargaUnit", "Harga/ Unit");
            dataGridViewBarang.Columns.Add("TotalHarga", "Total Harga");
            dataGridViewBarang.Columns.Add("HargaJual", "Harga Jual");          

            // agar lebar dapat menyesuaikan panjang/ isi data
            dataGridViewBarang.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jenis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["HargaUnit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["TotalHarga"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["HargaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Satuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // agar harga jual dan sub Jumlah rata kanan
            dataGridViewBarang.Columns["TotalHarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["HargaUnit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["HargaJual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // agar harga jual dan sub total ditampilkan dengan format pemisah ribuan (1000 delimiter)
            dataGridViewBarang.Columns["TotalHarga"].DefaultCellStyle.Format = "0,###";
            dataGridViewBarang.Columns["HargaUnit"].DefaultCellStyle.Format = "0,###";
            dataGridViewBarang.Columns["HargaJual"].DefaultCellStyle.Format = "0,###";

            // agar user tidak bisa menambahkan data langsung di datagridview (harus melalui tombol Tambah)
            dataGridViewBarang.AllowUserToAddRows = false;
        }

        private void FormDaftarKartuStok_Load(object sender, EventArgs e)
        {
            FormatDataGrid();

            listHasilData.Clear();
            string hasilBaca = Barang.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                dataGridViewBarang.Rows.Clear();

                for (int i = 0; i < listHasilData.Count; i++)
                {
                    dataGridViewBarang.Rows.Add(listHasilData[i].Id, listHasilData[i].Nama,
                        listHasilData[i].Jenis, listHasilData[i].Stok, listHasilData[i].Satuan,
                        listHasilData[i].HargaBeli, (listHasilData[i].Stok * listHasilData[i].HargaBeli), listHasilData[i].HargaJual);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
