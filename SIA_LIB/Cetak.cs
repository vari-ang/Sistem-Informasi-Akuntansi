using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// agar objek bertipe file stream dapat digunakan
using System.IO;

// agar objek bertipe Font dapat digunakan
using System.Drawing;

// agar objek bertipe PrintPageEventArgs dapat digunakan
using System.Drawing.Printing;

namespace SIA_LIB
{
    public class Cetak
    {
        private Font jenisFont;
        private StreamReader fileCetak;
        private float marginKiri, marginKanan, marginAtas, marginBawah;

        #region CONSTRUCTORS
        // untuk mencetak dengan format default
        public Cetak(string namaFile)
        {
            FileCetak = new StreamReader(namaFile);
            JenisFont = new Font("Arial", 12);
            MarginKiri = (float)10.5;
            MarginKanan = (float)10.5;
            MarginAtas = (float)10.5;
            MarginBawah = (float)10.5;
        }

        // untuk mencetak dengan format custom
        public Cetak(string pNamaFile, string pNamaFont, int pUkuranFont, float pMarginKiri, float pMarginKanan, float pMarginAtas, float pMarginBawah)
        {
            FileCetak = new StreamReader(pNamaFile);
            JenisFont = new Font(pNamaFont, pUkuranFont);
            MarginKiri = pMarginKiri;
            MarginKanan = pMarginKanan;
            MarginAtas = pMarginAtas;
            MarginBawah = pMarginBawah;
        }
        #endregion

        #region PROPERTIES
        public Font JenisFont
        {
            get { return jenisFont; }
            set { jenisFont = value; }
        }

        public StreamReader FileCetak
        {
            get { return fileCetak; }
            set { fileCetak = value; }
        }

        public float MarginBawah
        {
            get { return marginBawah; }
            set { marginBawah = value; }
        }

        public float MarginAtas
        {
            get { return marginAtas; }
            set { marginAtas = value; }
        }

        public float MarginKanan
        {
            get { return marginKanan; }
            set { marginKanan = value; }
        }

        public float MarginKiri
        {
            get { return marginKiri; }
            set { marginKiri = value; }
        }
        #endregion

        #region METHODS
        private void CetakTulisan(object sender, PrintPageEventArgs e)
        {
            // hitung jumlah baris maksimal yang dapat ditampilkan pada 1 halaman kertas
            int jumBarisPerHalaman = (int)((e.MarginBounds.Height - MarginBawah) / jenisFont.GetHeight(e.Graphics));

            // untuk menyimpan posisi y terakhir tulisan yang tercetak
            int jumBaris = 0;

            // untuk menyimpan tulisan yang akan dicetak
            string tulisanCetak = fileCetak.ReadLine();

            // Baca filestream untuk mencetak tiap baris tulisan
            while (jumBaris < jumBarisPerHalaman && tulisanCetak != null)
            {
                float y = MarginAtas + (jumBaris * jenisFont.GetHeight(e.Graphics));

                // cetak tulisan sesuai jenis fotn dan margin (warna tulisan hitam)
                e.Graphics.DrawString(tulisanCetak, JenisFont, Brushes.Black, MarginKiri, y);

                // jumlah baris tercetak ditambah 1
                jumBaris++;

                // baca baris file berikutnya
                tulisanCetak = FileCetak.ReadLine();
            }

            // jika masih belum selesai mencetak, cetak di halaman berikutnya
            if (tulisanCetak != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        public string CetakKePrinter(string pTipe)
        {
            try
            {
                // buat objek untuk mencetak
                PrintDocument p = new PrintDocument();

                if (pTipe == "tulisan") // jika tipe yg akan dicetak adalah text atau tulisan
                {
                    // tambahkan event handler untuk mencetak tulisan
                    p.PrintPage += new PrintPageEventHandler(CetakTulisan);
                }

                // cetak tulisan
                p.Print();

                FileCetak.Close();

                return "1";
            }
            catch (Exception exc)
            {
                return "Proses cetak gagal. Pesan = " + exc.Message;
            }
        }
        #endregion
    }
}
