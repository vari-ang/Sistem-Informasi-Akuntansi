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
    public partial class FormUtama : Form
    {
        public Periode periode;

        public FormUtama()
        {
            InitializeComponent();
        }

        private void keluarSistemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        {
            periode = Periode.GetPeriodeTerbaru();

            // Ubah form ini (FormUtama) menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;

            // Ubah form utama menjadi MdiParen (MdiContainer)
            this.IsMdiContainer = true;
        }

        public int HitungPendapatan()
        {
            string baca = Laporan.TampilPendapatan();
            if (baca != "0")
            {
                return int.Parse(baca);
            }
            else
            {
                return 0;
            }
        }

        public int HitungBiaya()
        {
            string baca = Laporan.TampilBiaya();
            if (baca != "0")
            {
                return int.Parse(baca);
            }
            else
            {
                return 0;
            }
        }

        public int HitungLabaRugi()
        {
            return HitungPendapatan() - HitungBiaya();
        }

        private void jabatanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPembelian frm = new FormPembelian();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPenjualan frm = new FormPenjualan();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void penerimaanPembayaranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPembayaran frm = new FormPembayaran();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void suratPermintaanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormSuratPermintaan frm = new FormSuratPermintaan();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void bayarGajiKaryawanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPembayaranGajiTenagaKerja frm = new FormPembayaranGajiTenagaKerja();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void tutupPeriodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormJurnalPenutup frm = new FormJurnalPenutup();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemJurnal_Click(object sender, EventArgs e)
        {
            FormDaftarJurnal frm = new FormDaftarJurnal();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemPerubahanEkuitas_Click(object sender, EventArgs e)
        {
            FormDaftarEkuitas frm = new FormDaftarEkuitas();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemLabaRugi_Click(object sender, EventArgs e)
        {
            FormDaftarLabaRugi frm = new FormDaftarLabaRugi();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemNeraca_Click(object sender, EventArgs e)
        {
            FormDaftarNeraca frm = new FormDaftarNeraca();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemKartuStok_Click(object sender, EventArgs e)
        {
            FormDaftarKartuStok frm = new FormDaftarKartuStok();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void jobOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormJobOrder frm = new FormJobOrder();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemJobOrder_Click(object sender, EventArgs e)
        {
            FormDaftarJobOrder frm = new FormDaftarJobOrder();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemSuratPermintaan_Click(object sender, EventArgs e)
        {
            FormDaftarSuratPermintaan frm = new FormDaftarSuratPermintaan();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void penerimaanBahanBakuDariGudangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPenerimaanBahanBaku frm = new FormPenerimaanBahanBaku();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void perhitunganPembebananBiayaTenagaKerjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPerhitunganDanPembebananBiayaTenagaKerja frm = new FormPerhitunganDanPembebananBiayaTenagaKerja();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void penyelesaianJobOrderDanPengirimanBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPenyelesaianJobOrder frm = new FormPenyelesaianJobOrder();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void suratJalanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormSuratJalan frm = new FormSuratJalan();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void toolStripMenuItemSuratJalan_Click(object sender, EventArgs e)
        {
            FormDaftarSuratJalan frm = new FormDaftarSuratJalan();
            frm.Owner = this;
            frm.ShowDialog();
        }
    }
}
