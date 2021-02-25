CREATE VIEW vLaporanJurnal AS
SELECT j.jenis, j.id, j.tanggal, t.keterangan, a.nama, d.debet, d.kredit, j.nomor_bukti, id_periode, id_transaksi
FROM _jurnal j INNER JOIN _transaksi t ON j.id_transaksi = t.id
    INNER JOIN _detil_jurnal d ON j.id = d.id_jurnal
        INNER JOIN _akun a ON d.nomor_akun = a.nomor
            ORDER BY j.id ASC, d.no_urut ASC;

-- Pastikan isi tabel detiljurnal balance
-- select * from vlaporanjurnal;

-- Mendapatkan nominal akun kas
CREATE VIEW vSaldoAkhir AS
SELECT A.nomor, A.nama, A.kelompok, IFNULL(PA.saldo_awal, 0) + ((IFNULL(SUM(D.debet), 0) - IFNULL(SUM(D.kredit), 0)) * A.saldo_normal) AS TotalTransaksi, PA.id_periode
FROM _akun A LEFT JOIN _detil_jurnal D ON D.nomor_akun = A.nomor
	LEFT JOIN _periode_akun PA ON A.nomor = PA.nomor_akun
GROUP BY A.nomor, A.nama, PA.saldo_awal, PA.id_periode;

-- menampilkan laporan ekuitas
CREATE VIEW vLaporanPerubahanEkuitas AS
SELECT * FROM
vSaldoAkhir
WHERE kelompok IN ('EKUITAS');

-- Menampilkan jumlah eukitas
SELECT SUM(TotalTransaksi) FROM VLaporanPerubahanEkuitas;

-- menampilkan laporan neraca
CREATE VIEW vLaporanNeraca AS
SELECT * FROM
vSaldoAkhir
WHERE kelompok IN ('ASET', 'KEWAJIBAN', 'EKUITAS');

-- Menampilkan Total Pendapatan
SELECT SUM(TotalTransaksi) AS TotalPendapatan 
FROM vSaldoAkhir
WHERE kelompok = 'PENDAPATAN';

-- Menampilkan Total Biaya
SELECT SUM(TotalTransaksi) AS TotalBiaya
FROM vSaldoAkhir
WHERE kelompok = 'BIAYA';

SELECT * FROM vSaldoAkhir
WHERE kelompok IN ('PENDAPATAN','BIAYA');

-- Menampilkan Total Aktiva
SELECT SUM(TotalTransaksi) AS TotalAkitva
FROM vLaporanNeraca
WHERE kelompok = 'ASET';

-- Menampilkan Total Pasiva (ASET)
SELECT SUM(TotalTransaksi) + VARLabaRugi AS TotalPasiva
FROM vLaporanNeraca
WHERE kelompok IN ('KEWAJIBAN', 'EKUITAS');

/* TUTUP PERIODE */
-- 1. Update saldo akhir tiap akun di periode aktif
UPDATE _periode_akun PA
SET PA.saldo_akhir = (SELECT TotalTransaksi FROM vSaldoAkhir V WHERE V.nomor = PA.nomor_akun)
WHERE PA.id_periode = "1502"; -- Ganti dengan GetPeriodeTerbaru().idPeriode

-- 2. Posting jurnal penutup

-- Tambahkan akun khusus (untuk bantuan) Ihtisar Laba Rugi

-- 2.1 Penutupan pendapatan
INSERT INTO _jurnal(id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi)
VALUES('12', (SELECT CURDATE()), '-', 'JT', '1503', '901'); 
-- Ganti id jurnal dengan GetIdJurnalTerbaru(). Ganti dengan GetPeriodeTerbaru().idPeriode

-- Insert ke tabel _detil_jurnal
-- saldo akhir kredit
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '9', V.nomor, 1, V.TotalTransaksi, 0
FROM vSaldoAkhir V INNER JOIN _akun A ON V.nomor = A.nomor
WHERE V.kelompok = 'PENDAPATAN' AND A.saldo_normal = -1 AND id_periode = '1503';

-- saldo akhri debet
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '12', V.nomor, 1, 0, V.TotalTransaksi
FROM vSaldoAkhir V INNER JOIN _akun A ON V.nomor = A.nomor
WHERE V.kelompok = 'PENDAPATAN' AND A.saldo_normal = 1 AND id_periode = '1503';

-- Insert ithisar laba rgui (diasumsikan akun ihtisar laba rudi memiliki nomor 00)
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '10', '00', 1, 0, HitungTotalPendapatan();

-- 2.2 Penutupan biaya
INSERT INTO _jurnal(id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi)
VALUES('10', (SELECT CURDATE()), '-', 'JT', '1502', '902'); 
-- Ganti id jurnal dengan GetIdJurnalTerbaru(). Ganti dengan GetPeriodeTerbaru().idPeriode

-- Insert ke tabel _detil_jurnal

-- Insert biaya (umumnya semua biaya saldo normalnya di sisi debet)
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '11', V.nomor, 1, 0, V.TotalTransaksi
FROM vSaldoAkhir V INNER JOIN _akun A ON V.nomor = A.nomor
WHERE V.kelompok = 'BIAYA' AND A.saldo_normal = 1 AND id_periode = '1503';

-- insert ihtisar laba rugi :
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '11', '00', 1,  HitungTotalBiaya(), 0;

-- 2.3 Penutupan modal dan laba rugi

-- Insert ke tabel jurnal
INSERT INTO _jurnal(id, tanggal, nomor_bukti, jenis, id_periode, id_transaksi)
VALUES('12', (SELECT CURDATE()), '-', 'JT', '1502', '903'); 

-- Insert ke tabel _detil_jurnal

-- Insert ihtisar laba rugi:
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '12', '00', 1, HitungPendapatan() - HitungBiaya(), 0;

-- Insert modal
INSERT INTO _detil_jurnal(id_jurnal, nomor_akun, no_urut, debet, kredit)
SELECT '12', '31', 1, 0, HitungPendapatan() - HitungBiaya();

-- 2.4 Penutupan modal dan prive
-- Tidak dikerjakan pada project karena tidak ada akun prive

-- 3. Buat periode baru di tabel periode
INSERT INTO _periode (id, tgl_awal, tgl_akhir)
VALUES ('1503', tglAwal, tglAkhir);
-- Generate periode baru

-- 4. Tambah akun2 di periode akun (beserta saldo awal)
INSERT INTO _periode_akun(id_periode, nomor_akun, saldo_awal, saldo_akhir)
SELECT '1503', nomor, TotalTransaksi, 0
FROM vSaldoAkhir WHERE id_periode = '1503';
-- Generate periode baru