namespace ProjectSIA
{
    partial class FormPerhitunganDanPembebananBiayaTenagaKerja
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSimpan = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxSatuanKerja = new System.Windows.Forms.ComboBox();
            this.numericUpDownRateGaji = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSatuanKerja = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxPilihSemuaTenagaKerja = new System.Windows.Forms.CheckBox();
            this.textBoxJenisKelaminTenagaKerja = new System.Windows.Forms.TextBox();
            this.buttonTambahTenagaKerja = new System.Windows.Forms.Button();
            this.dataGridViewTenagaKerja = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxNamaTenagaKerja = new System.Windows.Forms.TextBox();
            this.textBoxIdTenagaKerja = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxKeterangan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNoJobOrder = new System.Windows.Forms.TextBox();
            this.buttonKeluar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRateGaji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSatuanKerja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTenagaKerja)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSimpan
            // 
            this.buttonSimpan.BackColor = System.Drawing.Color.DarkSalmon;
            this.buttonSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSimpan.ForeColor = System.Drawing.Color.Black;
            this.buttonSimpan.Location = new System.Drawing.Point(57, 1132);
            this.buttonSimpan.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSimpan.Name = "buttonSimpan";
            this.buttonSimpan.Size = new System.Drawing.Size(176, 67);
            this.buttonSimpan.TabIndex = 98;
            this.buttonSimpan.Text = "SIMPAN";
            this.buttonSimpan.UseVisualStyleBackColor = false;
            this.buttonSimpan.Click += new System.EventHandler(this.buttonSimpan_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSalmon;
            this.groupBox2.Controls.Add(this.comboBoxSatuanKerja);
            this.groupBox2.Controls.Add(this.numericUpDownRateGaji);
            this.groupBox2.Controls.Add(this.numericUpDownSatuanKerja);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkBoxPilihSemuaTenagaKerja);
            this.groupBox2.Controls.Add(this.textBoxJenisKelaminTenagaKerja);
            this.groupBox2.Controls.Add(this.buttonTambahTenagaKerja);
            this.groupBox2.Controls.Add(this.dataGridViewTenagaKerja);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxNamaTenagaKerja);
            this.groupBox2.Controls.Add(this.textBoxIdTenagaKerja);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(26, 125);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1158, 850);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tenaga Kerja";
            // 
            // comboBoxSatuanKerja
            // 
            this.comboBoxSatuanKerja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSatuanKerja.FormattingEnabled = true;
            this.comboBoxSatuanKerja.Items.AddRange(new object[] {
            "Jam"});
            this.comboBoxSatuanKerja.Location = new System.Drawing.Point(999, 116);
            this.comboBoxSatuanKerja.Name = "comboBoxSatuanKerja";
            this.comboBoxSatuanKerja.Size = new System.Drawing.Size(121, 33);
            this.comboBoxSatuanKerja.TabIndex = 108;
            // 
            // numericUpDownRateGaji
            // 
            this.numericUpDownRateGaji.Location = new System.Drawing.Point(807, 190);
            this.numericUpDownRateGaji.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownRateGaji.Name = "numericUpDownRateGaji";
            this.numericUpDownRateGaji.Size = new System.Drawing.Size(162, 31);
            this.numericUpDownRateGaji.TabIndex = 107;
            // 
            // numericUpDownSatuanKerja
            // 
            this.numericUpDownSatuanKerja.Location = new System.Drawing.Point(807, 118);
            this.numericUpDownSatuanKerja.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownSatuanKerja.Name = "numericUpDownSatuanKerja";
            this.numericUpDownSatuanKerja.Size = new System.Drawing.Size(162, 31);
            this.numericUpDownSatuanKerja.TabIndex = 106;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(594, 172);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 60);
            this.label3.TabIndex = 104;
            this.label3.Text = "Rate Gaji Per \r\nSatuan Kerja :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(594, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 30);
            this.label2.TabIndex = 102;
            this.label2.Text = "Satuan Kerja :";
            // 
            // checkBoxPilihSemuaTenagaKerja
            // 
            this.checkBoxPilihSemuaTenagaKerja.AutoSize = true;
            this.checkBoxPilihSemuaTenagaKerja.Location = new System.Drawing.Point(269, 55);
            this.checkBoxPilihSemuaTenagaKerja.Name = "checkBoxPilihSemuaTenagaKerja";
            this.checkBoxPilihSemuaTenagaKerja.Size = new System.Drawing.Size(293, 29);
            this.checkBoxPilihSemuaTenagaKerja.TabIndex = 101;
            this.checkBoxPilihSemuaTenagaKerja.Text = "Pilih Semua Tenaga Kerja";
            this.checkBoxPilihSemuaTenagaKerja.UseVisualStyleBackColor = true;
            this.checkBoxPilihSemuaTenagaKerja.CheckedChanged += new System.EventHandler(this.checkBoxPilihSemuaTenagaKerja_CheckedChanged);
            // 
            // textBoxJenisKelaminTenagaKerja
            // 
            this.textBoxJenisKelaminTenagaKerja.Enabled = false;
            this.textBoxJenisKelaminTenagaKerja.Location = new System.Drawing.Point(269, 223);
            this.textBoxJenisKelaminTenagaKerja.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxJenisKelaminTenagaKerja.Name = "textBoxJenisKelaminTenagaKerja";
            this.textBoxJenisKelaminTenagaKerja.Size = new System.Drawing.Size(254, 31);
            this.textBoxJenisKelaminTenagaKerja.TabIndex = 100;
            // 
            // buttonTambahTenagaKerja
            // 
            this.buttonTambahTenagaKerja.BackColor = System.Drawing.Color.MistyRose;
            this.buttonTambahTenagaKerja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTambahTenagaKerja.ForeColor = System.Drawing.Color.Black;
            this.buttonTambahTenagaKerja.Location = new System.Drawing.Point(416, 319);
            this.buttonTambahTenagaKerja.Margin = new System.Windows.Forms.Padding(6);
            this.buttonTambahTenagaKerja.Name = "buttonTambahTenagaKerja";
            this.buttonTambahTenagaKerja.Size = new System.Drawing.Size(340, 94);
            this.buttonTambahTenagaKerja.TabIndex = 97;
            this.buttonTambahTenagaKerja.Text = "TAMBAH TENAGA KERJA";
            this.buttonTambahTenagaKerja.UseVisualStyleBackColor = false;
            this.buttonTambahTenagaKerja.Click += new System.EventHandler(this.buttonTambahTenagaKerja_Click);
            // 
            // dataGridViewTenagaKerja
            // 
            this.dataGridViewTenagaKerja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTenagaKerja.Location = new System.Drawing.Point(20, 442);
            this.dataGridViewTenagaKerja.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.dataGridViewTenagaKerja.Name = "dataGridViewTenagaKerja";
            this.dataGridViewTenagaKerja.Size = new System.Drawing.Size(1113, 385);
            this.dataGridViewTenagaKerja.TabIndex = 94;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(53, 221);
            this.label7.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 30);
            this.label7.TabIndex = 43;
            this.label7.Text = "Jenis Kelamin:";
            // 
            // textBoxNamaTenagaKerja
            // 
            this.textBoxNamaTenagaKerja.Enabled = false;
            this.textBoxNamaTenagaKerja.Location = new System.Drawing.Point(269, 169);
            this.textBoxNamaTenagaKerja.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNamaTenagaKerja.Name = "textBoxNamaTenagaKerja";
            this.textBoxNamaTenagaKerja.Size = new System.Drawing.Size(254, 31);
            this.textBoxNamaTenagaKerja.TabIndex = 54;
            // 
            // textBoxIdTenagaKerja
            // 
            this.textBoxIdTenagaKerja.Location = new System.Drawing.Point(269, 117);
            this.textBoxIdTenagaKerja.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIdTenagaKerja.MaxLength = 4;
            this.textBoxIdTenagaKerja.Name = "textBoxIdTenagaKerja";
            this.textBoxIdTenagaKerja.Size = new System.Drawing.Size(254, 31);
            this.textBoxIdTenagaKerja.TabIndex = 53;
            this.textBoxIdTenagaKerja.TextChanged += new System.EventHandler(this.textBoxIdTenagaKerja_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(196, 117);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 30);
            this.label5.TabIndex = 47;
            this.label5.Text = "Id :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(145, 169);
            this.label8.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 30);
            this.label8.TabIndex = 48;
            this.label8.Text = "Nama :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox1.Controls.Add(this.textBoxKeterangan);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxNoJobOrder);
            this.groupBox1.Location = new System.Drawing.Point(29, 91);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1210, 999);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            // 
            // textBoxKeterangan
            // 
            this.textBoxKeterangan.Location = new System.Drawing.Point(719, 21);
            this.textBoxKeterangan.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxKeterangan.Multiline = true;
            this.textBoxKeterangan.Name = "textBoxKeterangan";
            this.textBoxKeterangan.Size = new System.Drawing.Size(465, 82);
            this.textBoxKeterangan.TabIndex = 102;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(535, 48);
            this.label10.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 30);
            this.label10.TabIndex = 101;
            this.label10.Text = "Keterangan :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 60);
            this.label6.TabIndex = 39;
            this.label6.Text = "No. \r\nJob Order :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNoJobOrder
            // 
            this.textBoxNoJobOrder.Location = new System.Drawing.Point(227, 48);
            this.textBoxNoJobOrder.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.textBoxNoJobOrder.MaxLength = 5;
            this.textBoxNoJobOrder.Name = "textBoxNoJobOrder";
            this.textBoxNoJobOrder.Size = new System.Drawing.Size(216, 31);
            this.textBoxNoJobOrder.TabIndex = 38;
       
            // 
            // buttonKeluar
            // 
            this.buttonKeluar.BackColor = System.Drawing.Color.DarkSalmon;
            this.buttonKeluar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKeluar.ForeColor = System.Drawing.Color.Black;
            this.buttonKeluar.Location = new System.Drawing.Point(1037, 1133);
            this.buttonKeluar.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.buttonKeluar.Name = "buttonKeluar";
            this.buttonKeluar.Size = new System.Drawing.Size(176, 65);
            this.buttonKeluar.TabIndex = 100;
            this.buttonKeluar.Text = "KELUAR";
            this.buttonKeluar.UseVisualStyleBackColor = false;
            this.buttonKeluar.Click += new System.EventHandler(this.buttonKeluar_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkSalmon;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1, -1);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1264, 75);
            this.label1.TabIndex = 97;
            this.label1.Text = "Perhitungan Dan Pembebanan Biaya Tenaga Kerja";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormPerhitunganDanPembebananBiayaTenagaKerja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1265, 1226);
            this.Controls.Add(this.buttonSimpan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonKeluar);
            this.Controls.Add(this.label1);
            this.Name = "FormPerhitunganDanPembebananBiayaTenagaKerja";
            this.Text = "FormPerhitunganDanPembebananBiayaTenagaKerja";
            this.Load += new System.EventHandler(this.FormPerhitunganDanPembebananBiayaTenagaKerja_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRateGaji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSatuanKerja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTenagaKerja)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSimpan;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxJenisKelaminTenagaKerja;
        private System.Windows.Forms.Button buttonTambahTenagaKerja;
        private System.Windows.Forms.DataGridView dataGridViewTenagaKerja;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNamaTenagaKerja;
        private System.Windows.Forms.TextBox textBoxIdTenagaKerja;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNoJobOrder;
        private System.Windows.Forms.Button buttonKeluar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxPilihSemuaTenagaKerja;
        private System.Windows.Forms.TextBox textBoxKeterangan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxSatuanKerja;
        private System.Windows.Forms.NumericUpDown numericUpDownRateGaji;
        private System.Windows.Forms.NumericUpDown numericUpDownSatuanKerja;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}