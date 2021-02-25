using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class Transaksi
    {
        private string idTransaksi;
        private string keterangan;

        #region CONSTRUCTORS
        public Transaksi(string idTransaksi, string keterangan)
        {
            this.idTransaksi = idTransaksi;
            this.keterangan = keterangan;
        }
        #endregion

        #region PROPERTIES
        public string IdTransaksi { get => idTransaksi; set => idTransaksi = value; }
        public string Keterangan { get => keterangan; set => keterangan = value; }
        #endregion

        #region METHODS
        #endregion
    }
}
