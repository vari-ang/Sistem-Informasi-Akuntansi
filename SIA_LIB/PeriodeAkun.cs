using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class PeriodeAkun
    {
        private Periode periode; // aggregation 
        private Akun akun; // aggregation
        private int saldoAwal;
        private int saldoAkhir;

        #region CONSTRUCTORS
        public PeriodeAkun()
        {    
            this.SaldoAwal = saldoAwal;
            this.SaldoAkhir = saldoAkhir;
        }

        public PeriodeAkun(Periode periode, Akun akun, int saldoAwal, int saldoAkhir)
        {
            this.Periode = periode;
            this.Akun = akun;
            this.SaldoAwal = saldoAwal;
            this.SaldoAkhir = saldoAkhir;
        }
        #endregion

        #region PROPERTIES
        public Periode Periode { get => periode; set => periode = value; }
        public Akun Akun { get => akun; set => akun = value; }
        public int SaldoAwal { get => saldoAwal; set => saldoAwal = value; }
        public int SaldoAkhir { get => saldoAkhir; set => saldoAkhir = value; }
        #endregion

        #region METHODS
        #endregion
    }
}
