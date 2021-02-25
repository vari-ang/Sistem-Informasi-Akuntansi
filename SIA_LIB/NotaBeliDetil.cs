using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class NotaBeliDetil
    {
        private Barang barang; // aggregation
        private int jumlah;
        private int harga;

        #region CONSTRUCTORS
        public NotaBeliDetil()
        {
            this.Jumlah = 1;
            this.Harga = 0;
        }
        public NotaBeliDetil(Barang pBarang, int pJumlah, int pHarga)
        {
            this.Barang = pBarang;
            this.Jumlah = pJumlah;
            this.Harga = pHarga;
        }
        #endregion

        #region PROPERTIES
        public int Harga
        {
            get { return harga; }
            set { harga = value; }
        }
        public Barang Barang
        {
            get { return barang; }
            set { barang = value; }
        }
        public int Jumlah
        {
            get { return jumlah; }
            set { jumlah = value; }
        }
        #endregion
    }
}
