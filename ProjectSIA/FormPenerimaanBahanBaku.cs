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
    public partial class FormPenerimaanBahanBaku : Form
    {
        FormUtama frmUtama;
        Periode periode;
        List<SuratJalan> listSJ = new List<SuratJalan>();

        public FormPenerimaanBahanBaku()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatDataGrid()
        {
            dataGridViewBarang.Columns.Clear();

            dataGridViewBarang.Columns.Add("IdBarang", "Id Barang");
            dataGridViewBarang.Columns.Add("NamaBarang", "Nama Barang");
            dataGridViewBarang.Columns.Add("JenisBarang", "Jenis Barang");
            dataGridViewBarang.Columns.Add("JumlahBarang", "Jumlah Barang");
            dataGridViewBarang.Columns.Add("HPP", "HPP");

            dataGridViewBarang.Columns["IdBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["NamaBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["JenisBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["JumlahBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["HPP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridViewBarang.Columns["HPP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void FormPenerimaanBahanBaku_Load(object sender, EventArgs e)
        {
            try
            {
                frmUtama = (FormUtama)this.Owner;
                periode = frmUtama.periode;
                textBoxKeterangan.Enabled = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textBoxNoSuratJalan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxKeterangan.Enabled = false;

                if (textBoxNoSuratJalan.Text.Length == textBoxNoSuratJalan.MaxLength)
                {
                    textBoxKeterangan.Enabled = true;

                    FormatDataGrid();
                    listSJ.Clear();
                    dataGridViewBarang.Rows.Clear();

                    // car nama barang sesuai barcode yang diinputkan user
                    string hasil = SuratJalan.BacaData("nomor", textBoxNoSuratJalan.Text, listSJ);

                    if (hasil == "1")
                    {
                        if (listSJ.Count > 0)
                        {
                            for (int i = 0; i < listSJ.Count; i++)
                            {
                                for (int j = 0; j < listSJ[i].ListSuratJalanDetil.Count; j++)
                                {
                                    dataGridViewBarang.Rows.Add(listSJ[i].ListSuratJalanDetil[j].Barang.Id,
                                       listSJ[i].ListSuratJalanDetil[j].Barang.Nama, listSJ[i].ListSuratJalanDetil[j].Barang.Jenis,
                                       listSJ[i].ListSuratJalanDetil[j].Jumlah, listSJ[i].ListSuratJalanDetil[j].HPP);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Surat Jalan tidak ditemukan.");
                            dataGridViewBarang.Rows.Clear();
                        }
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
            DialogResult pesan = MessageBox.Show("Surat jalan - " + textBoxNoSuratJalan.Text + " akan diterima. Apakah Anda yakin ingin melanjutkan?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pesan == DialogResult.Yes)
            {
                try
                {
                    int totalBiaya = 0;
                    string baca = JobOrder.TampilTotalDirectMaterial("nomor_surat_jalan", textBoxNoSuratJalan.Text, out totalBiaya);

                    string noJobOrder = "";
                    string haca = JobOrder.TampilNoJobOrder(textBoxNoSuratJalan.Text, out noJobOrder);
                   
                    // UPDATE DIRECT MATERIAL DI JOB ORDER
                    string bacaUbahJobOrder = JobOrder.UbahData(noJobOrder, "direct_material", totalBiaya);
                    if (bacaUbahJobOrder == "1")
                    {
                        MessageBox.Show("Berhasil mengupdate data Direct Material pada Job Order " + noJobOrder);
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengupdate data Direct Labor pada Job Order " + noJobOrder + ". Pesan kesalahan: " + bacaUbahJobOrder);
                    }

                    // buat transaksi
                    string keterangan = "Penerimaan Surat Jalan - " + textBoxNoSuratJalan.Text + " : " + textBoxKeterangan.Text;
                    Transaksi t = new Transaksi("00004", keterangan);

                    // buat objek bertipe Jurnal
                    int idJurnalBaru = Jurnal.GetIdJurnalTerbaru(); // digenerate otomatis

                    Jurnal jurnal = new Jurnal();
                    jurnal.IdJurnal = idJurnalBaru;
                    jurnal.Tanggal = DateTime.Now;
                    jurnal.NomorBukti = textBoxNoSuratJalan.Text;
                    jurnal.Jenis = "JU";
                    jurnal.Periode = periode;
                    jurnal.Transaksi = t;

                    // hitung HPP dari dataGridViewBarang
                    for (int i = 0; i < dataGridViewBarang.Rows.Count - 1; i++)
                    {
                        string idBarang = dataGridViewBarang.Rows[i].Cells["IdBarang"].Value.ToString();
                        int jumlahBarang = int.Parse(dataGridViewBarang.Rows[i].Cells["JumlahBarang"].Value.ToString());

                        // KURANGI STOK BAHAN BAKU
                        string bacaUbahStok = Barang.UbahStokTerjual(idBarang, jumlahBarang);
                        if (bacaUbahStok == "1")
                        {
                            MessageBox.Show("Berhasil mengurangi stok bahan baku " + idBarang);
                        }
                        else
                        {
                            MessageBox.Show("Gagal mengurangi stok bahan baku " + idBarang + ". Pesan kesalahan: " + bacaUbahStok);
                        }
                    }

                    // tambahkan detil jurnal
                    jurnal.TambahDetilJurnalTrans000004(totalBiaya);                 

                    // simpan ke database
                    string hasilPostingJurnal = Jurnal.TambahData(jurnal);

                    if (hasilPostingJurnal == "1")
                    {
                        MessageBox.Show("Berhasil memposting ke jurnal", "Info");

                        // kosongi data
                        textBoxNoSuratJalan.Text = "";
                        textBoxKeterangan.Text = "";
                        textBoxKeterangan.Enabled = false;
                        dataGridViewBarang.Rows.Clear();
                        listSJ.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Gagal memposting ke jurnal. Pesan kesalahan: " + hasilPostingJurnal, "Kesalahan");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
