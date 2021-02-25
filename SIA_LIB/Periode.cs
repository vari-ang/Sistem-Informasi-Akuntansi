using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Transactions;

namespace SIA_LIB
{
    public class Periode
    {
        // Data Member
        private string id;
        private DateTime tglAwal;
        private DateTime tglAkhir;

        #region CONSTRUCTORS
        public Periode()
        {
            this.Id = "";
            this.TglAwal = DateTime.Now;
            this.TglAkhir = DateTime.Now;
        }

        public Periode(string id, DateTime tglAwal, DateTime tglAkhir)
        {
            this.Id = id;
            this.TglAwal = tglAwal;
            this.TglAkhir = tglAkhir;
        }
        #endregion

        #region PROPERTIES
        public string Id { get => id; set => id = value; }
        public DateTime TglAwal { get => tglAwal; set => tglAwal = value; }
        public DateTime TglAkhir { get => tglAkhir; set => tglAkhir = value; }
        #endregion

        #region METHODS
        public static Periode GetPeriodeTerbaru()
        {
            // Ambil dari tabel periode
            string sql = "SELECT * FROM _periode " +
                         "WHERE id = (SELECT MAX(id) FROM _periode)";
            
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            Periode hasilPeriode = null;

            if(hasil.Read() == true) // jika ada data
            {
                string idPeriode = hasil.GetValue(0).ToString();
                DateTime tglAwal = DateTime.Parse(hasil.GetValue(1).ToString());
                DateTime tglAkhir = DateTime.Parse(hasil.GetValue(2).ToString());

                hasilPeriode = new Periode(idPeriode, tglAwal, tglAkhir);
            }

            return hasilPeriode;
        }

        public static string TutupPeriode(int pIdJurnalTerbaru, int pIdPeriodeTerakhir, int pTotalPendapatan, int pTotalBiaya, DateTime pTglAwal, DateTime pTglAkhir)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    //  Update saldo akhir tiap akun di periode aktif
                    string sql0 = "UPDATE _periode_akun PA " +
                                  "SET PA.saldo_akhir = (SELECT TotalTransaksi FROM vSaldoAkhir V WHERE V.nomor = PA.nomor_akun AND id_periode = '" + pIdPeriodeTerakhir + "') " +
                                  "WHERE PA.id_periode = '" + pIdPeriodeTerakhir + "'";
                    Koneksi.JalankanPerintahDML(sql0);

                    // PENUTUPAN PENDAPATAN

                    string sql1 = "INSERT INTO _jurnal(id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi) " +
                             "VALUES('" + pIdJurnalTerbaru + "', (SELECT CURDATE()), '-', 'JT', '" + pIdPeriodeTerakhir + "', '901'); ";
                    Koneksi.JalankanPerintahDML(sql1);

                    // Insert ke tabel _detil_jurnal (saldo akhir kredit)
                    string sql2 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + pIdJurnalTerbaru + "', V.nomor, 1, V.TotalTransaksi, 0 " +
                                  "FROM vSaldoAkhir V INNER JOIN _akun A ON V.nomor = A.nomor " +
                                  "WHERE V.kelompok = 'PENDAPATAN' AND A.saldo_normal = -1 AND id_periode = '" + pIdPeriodeTerakhir + "'";
                    Koneksi.JalankanPerintahDML(sql2);

                    // Insert ke tabel _detil_jurnal (saldo akhir debet)
                    string sql3 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + pIdJurnalTerbaru + "', V.nomor, 1, 0, V.TotalTransaksi " +
                                  "FROM vSaldoAkhir V INNER JOIN _akun A ON V.nomor = A.nomor " +
                                  "WHERE V.kelompok = 'PENDAPATAN' AND A.saldo_normal = 1 AND id_periode = '" + pIdPeriodeTerakhir + "'";
                    Koneksi.JalankanPerintahDML(sql3);

                    // Insert ithisar laba rgui (diasumsikan akun ihtisar laba rudi memiliki nomor 00)
                    string sql4 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + pIdJurnalTerbaru + "', '00', 1, 0, " + pTotalPendapatan;
                    Koneksi.JalankanPerintahDML(sql4);

                    // PENUTUPAN BIAYA

                    string sql5 = "INSERT INTO _jurnal(id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi) " +
                                 "VALUES('" + (pIdJurnalTerbaru + 1) + "', (SELECT CURDATE()), '-', 'JT', '" + pIdPeriodeTerakhir + "', '902')";
                    Koneksi.JalankanPerintahDML(sql5);

                    // Insert ke tabel _detil_jurnal (umumnya semua biaya saldo normalnya di sisi debet)
                    string sql6 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + (pIdJurnalTerbaru + 1) + "', V.nomor, 1, 0, V.TotalTransaksi " +
                                  "FROM vSaldoAkhir V INNER JOIN _akun A ON V.nomor = A.nomor " +
                                  "WHERE V.kelompok = 'BIAYA' AND A.saldo_normal = 1 AND id_periode = '" + pIdPeriodeTerakhir + "'";
                    Koneksi.JalankanPerintahDML(sql6);

                    // Insert ithisar laba rugi (diasumsikan akun ihtisar laba rudi memiliki nomor 00)
                    string sql7 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + (pIdJurnalTerbaru + 1) + "', '00', 1, " + pTotalBiaya + ", 0";
                    Koneksi.JalankanPerintahDML(sql7);

                    // PENUTUPAN MODAL DAN LABA RUGI

                    string sql8 = "INSERT INTO _jurnal(id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi) " +
                                  "VALUES('" + (pIdJurnalTerbaru + 2) + "', (SELECT CURDATE()), '-', 'JT', '" + pIdPeriodeTerakhir + "', '903')";
                    Koneksi.JalankanPerintahDML(sql8);

                    // Insert ke tabel _detil_jurnal

                    // Insert ithisar laba rugi
                    string sql9 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + (pIdJurnalTerbaru + 2) + "', '00', 1, " + (pTotalPendapatan - pTotalBiaya) + ", 0";
                    Koneksi.JalankanPerintahDML(sql9);

                    // Insert Modal
                    string sql10 = "INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit) " +
                                  "SELECT '" + (pIdJurnalTerbaru + 2) + "', '31', 1, 0, " + (pTotalPendapatan - pTotalBiaya);
                    Koneksi.JalankanPerintahDML(sql10);

                    // xxx PENUTUPAN MODAL DAN PRIVE (TIDAK PERLU DIKERJAKAN) xxx

                    // BUAT PERIODE BARU DI TABEL PERIODE
                    string sql11 = "INSERT INTO _periode (id, tgl_awal, tgl_akhir) " +
                                   "VALUES('" + (pIdPeriodeTerakhir + 1) + "', '" + pTglAwal.ToString("yyyy-MM-dd hh:mm:ss") + "', '" + pTglAkhir.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                    Koneksi.JalankanPerintahDML(sql11);

                    // Tambah akun2 di periode akun (beserta saldo awal)
                    string sql12 = "INSERT INTO _periode_akun(id_periode, nomor_akun, saldo_awal, saldo_akhir) " +
                                   "SELECT '" + (pIdPeriodeTerakhir + 1) + "', nomor, TotalTransaksi, 0 " +
                                   "FROM vSaldoAkhir WHERE id_periode = '" + pIdPeriodeTerakhir + "'";
                    Koneksi.JalankanPerintahDML(sql12);

                    // jika semua perintah DML berhasil dijalankan
                    tranScope.Complete();

                    return "1";
                }
                catch (Exception exc)
                {
                    tranScope.Dispose();

                    return exc.Message;
                }
            }
        }
        #endregion
    }
}
