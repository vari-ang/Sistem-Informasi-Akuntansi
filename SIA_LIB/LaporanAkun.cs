using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class LaporanAkun
    {
        private Laporan laporan; // aggregation
        private Akun akun; // aggregation
        #region CONSTRUCTORS
        public LaporanAkun()
        {
           
        }

        public LaporanAkun(Laporan laporan, Akun akun)
        {
            this.Laporan = laporan;
            this.Akun = akun;
        }
        #endregion
        #region PROPERTIES
        public Laporan Laporan { get => laporan; set => laporan = value; }
        public Akun Akun { get => akun; set => akun = value; }
        #endregion
        #region METHODS
        #endregion
    }
}
