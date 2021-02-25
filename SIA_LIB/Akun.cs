using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_LIB
{
    public class Akun
    {
        // Data Member
        private string nomor;
        private string nama;
        private string kelompok;
        private int saldoNormal;

        #region CONSTRUCTORS
        public Akun()
        {
            this.Nomor = "";
            this.Nama = "";
            this.Kelompok = "";
            this.SaldoNormal = 0;
        }

        public Akun(string nama, string kelompok)
        {
            this.nama = nama;
            this.kelompok = kelompok;
            
        }

        public Akun(string pNomor, string pNama, string pKelompok, int pSaldoNormal)
        {
            this.Nomor = pNomor;
            this.Nama = pNama;
            this.Kelompok = pKelompok;
            this.SaldoNormal = pSaldoNormal;
        }
        #endregion

        #region PROPERTIES
        public string Nomor { get => nomor; set => nomor = value; }
        public string Nama { get => nama; set => nama = value; }    
        public string Kelompok { get => kelompok; set => kelompok = value; }
        public int SaldoNormal { get => saldoNormal; set => saldoNormal = value; }
        #endregion

        #region METHODS
        #endregion
    }
}
