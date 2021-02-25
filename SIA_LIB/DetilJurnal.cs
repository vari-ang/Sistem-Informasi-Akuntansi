using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class DetilJurnal
    {
        private Akun akun; // aggregation
        private int noUrut;
        private int debet;
        private int kredit;

        #region CONSTRUCTORS
        public DetilJurnal()
        {
            this.NoUrut = NoUrut;
            this.Debet = Debet;
            this.Kredit = Kredit;
        }

        public DetilJurnal(Akun akun, int noUrut, int debet, int kredit)
        {
            this.Akun = akun;
            this.NoUrut = noUrut;
            this.Debet = debet;
            this.Kredit = kredit;
        }
        #endregion

        #region PROPERTIES
        public Akun Akun { get => akun; set => akun = value; }
        public int NoUrut { get => noUrut; set => noUrut = value; }
        public int Debet { get => debet; set => debet = value; }
        public int Kredit { get => kredit; set => kredit = value; }
        #endregion

        #region METHODS
        #endregion
    }
}
