-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jun 27, 2019 at 06:27 AM
-- Server version: 5.7.17
-- PHP Version: 7.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `si_sia`
--

-- --------------------------------------------------------

--
-- Table structure for table `barang`
--

CREATE TABLE `barang` (
  `id` varchar(12) NOT NULL,
  `nama` varchar(45) DEFAULT NULL,
  `jenis` enum('BB','BJ','BP') DEFAULT NULL,
  `stok` int(11) DEFAULT NULL,
  `harga_beli` int(11) DEFAULT NULL,
  `harga_jual` int(11) DEFAULT NULL,
  `satuan` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `barang`
--

INSERT INTO `barang` (`id`, `nama`, `jenis`, `stok`, `harga_beli`, `harga_jual`, `satuan`) VALUES
('B001', 'Sapu', 'BJ', 200, 35000, 40000, 'buah'),
('B002', 'Ember', 'BJ', 1900, 11039, 40000, 'buah'),
('B003', 'Bijih Plastik', 'BB', 500, 5667, 0, 'kg'),
('B004', 'Pewarna', 'BB', 1100, 1167, 0, 'botol');

-- --------------------------------------------------------

--
-- Table structure for table `detil_nota_beli`
--

CREATE TABLE `detil_nota_beli` (
  `no_nota_beli` varchar(12) NOT NULL,
  `id_barang` varchar(12) NOT NULL,
  `jumlah` int(11) DEFAULT NULL,
  `harga_beli` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `detil_nota_beli`
--

INSERT INTO `detil_nota_beli` (`no_nota_beli`, `id_barang`, `jumlah`, `harga_beli`) VALUES
('NB001', 'B003', 1000, 6000),
('NB002', 'B004', 200, 2000);

-- --------------------------------------------------------

--
-- Table structure for table `detil_nota_jual`
--

CREATE TABLE `detil_nota_jual` (
  `nomor_nota_jual` varchar(12) NOT NULL,
  `id_barang` varchar(12) NOT NULL,
  `jumlah` int(11) DEFAULT NULL,
  `harga_jual` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `detil_nota_jual`
--

INSERT INTO `detil_nota_jual` (`nomor_nota_jual`, `id_barang`, `jumlah`, `harga_jual`) VALUES
('NJ001', 'B002', 200, 40000);

-- --------------------------------------------------------

--
-- Table structure for table `detil_penerimaan`
--

CREATE TABLE `detil_penerimaan` (
  `id_penerimaan_pembayaran` varchar(12) NOT NULL,
  `id_barang` varchar(12) NOT NULL,
  `jumlah` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `detil_surat_jalan`
--

CREATE TABLE `detil_surat_jalan` (
  `nomor_surat_jalan` varchar(12) NOT NULL,
  `id_barang` varchar(12) NOT NULL,
  `jumlah` int(11) DEFAULT NULL,
  `hpp` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `detil_surat_jalan`
--

INSERT INTO `detil_surat_jalan` (`nomor_surat_jalan`, `id_barang`, `jumlah`, `hpp`) VALUES
('SJ001', 'B003', 1000, 5667),
('SJ001', 'B004', 100, 1167),
('SJ002', 'B002', 2000, 30000);

-- --------------------------------------------------------

--
-- Table structure for table `detil_surat_permintaan`
--

CREATE TABLE `detil_surat_permintaan` (
  `id_barang` varchar(12) NOT NULL,
  `nomor_surat_permintaan` varchar(12) NOT NULL,
  `jumlah` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `detil_surat_permintaan`
--

INSERT INTO `detil_surat_permintaan` (`id_barang`, `nomor_surat_permintaan`, `jumlah`) VALUES
('B002', 'SP002', 2000),
('B003', 'SP001', 1000),
('B004', 'SP001', 100);

-- --------------------------------------------------------

--
-- Table structure for table `ekspedisi`
--

CREATE TABLE `ekspedisi` (
  `id` varchar(12) NOT NULL,
  `nama` varchar(45) DEFAULT NULL,
  `alamat` varchar(45) DEFAULT NULL,
  `tlp` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `ekspedisi`
--

INSERT INTO `ekspedisi` (`id`, `nama`, `alamat`, `tlp`) VALUES
('E001', 'JNE Express', 'Jl. Tenggilis 9', '08139086896'),
('E002', 'TIKI', 'Jl. Durian 102', '08240100227'),
('E003', 'Pos Indonesia', 'Jl. Bintang Emas 88', '081881432378');

-- --------------------------------------------------------

--
-- Table structure for table `job_order`
--

CREATE TABLE `job_order` (
  `nomor` varchar(12) NOT NULL,
  `tanggal_mulai` date DEFAULT NULL,
  `tanggal_selesai` date DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `direct_material` int(11) DEFAULT NULL,
  `direct_labor` int(11) DEFAULT NULL,
  `factory_overhead` int(11) DEFAULT NULL,
  `id_barang` varchar(12) NOT NULL,
  `nomor_nota_jual` varchar(12) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `job_order`
--

INSERT INTO `job_order` (`nomor`, `tanggal_mulai`, `tanggal_selesai`, `quantity`, `direct_material`, `direct_labor`, `factory_overhead`, `id_barang`, `nomor_nota_jual`) VALUES
('JO123', '2019-06-27', '2019-06-27', 2000, 5783700, 14400000, 0, 'B002', '-');

-- --------------------------------------------------------

--
-- Table structure for table `karyawan`
--

CREATE TABLE `karyawan` (
  `id` varchar(12) NOT NULL,
  `nama` varchar(45) DEFAULT NULL,
  `jenis_kelamin` enum('L','P') DEFAULT NULL,
  `gaji` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `karyawan`
--

INSERT INTO `karyawan` (`id`, `nama`, `jenis_kelamin`, `gaji`) VALUES
('K001', 'Budi', 'L', 1200000),
('K002', 'Agus', 'L', 1200000),
('K003', 'Andi', 'L', 1200000),
('K004', 'Joni', 'L', 1200000),
('K005', 'Indra', 'L', 1200000),
('K006', 'Yoshua', 'L', 1200000),
('K007', 'Kevin', 'L', 1200000),
('K008', 'Otniel', 'P', 1200000),
('K009', 'Batara', 'P', 1200000),
('K010', 'Billion', 'P', 1200000),
('K011', 'Rudy', 'L', 1200000),
('K012', 'Yudi', 'L', 1200000);

-- --------------------------------------------------------

--
-- Table structure for table `karyawan_has_job_order`
--

CREATE TABLE `karyawan_has_job_order` (
  `id_karyawan` varchar(12) NOT NULL,
  `nomor_job_order` varchar(12) NOT NULL,
  `jumlah` int(11) DEFAULT NULL,
  `satuan` varchar(45) DEFAULT NULL,
  `upah` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `karyawan_has_job_order`
--

INSERT INTO `karyawan_has_job_order` (`id_karyawan`, `nomor_job_order`, `jumlah`, `satuan`, `upah`) VALUES
('K001', 'JO123', 24, 'Jam', 50000),
('K002', 'JO123', 24, 'Jam', 50000),
('K003', 'JO123', 24, 'Jam', 50000),
('K004', 'JO123', 24, 'Jam', 50000),
('K005', 'JO123', 24, 'Jam', 50000),
('K006', 'JO123', 24, 'Jam', 50000),
('K007', 'JO123', 24, 'Jam', 50000),
('K008', 'JO123', 24, 'Jam', 50000),
('K009', 'JO123', 24, 'Jam', 50000),
('K010', 'JO123', 24, 'Jam', 50000),
('K011', 'JO123', 24, 'Jam', 50000),
('K012', 'JO123', 24, 'Jam', 50000);

-- --------------------------------------------------------

--
-- Table structure for table `nota_beli`
--

CREATE TABLE `nota_beli` (
  `nomor` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `harga_total` int(11) DEFAULT NULL,
  `tanggal_batas_pelunasan` datetime DEFAULT NULL,
  `tanggal_batas_diskon` datetime DEFAULT NULL,
  `diskon_pelunasan` double DEFAULT NULL,
  `status` enum('D','B') DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `id_supplier` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `nota_beli`
--

INSERT INTO `nota_beli` (`nomor`, `tanggal`, `harga_total`, `tanggal_batas_pelunasan`, `tanggal_batas_diskon`, `diskon_pelunasan`, `status`, `keterangan`, `id_supplier`) VALUES
('NB001', '2019-06-27 10:15:03', 6000000, '2019-06-30 10:15:03', '2019-06-27 10:15:03', 0, 'D', 'pembelian', 'S001'),
('NB002', '2019-06-27 10:15:58', 400000, '2019-07-27 10:15:58', '2019-07-06 10:15:58', 2, 'D', 'pembelian 2', 'S002');

-- --------------------------------------------------------

--
-- Table structure for table `nota_jual`
--

CREATE TABLE `nota_jual` (
  `nomor` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `harga_total` int(11) DEFAULT NULL,
  `tanggal_batas_pelunasan` datetime DEFAULT NULL,
  `tanggal_batas_diskon` datetime DEFAULT NULL,
  `diskon_pelunasan` int(11) DEFAULT NULL,
  `status` enum('D','B') DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `id_pelanggan` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `nota_jual`
--

INSERT INTO `nota_jual` (`nomor`, `tanggal`, `harga_total`, `tanggal_batas_pelunasan`, `tanggal_batas_diskon`, `diskon_pelunasan`, `status`, `keterangan`, `id_pelanggan`) VALUES
('NJ001', '2019-06-27 10:21:59', 8000000, '2019-06-27 10:21:59', '2019-06-27 10:21:59', 0, 'D', 'penjualan', 'P001');

-- --------------------------------------------------------

--
-- Table structure for table `pelanggan`
--

CREATE TABLE `pelanggan` (
  `id` varchar(12) NOT NULL,
  `nama` varchar(45) DEFAULT NULL,
  `alamat` varchar(45) DEFAULT NULL,
  `tlp` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pelanggan`
--

INSERT INTO `pelanggan` (`id`, `nama`, `alamat`, `tlp`) VALUES
('P001', 'Toko Laris', 'Jl. Mangga 9', '086833221008');

-- --------------------------------------------------------

--
-- Table structure for table `pembayaran`
--

CREATE TABLE `pembayaran` (
  `id` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `nominal` int(11) DEFAULT NULL,
  `metode_pembayaran` enum('K','T','TF') DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `nomor_nota_beli` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pembayaran`
--

INSERT INTO `pembayaran` (`id`, `tanggal`, `nominal`, `metode_pembayaran`, `keterangan`, `nomor_nota_beli`) VALUES
('P001', '2019-06-27 10:22:43', 6000000, 'T', 'pelunasan', 'NB001');

-- --------------------------------------------------------

--
-- Table structure for table `penerimaan_pembayaran`
--

CREATE TABLE `penerimaan_pembayaran` (
  `id` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `metode_pembayaran` enum('K','T') DEFAULT NULL,
  `nominal` int(11) DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `nomor_nota_jual` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `penerimaan_pembelian`
--

CREATE TABLE `penerimaan_pembelian` (
  `id` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `nomor_nota_beli` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `pengiriman`
--

CREATE TABLE `pengiriman` (
  `id` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `jenis_pengiriman` enum('DP','SP') DEFAULT NULL,
  `biaya` int(11) DEFAULT NULL,
  `metode_pembayaran` enum('T','K') DEFAULT NULL,
  `nomor_nota_jual` varchar(12) NOT NULL,
  `id_ekspedisi` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `supplier`
--

CREATE TABLE `supplier` (
  `id` varchar(12) NOT NULL,
  `nama` varchar(45) DEFAULT NULL,
  `alamat` varchar(45) DEFAULT NULL,
  `tlp` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `supplier`
--

INSERT INTO `supplier` (`id`, `nama`, `alamat`, `tlp`) VALUES
('S001', 'CV. Plastik Utama', 'Jl. Rungkut Jaya no2', '082233222766'),
('S002', 'CV. Warna Pelangi', 'Jl. Semolowaru No.3', '081213243243');

-- --------------------------------------------------------

--
-- Table structure for table `surat_jalan`
--

CREATE TABLE `surat_jalan` (
  `nomor` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `jenis` enum('Keluar','Masuk') DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `nomor_surat_permintaan` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `surat_jalan`
--

INSERT INTO `surat_jalan` (`nomor`, `tanggal`, `jenis`, `keterangan`, `nomor_surat_permintaan`) VALUES
('SJ001', '2019-06-27 10:18:46', 'Keluar', 'surat jalan', 'SP001'),
('SJ002', '2019-06-27 10:21:37', 'Keluar', 'Pembuatan surat jalan barang jadi ke gudang secara otomatis', 'SP002');

-- --------------------------------------------------------

--
-- Table structure for table `surat_permintaan`
--

CREATE TABLE `surat_permintaan` (
  `nomor` varchar(12) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `keterangan` varchar(250) DEFAULT NULL,
  `nomor_job_order` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `surat_permintaan`
--

INSERT INTO `surat_permintaan` (`nomor`, `tanggal`, `keterangan`, `nomor_job_order`) VALUES
('SP001', '2019-06-27 10:18:04', 'surat permintaan', 'JO123'),
('SP002', '2019-06-27 10:21:36', 'Pembuatan surat penerimaan barang jadi secara otomatis', 'JO123');

-- --------------------------------------------------------

--
-- Stand-in structure for view `vlaporanjurnal`
-- (See below for the actual view)
--
CREATE TABLE `vlaporanjurnal` (
`jenis` enum('JU','JK','JP','JT')
,`id` int(11)
,`tanggal` datetime
,`keterangan` varchar(250)
,`nama` varchar(45)
,`debet` int(11)
,`kredit` int(11)
,`nomor_bukti` varchar(12)
,`id_periode` varchar(5)
,`id_transaksi` varchar(5)
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vlaporanneraca`
-- (See below for the actual view)
--
CREATE TABLE `vlaporanneraca` (
`nomor` varchar(3)
,`nama` varchar(45)
,`kelompok` enum('aset','kewajiban','ekuitas','pendapatan','biaya')
,`TotalTransaksi` decimal(44,0)
,`id_periode` varchar(5)
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vlaporanperubahanekuitas`
-- (See below for the actual view)
--
CREATE TABLE `vlaporanperubahanekuitas` (
`nomor` varchar(3)
,`nama` varchar(45)
,`kelompok` enum('aset','kewajiban','ekuitas','pendapatan','biaya')
,`TotalTransaksi` decimal(44,0)
,`id_periode` varchar(5)
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vsaldoakhir`
-- (See below for the actual view)
--
CREATE TABLE `vsaldoakhir` (
`nomor` varchar(3)
,`nama` varchar(45)
,`kelompok` enum('aset','kewajiban','ekuitas','pendapatan','biaya')
,`TotalTransaksi` decimal(44,0)
,`id_periode` varchar(5)
);

-- --------------------------------------------------------

--
-- Table structure for table `_akun`
--

CREATE TABLE `_akun` (
  `nomor` varchar(3) NOT NULL,
  `nama` varchar(45) DEFAULT NULL,
  `kelompok` enum('aset','kewajiban','ekuitas','pendapatan','biaya') DEFAULT NULL,
  `saldo_normal` int(11) DEFAULT NULL COMMENT 'Diisi 1 atau -1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_akun`
--

INSERT INTO `_akun` (`nomor`, `nama`, `kelompok`, `saldo_normal`) VALUES
('00', 'Ihtisar Laba Rugi', NULL, -1),
('11', 'Kas', 'aset', 1),
('12', 'Piutang', 'aset', 1),
('13', 'Sediaan Bahan Baku', 'aset', 1),
('14', 'WIP', 'aset', 1),
('15', 'Sediaan Barang Jadi', 'aset', 1),
('21', 'Hutang', 'kewajiban', -1),
('22', 'Hutang Gaji', 'kewajiban', -1),
('31', 'Modal', 'ekuitas', -1),
('41', 'Penjualan', 'pendapatan', -1),
('42', 'Retur Penjualan & Penyesuaian Hrg', 'pendapatan', 1),
('43', 'Diskon Penjualan', 'pendapatan', 1),
('51', 'HPP', 'biaya', 1),
('52', 'Biaya Gaji', 'biaya', 1),
('53', 'Biaya Transportasi', 'biaya', 1);

-- --------------------------------------------------------

--
-- Table structure for table `_detil_jurnal`
--

CREATE TABLE `_detil_jurnal` (
  `id_jurnal` int(11) NOT NULL,
  `nomor_akun` varchar(3) NOT NULL,
  `no_urut` int(11) DEFAULT NULL,
  `debet` int(11) DEFAULT NULL,
  `kredit` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_detil_jurnal`
--

INSERT INTO `_detil_jurnal` (`id_jurnal`, `nomor_akun`, `no_urut`, `debet`, `kredit`) VALUES
(1, '13', 1, 6000000, 0),
(1, '21', 2, 0, 6000000),
(2, '13', 1, 400000, 0),
(2, '21', 2, 0, 400000),
(3, '13', 2, 0, 5783700),
(3, '14', 1, 5783700, 0),
(4, '14', 1, 14400000, 0),
(4, '22', 2, 0, 14400000),
(5, '11', 2, 0, 14400000),
(5, '22', 1, 14400000, 0),
(6, '14', 2, 0, 20183700),
(6, '15', 1, 20183700, 0),
(7, '11', 1, 8000000, 0),
(7, '15', 4, 0, 2207800),
(7, '41', 2, 0, 8000000),
(7, '51', 3, 2207800, 0),
(8, '11', 2, 0, 6000000),
(8, '13', 3, 0, 0),
(8, '21', 1, 6000000, 0),
(9, '00', 1, 0, 8000000),
(9, '41', 1, 8000000, 0),
(9, '42', 1, 0, 0),
(10, '00', 1, 2207800, 0),
(10, '51', 1, 0, 2207800),
(10, '52', 1, 0, 0),
(10, '53', 1, 0, 0),
(11, '00', 1, 5792200, 0),
(11, '31', 1, 0, 5792200),
(12, '00', 1, 0, 0),
(12, '41', 1, 0, 0),
(12, '42', 1, 0, 0),
(13, '00', 1, 0, 0),
(13, '51', 1, 0, 0),
(13, '52', 1, 0, 0),
(13, '53', 1, 0, 0),
(14, '00', 1, 0, 0),
(14, '31', 1, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `_jurnal`
--

CREATE TABLE `_jurnal` (
  `id` int(11) NOT NULL,
  `tanggal` datetime DEFAULT NULL,
  `nomor_bukti` varchar(12) DEFAULT NULL,
  `jenis` enum('JU','JK','JP','JT') DEFAULT NULL,
  `id_periode` varchar(5) NOT NULL,
  `id_transaksi` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_jurnal`
--

INSERT INTO `_jurnal` (`id`, `tanggal`, `nomor_bukti`, `jenis`, `id_periode`, `id_transaksi`) VALUES
(1, '2019-06-27 10:15:03', 'NB001', 'JU', '1502', '00001'),
(2, '2019-06-27 10:15:58', 'NB002', 'JU', '1502', '00001'),
(3, '2019-06-27 10:19:33', 'SJ001', 'JU', '1502', '00004'),
(4, '2019-06-27 10:20:40', 'JO123', 'JU', '1502', '00005'),
(5, '2019-06-27 10:21:00', 'JO123', 'JU', '1502', '00006'),
(6, '2019-06-27 10:21:30', 'JO123', 'JU', '1502', '00007'),
(7, '2019-06-27 10:21:59', 'NJ001', 'JU', '1502', '00008'),
(8, '2019-06-27 10:22:43', 'NB001', 'JU', '1502', '00009'),
(9, '2019-06-27 00:00:00', '-', 'JT', '1502', '901'),
(10, '2019-06-27 00:00:00', '-', 'JT', '1502', '902'),
(11, '2019-06-27 00:00:00', '-', 'JT', '1502', '903'),
(12, '2019-06-27 00:00:00', '-', 'JT', '1503', '901'),
(13, '2019-06-27 00:00:00', '-', 'JT', '1503', '902'),
(14, '2019-06-27 00:00:00', '-', 'JT', '1503', '903');

-- --------------------------------------------------------

--
-- Table structure for table `_laporan`
--

CREATE TABLE `_laporan` (
  `id` varchar(2) NOT NULL,
  `judul` varchar(45) DEFAULT NULL,
  `id_periode` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_laporan`
--

INSERT INTO `_laporan` (`id`, `judul`, `id_periode`) VALUES
('L1', 'Laporan Laba Rugi PT Bagoes Bangets', '1501'),
('L2', 'Laporan Laba Rugi PT Bagoes Bangets', '1502'),
('N1', 'Laporan Neraca PT Bagoes Bangets', '1501'),
('N2', 'Laporan Neraca PT Bagoes Bangets', '1502'),
('P1', 'Laporan Perubahan Ekuitas PT Bagoes Bangets', '1501'),
('P2', 'Laporan Perubahan Ekuitas PT Bagoes Bangets', '1502');

-- --------------------------------------------------------

--
-- Table structure for table `_laporan_akun`
--

CREATE TABLE `_laporan_akun` (
  `nomor_akun` varchar(3) NOT NULL,
  `id_laporan` varchar(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_laporan_akun`
--

INSERT INTO `_laporan_akun` (`nomor_akun`, `id_laporan`) VALUES
('41', 'L1'),
('42', 'L1'),
('43', 'L1'),
('51', 'L1'),
('52', 'L1'),
('53', 'L1'),
('11', 'N1'),
('12', 'N1'),
('13', 'N1'),
('14', 'N1'),
('15', 'N1'),
('21', 'N1'),
('22', 'N1'),
('31', 'N1'),
('11', 'N2'),
('12', 'N2'),
('13', 'N2'),
('14', 'N2'),
('15', 'N2'),
('21', 'N2'),
('22', 'N2'),
('31', 'N2'),
('31', 'P1'),
('31', 'P2');

-- --------------------------------------------------------

--
-- Table structure for table `_periode`
--

CREATE TABLE `_periode` (
  `id` varchar(5) NOT NULL,
  `tgl_awal` date DEFAULT NULL,
  `tgl_akhir` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_periode`
--

INSERT INTO `_periode` (`id`, `tgl_awal`, `tgl_akhir`) VALUES
('1501', '2015-01-01', '2015-01-31'),
('1502', '2015-02-01', '2015-01-28'),
('1503', '2019-06-27', '2019-07-27'),
('1504', '2019-06-27', '2019-07-27');

-- --------------------------------------------------------

--
-- Table structure for table `_periode_akun`
--

CREATE TABLE `_periode_akun` (
  `nomor_akun` varchar(3) NOT NULL,
  `id_periode` varchar(5) NOT NULL,
  `saldo_awal` bigint(20) DEFAULT NULL,
  `saldo_akhir` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_periode_akun`
--

INSERT INTO `_periode_akun` (`nomor_akun`, `id_periode`, `saldo_awal`, `saldo_akhir`) VALUES
('11', '1502', 30000000, 17600000),
('11', '1503', 17600000, 5200000),
('11', '1504', 5200000, 0),
('12', '1502', 20000000, 20000000),
('12', '1503', 20000000, 20000000),
('12', '1504', 20000000, 0),
('13', '1502', 3500000, 4116300),
('13', '1503', 4116300, 4732600),
('13', '1504', 4732600, 0),
('14', '1502', 0, 0),
('14', '1503', 0, 0),
('14', '1504', 0, 0),
('15', '1502', 10000000, 27975900),
('15', '1503', 27975900, 45951800),
('15', '1504', 45951800, 0),
('21', '1502', 13500000, 13900000),
('21', '1503', 13900000, 14300000),
('21', '1504', 14300000, 0),
('22', '1502', 0, 0),
('22', '1503', 0, 0),
('22', '1504', 0, 0),
('31', '1502', 50000000, 50000000),
('31', '1503', 55792200, 61584400),
('31', '1504', 61584400, 0),
('41', '1502', 0, 8000000),
('41', '1503', 0, 0),
('41', '1504', 0, 0),
('42', '1502', 0, 0),
('42', '1503', 0, 0),
('42', '1504', 0, 0),
('51', '1502', 0, 2207800),
('51', '1503', 0, 0),
('51', '1504', 0, 0),
('52', '1502', 0, 0),
('52', '1503', 0, 0),
('52', '1504', 0, 0),
('53', '1502', 0, 0),
('53', '1503', 0, 0),
('53', '1504', 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `_transaksi`
--

CREATE TABLE `_transaksi` (
  `id` varchar(5) NOT NULL,
  `keterangan` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `_transaksi`
--

INSERT INTO `_transaksi` (`id`, `keterangan`) VALUES
('00001', 'Membeli bahan baku secara kredit , FOB Destination Point, n/eom'),
('00002', 'Membeli bahan baku secara kredit , FOB Destination Point, 2/15, n/30'),
('00003', 'Membuat Job Order nomor 123'),
('00004', 'PPIC menerima bahan baku dari gudang atau gudang mengeluarkan bahan baku dan diterima PPIC'),
('00005', 'Menghitung dan membebankan biaya tenaga kerja langsung terhadap Job Order no 123'),
('00006', 'Membayar biaya / gaji tenaga kerja langsung secara tunai'),
('00007', 'Menyelesaikan job order no 123 dan mengirimkan barang jadi ke gudang'),
('00008', 'Menjual barang jadi secara tunai'),
('00009', 'Melunasi hutang secara tunai'),
('901', 'Penutupan Pendapatan'),
('902', 'Penutupan Biaya'),
('903', 'Penutupan Modal dan Laba Rugi'),
('904', 'Penutupan Modal dan Prive');

-- --------------------------------------------------------

--
-- Structure for view `vlaporanjurnal`
--
DROP TABLE IF EXISTS `vlaporanjurnal`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vlaporanjurnal`  AS  select `j`.`jenis` AS `jenis`,`j`.`id` AS `id`,`j`.`tanggal` AS `tanggal`,`t`.`keterangan` AS `keterangan`,`a`.`nama` AS `nama`,`d`.`debet` AS `debet`,`d`.`kredit` AS `kredit`,`j`.`nomor_bukti` AS `nomor_bukti`,`j`.`id_periode` AS `id_periode`,`j`.`id_transaksi` AS `id_transaksi` from (((`_jurnal` `j` join `_transaksi` `t` on((`j`.`id_transaksi` = `t`.`id`))) join `_detil_jurnal` `d` on((`j`.`id` = `d`.`id_jurnal`))) join `_akun` `a` on((`d`.`nomor_akun` = `a`.`nomor`))) order by `j`.`id`,`d`.`no_urut` ;

-- --------------------------------------------------------

--
-- Structure for view `vlaporanneraca`
--
DROP TABLE IF EXISTS `vlaporanneraca`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vlaporanneraca`  AS  select `vsaldoakhir`.`nomor` AS `nomor`,`vsaldoakhir`.`nama` AS `nama`,`vsaldoakhir`.`kelompok` AS `kelompok`,`vsaldoakhir`.`TotalTransaksi` AS `TotalTransaksi`,`vsaldoakhir`.`id_periode` AS `id_periode` from `vsaldoakhir` where (`vsaldoakhir`.`kelompok` in ('ASET','KEWAJIBAN','EKUITAS')) ;

-- --------------------------------------------------------

--
-- Structure for view `vlaporanperubahanekuitas`
--
DROP TABLE IF EXISTS `vlaporanperubahanekuitas`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vlaporanperubahanekuitas`  AS  select `vsaldoakhir`.`nomor` AS `nomor`,`vsaldoakhir`.`nama` AS `nama`,`vsaldoakhir`.`kelompok` AS `kelompok`,`vsaldoakhir`.`TotalTransaksi` AS `TotalTransaksi`,`vsaldoakhir`.`id_periode` AS `id_periode` from `vsaldoakhir` where (`vsaldoakhir`.`kelompok` = 'EKUITAS') ;

-- --------------------------------------------------------

--
-- Structure for view `vsaldoakhir`
--
DROP TABLE IF EXISTS `vsaldoakhir`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vsaldoakhir`  AS  select `a`.`nomor` AS `nomor`,`a`.`nama` AS `nama`,`a`.`kelompok` AS `kelompok`,(ifnull(`pa`.`saldo_awal`,0) + ((ifnull(sum(`d`.`debet`),0) - ifnull(sum(`d`.`kredit`),0)) * `a`.`saldo_normal`)) AS `TotalTransaksi`,`pa`.`id_periode` AS `id_periode` from ((`_akun` `a` left join `_detil_jurnal` `d` on((`d`.`nomor_akun` = `a`.`nomor`))) left join `_periode_akun` `pa` on((`a`.`nomor` = `pa`.`nomor_akun`))) group by `a`.`nomor`,`a`.`nama`,`pa`.`saldo_awal`,`pa`.`id_periode` ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `detil_nota_beli`
--
ALTER TABLE `detil_nota_beli`
  ADD PRIMARY KEY (`no_nota_beli`,`id_barang`),
  ADD KEY `fk_barang_has_nota_beli_nota_beli1_idx` (`no_nota_beli`),
  ADD KEY `fk_barang_has_nota_beli_barang1_idx` (`id_barang`);

--
-- Indexes for table `detil_nota_jual`
--
ALTER TABLE `detil_nota_jual`
  ADD PRIMARY KEY (`nomor_nota_jual`,`id_barang`),
  ADD KEY `fk_barang_has_nota_jual_nota_jual1_idx` (`nomor_nota_jual`),
  ADD KEY `fk_barang_has_nota_jual_barang1_idx` (`id_barang`);

--
-- Indexes for table `detil_penerimaan`
--
ALTER TABLE `detil_penerimaan`
  ADD PRIMARY KEY (`id_penerimaan_pembayaran`,`id_barang`),
  ADD KEY `fk_penerimaan_pembayaran_has_barang_barang1_idx` (`id_barang`),
  ADD KEY `fk_penerimaan_pembayaran_has_barang_penerimaan_pembayaran1_idx` (`id_penerimaan_pembayaran`);

--
-- Indexes for table `detil_surat_jalan`
--
ALTER TABLE `detil_surat_jalan`
  ADD PRIMARY KEY (`nomor_surat_jalan`,`id_barang`),
  ADD KEY `fk_surat_jalan_has_barang_barang1_idx` (`id_barang`),
  ADD KEY `fk_surat_jalan_has_barang_surat_jalan1_idx` (`nomor_surat_jalan`);

--
-- Indexes for table `detil_surat_permintaan`
--
ALTER TABLE `detil_surat_permintaan`
  ADD PRIMARY KEY (`id_barang`,`nomor_surat_permintaan`),
  ADD KEY `fk_barang_has_surat_permintaan_barang1_idx` (`id_barang`),
  ADD KEY `fk_detil_surat_permintaan_surat_permintaan1_idx` (`nomor_surat_permintaan`);

--
-- Indexes for table `ekspedisi`
--
ALTER TABLE `ekspedisi`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `job_order`
--
ALTER TABLE `job_order`
  ADD PRIMARY KEY (`nomor`),
  ADD KEY `fk_job_order_barang1_idx` (`id_barang`);

--
-- Indexes for table `karyawan`
--
ALTER TABLE `karyawan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `karyawan_has_job_order`
--
ALTER TABLE `karyawan_has_job_order`
  ADD PRIMARY KEY (`id_karyawan`,`nomor_job_order`),
  ADD KEY `fk_karyawan_has_job_order_job_order1_idx` (`nomor_job_order`),
  ADD KEY `fk_karyawan_has_job_order_karyawan1_idx` (`id_karyawan`);

--
-- Indexes for table `nota_beli`
--
ALTER TABLE `nota_beli`
  ADD PRIMARY KEY (`nomor`),
  ADD KEY `fk_nota_beli_supplier1_idx` (`id_supplier`);

--
-- Indexes for table `nota_jual`
--
ALTER TABLE `nota_jual`
  ADD PRIMARY KEY (`nomor`),
  ADD KEY `fk_nota_jual_pelanggan1_idx` (`id_pelanggan`);

--
-- Indexes for table `pelanggan`
--
ALTER TABLE `pelanggan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pembayaran`
--
ALTER TABLE `pembayaran`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_pembayaran_beli_nota_beli1_idx` (`nomor_nota_beli`);

--
-- Indexes for table `penerimaan_pembayaran`
--
ALTER TABLE `penerimaan_pembayaran`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_pembayaran_jual_nota_jual1_idx` (`nomor_nota_jual`);

--
-- Indexes for table `penerimaan_pembelian`
--
ALTER TABLE `penerimaan_pembelian`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_penerimaan_pembayaran_nota_beli1_idx` (`nomor_nota_beli`);

--
-- Indexes for table `pengiriman`
--
ALTER TABLE `pengiriman`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_pengiriman_nota_jual1_idx` (`nomor_nota_jual`),
  ADD KEY `fk_pengiriman_ekspedisi1_idx` (`id_ekspedisi`);

--
-- Indexes for table `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `surat_jalan`
--
ALTER TABLE `surat_jalan`
  ADD PRIMARY KEY (`nomor`),
  ADD KEY `fk_surat_jalan_surat_permintaan1_idx` (`nomor_surat_permintaan`);

--
-- Indexes for table `surat_permintaan`
--
ALTER TABLE `surat_permintaan`
  ADD PRIMARY KEY (`nomor`),
  ADD KEY `fk_surat_permintaan_job_order1_idx` (`nomor_job_order`);

--
-- Indexes for table `_akun`
--
ALTER TABLE `_akun`
  ADD PRIMARY KEY (`nomor`);

--
-- Indexes for table `_detil_jurnal`
--
ALTER TABLE `_detil_jurnal`
  ADD PRIMARY KEY (`id_jurnal`,`nomor_akun`),
  ADD KEY `fk__akun_has__jurnal__jurnal1_idx` (`id_jurnal`),
  ADD KEY `fk__akun_has__jurnal__akun1_idx` (`nomor_akun`);

--
-- Indexes for table `_jurnal`
--
ALTER TABLE `_jurnal`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk__jurnal__periode1_idx` (`id_periode`),
  ADD KEY `fk__jurnal_transaksi1_idx` (`id_transaksi`);

--
-- Indexes for table `_laporan`
--
ALTER TABLE `_laporan`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk__laporan__periode1_idx` (`id_periode`);

--
-- Indexes for table `_laporan_akun`
--
ALTER TABLE `_laporan_akun`
  ADD PRIMARY KEY (`nomor_akun`,`id_laporan`),
  ADD KEY `fk__akun_has__laporan__laporan1_idx` (`id_laporan`),
  ADD KEY `fk__akun_has__laporan__akun1_idx` (`nomor_akun`);

--
-- Indexes for table `_periode`
--
ALTER TABLE `_periode`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `_periode_akun`
--
ALTER TABLE `_periode_akun`
  ADD PRIMARY KEY (`nomor_akun`,`id_periode`),
  ADD KEY `fk__akun_has__periode__periode1_idx` (`id_periode`),
  ADD KEY `fk__akun_has__periode__akun1_idx` (`nomor_akun`);

--
-- Indexes for table `_transaksi`
--
ALTER TABLE `_transaksi`
  ADD PRIMARY KEY (`id`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `detil_nota_beli`
--
ALTER TABLE `detil_nota_beli`
  ADD CONSTRAINT `fk_barang_has_nota_beli_barang1` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_barang_has_nota_beli_nota_beli1` FOREIGN KEY (`no_nota_beli`) REFERENCES `nota_beli` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `detil_nota_jual`
--
ALTER TABLE `detil_nota_jual`
  ADD CONSTRAINT `fk_barang_has_nota_jual_barang1` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_barang_has_nota_jual_nota_jual1` FOREIGN KEY (`nomor_nota_jual`) REFERENCES `nota_jual` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `detil_penerimaan`
--
ALTER TABLE `detil_penerimaan`
  ADD CONSTRAINT `fk_penerimaan_pembayaran_has_barang_barang1` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_penerimaan_pembayaran_has_barang_penerimaan_pembayaran1` FOREIGN KEY (`id_penerimaan_pembayaran`) REFERENCES `penerimaan_pembelian` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `detil_surat_jalan`
--
ALTER TABLE `detil_surat_jalan`
  ADD CONSTRAINT `fk_surat_jalan_has_barang_barang1` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_surat_jalan_has_barang_surat_jalan1` FOREIGN KEY (`nomor_surat_jalan`) REFERENCES `surat_jalan` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `detil_surat_permintaan`
--
ALTER TABLE `detil_surat_permintaan`
  ADD CONSTRAINT `fk_barang_has_surat_permintaan_barang1` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_detil_surat_permintaan_surat_permintaan1` FOREIGN KEY (`nomor_surat_permintaan`) REFERENCES `surat_permintaan` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `job_order`
--
ALTER TABLE `job_order`
  ADD CONSTRAINT `fk_job_order_barang1` FOREIGN KEY (`id_barang`) REFERENCES `barang` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `karyawan_has_job_order`
--
ALTER TABLE `karyawan_has_job_order`
  ADD CONSTRAINT `fk_karyawan_has_job_order_job_order1` FOREIGN KEY (`nomor_job_order`) REFERENCES `job_order` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_karyawan_has_job_order_karyawan1` FOREIGN KEY (`id_karyawan`) REFERENCES `karyawan` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `nota_beli`
--
ALTER TABLE `nota_beli`
  ADD CONSTRAINT `fk_nota_beli_supplier1` FOREIGN KEY (`id_supplier`) REFERENCES `supplier` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `nota_jual`
--
ALTER TABLE `nota_jual`
  ADD CONSTRAINT `fk_nota_jual_pelanggan1` FOREIGN KEY (`id_pelanggan`) REFERENCES `pelanggan` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `pembayaran`
--
ALTER TABLE `pembayaran`
  ADD CONSTRAINT `fk_pembayaran_beli_nota_beli1` FOREIGN KEY (`nomor_nota_beli`) REFERENCES `nota_beli` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `penerimaan_pembayaran`
--
ALTER TABLE `penerimaan_pembayaran`
  ADD CONSTRAINT `fk_pembayaran_jual_nota_jual1` FOREIGN KEY (`nomor_nota_jual`) REFERENCES `nota_jual` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `penerimaan_pembelian`
--
ALTER TABLE `penerimaan_pembelian`
  ADD CONSTRAINT `fk_penerimaan_pembayaran_nota_beli1` FOREIGN KEY (`nomor_nota_beli`) REFERENCES `nota_beli` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `pengiriman`
--
ALTER TABLE `pengiriman`
  ADD CONSTRAINT `fk_pengiriman_ekspedisi1` FOREIGN KEY (`id_ekspedisi`) REFERENCES `ekspedisi` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_pengiriman_nota_jual1` FOREIGN KEY (`nomor_nota_jual`) REFERENCES `nota_jual` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `surat_jalan`
--
ALTER TABLE `surat_jalan`
  ADD CONSTRAINT `fk_surat_jalan_surat_permintaan1` FOREIGN KEY (`nomor_surat_permintaan`) REFERENCES `surat_permintaan` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `surat_permintaan`
--
ALTER TABLE `surat_permintaan`
  ADD CONSTRAINT `fk_surat_permintaan_job_order1` FOREIGN KEY (`nomor_job_order`) REFERENCES `job_order` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `_detil_jurnal`
--
ALTER TABLE `_detil_jurnal`
  ADD CONSTRAINT `fk__akun_has__jurnal__akun1` FOREIGN KEY (`nomor_akun`) REFERENCES `_akun` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk__akun_has__jurnal__jurnal1` FOREIGN KEY (`id_jurnal`) REFERENCES `_jurnal` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `_jurnal`
--
ALTER TABLE `_jurnal`
  ADD CONSTRAINT `fk__jurnal__periode1` FOREIGN KEY (`id_periode`) REFERENCES `_periode` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk__jurnal_transaksi1` FOREIGN KEY (`id_transaksi`) REFERENCES `_transaksi` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `_laporan`
--
ALTER TABLE `_laporan`
  ADD CONSTRAINT `fk__laporan__periode1` FOREIGN KEY (`id_periode`) REFERENCES `_periode` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `_laporan_akun`
--
ALTER TABLE `_laporan_akun`
  ADD CONSTRAINT `fk__akun_has__laporan__akun1` FOREIGN KEY (`nomor_akun`) REFERENCES `_akun` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk__akun_has__laporan__laporan1` FOREIGN KEY (`id_laporan`) REFERENCES `_laporan` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `_periode_akun`
--
ALTER TABLE `_periode_akun`
  ADD CONSTRAINT `fk__akun_has__periode__akun1` FOREIGN KEY (`nomor_akun`) REFERENCES `_akun` (`nomor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk__akun_has__periode__periode1` FOREIGN KEY (`id_periode`) REFERENCES `_periode` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
