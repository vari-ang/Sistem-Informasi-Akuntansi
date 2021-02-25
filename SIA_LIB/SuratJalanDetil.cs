using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class SuratJalanDetil
    {
        private Barang barang; // aggregation
        private int jumlah;
        private int hpp;

        #region CONSTRUCTORS
        public SuratJalanDetil()
        {
            this.Jumlah = 1;
            this.HPP = 0;
        }
        public SuratJalanDetil(Barang pBarang, int pJumlah, int pHPP)
        {
            this.Barang = pBarang;
            this.Jumlah = pJumlah;
            this.HPP = pHPP;
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

        public int HPP
        {
            get { return hpp; }
            set { hpp = value; }
        }
        #endregion
    }
}
