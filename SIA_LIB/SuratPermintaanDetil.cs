using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class SuratPermintaanDetil
    {
        private Barang barang; // aggregation
        private int jumlah;

        #region CONSTRUCTORS
        public SuratPermintaanDetil()
        {
            this.Jumlah = 1;
        }
        public SuratPermintaanDetil(Barang pBarang, int pJumlah)
        {
            this.Barang = pBarang;
            this.Jumlah = pJumlah;
        }
        #endregion

        #region PROPERTIES
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
