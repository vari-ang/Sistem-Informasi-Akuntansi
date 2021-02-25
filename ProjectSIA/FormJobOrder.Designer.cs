namespace ProjectSIA
{
    partial class FormJobOrder
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
            this.buttonKeluar = new System.Windows.Forms.Button();
            this.buttonSimpan = new System.Windows.Forms.Button();
            this.comboBoxNoNota = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownFactoryOverhead = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDirectLabor = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDirectMaterial = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
            this.textBoxIDBarang = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePickerTglSelesai = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNoJobOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerTglMulai = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFactoryOverhead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDirectLabor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDirectMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonKeluar
            // 
            this.buttonKeluar.BackColor = System.Drawing.Color.DarkSalmon;
            this.buttonKeluar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKeluar.ForeColor = System.Drawing.Color.Black;
            this.buttonKeluar.Location = new System.Drawing.Point(985, 883);
            this.buttonKeluar.Margin = new System.Windows.Forms.Padding(6);
            this.buttonKeluar.Name = "buttonKeluar";
            this.buttonKeluar.Size = new System.Drawing.Size(176, 67);
            this.buttonKeluar.TabIndex = 99;
            this.buttonKeluar.Text = "KELUAR";
            this.buttonKeluar.UseVisualStyleBackColor = false;
            this.buttonKeluar.Click += new System.EventHandler(this.buttonKeluar_Click);
            // 
            // buttonSimpan
            // 
            this.buttonSimpan.BackColor = System.Drawing.Color.DarkSalmon;
            this.buttonSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSimpan.ForeColor = System.Drawing.Color.Black;
            this.buttonSimpan.Location = new System.Drawing.Point(94, 883);
            this.buttonSimpan.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSimpan.Name = "buttonSimpan";
            this.buttonSimpan.Size = new System.Drawing.Size(176, 67);
            this.buttonSimpan.TabIndex = 98;
            this.buttonSimpan.Text = "SIMPAN";
            this.buttonSimpan.UseVisualStyleBackColor = false;
            this.buttonSimpan.Click += new System.EventHandler(this.buttonSimpan_Click);
            // 
            // comboBoxNoNota
            // 
            this.comboBoxNoNota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNoNota.FormattingEnabled = true;
            this.comboBoxNoNota.Location = new System.Drawing.Point(533, 124);
            this.comboBoxNoNota.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.comboBoxNoNota.Name = "comboBoxNoNota";
            this.comboBoxNoNota.Size = new System.Drawing.Size(288, 33);
            this.comboBoxNoNota.TabIndex = 65;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(372, 193);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 25);
            this.label10.TabIndex = 64;
            this.label10.Text = " ID Barang :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox1.Controls.Add(this.numericUpDownFactoryOverhead);
            this.groupBox1.Controls.Add(this.numericUpDownDirectLabor);
            this.groupBox1.Controls.Add(this.numericUpDownDirectMaterial);
            this.groupBox1.Controls.Add(this.numericUpDownQuantity);
            this.groupBox1.Controls.Add(this.comboBoxNoNota);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxIDBarang);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dateTimePickerTglSelesai);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxNoJobOrder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePickerTglMulai);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(28, 115);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(1185, 729);
            this.groupBox1.TabIndex = 97;
            this.groupBox1.TabStop = false;
            // 
            // numericUpDownFactoryOverhead
            // 
            this.numericUpDownFactoryOverhead.Location = new System.Drawing.Point(531, 583);
            this.numericUpDownFactoryOverhead.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownFactoryOverhead.Name = "numericUpDownFactoryOverhead";
            this.numericUpDownFactoryOverhead.Size = new System.Drawing.Size(290, 31);
            this.numericUpDownFactoryOverhead.TabIndex = 69;
            // 
            // numericUpDownDirectLabor
            // 
            this.numericUpDownDirectLabor.Location = new System.Drawing.Point(531, 516);
            this.numericUpDownDirectLabor.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownDirectLabor.Name = "numericUpDownDirectLabor";
            this.numericUpDownDirectLabor.Size = new System.Drawing.Size(292, 31);
            this.numericUpDownDirectLabor.TabIndex = 68;
            // 
            // numericUpDownDirectMaterial
            // 
            this.numericUpDownDirectMaterial.Location = new System.Drawing.Point(535, 449);
            this.numericUpDownDirectMaterial.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownDirectMaterial.Name = "numericUpDownDirectMaterial";
            this.numericUpDownDirectMaterial.Size = new System.Drawing.Size(288, 31);
            this.numericUpDownDirectMaterial.TabIndex = 67;
            // 
            // numericUpDownQuantity
            // 
            this.numericUpDownQuantity.Location = new System.Drawing.Point(531, 248);
            this.numericUpDownQuantity.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownQuantity.Name = "numericUpDownQuantity";
            this.numericUpDownQuantity.Size = new System.Drawing.Size(286, 31);
            this.numericUpDownQuantity.TabIndex = 66;
            // 
            // textBoxIDBarang
            // 
            this.textBoxIDBarang.Location = new System.Drawing.Point(531, 187);
            this.textBoxIDBarang.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxIDBarang.MaxLength = 4;
            this.textBoxIDBarang.Name = "textBoxIDBarang";
            this.textBoxIDBarang.Size = new System.Drawing.Size(290, 31);
            this.textBoxIDBarang.TabIndex = 63;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(389, 124);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 25);
            this.label9.TabIndex = 62;
            this.label9.Text = "No. Nota :";
            // 
            // dateTimePickerTglSelesai
            // 
            this.dateTimePickerTglSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTglSelesai.Location = new System.Drawing.Point(531, 377);
            this.dateTimePickerTglSelesai.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerTglSelesai.Name = "dateTimePickerTglSelesai";
            this.dateTimePickerTglSelesai.Size = new System.Drawing.Size(292, 31);
            this.dateTimePickerTglSelesai.TabIndex = 60;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(234, 377);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(263, 25);
            this.label8.TabIndex = 59;
            this.label8.Text = "Tanggal Selesai Produksi:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(301, 589);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(196, 25);
            this.label7.TabIndex = 57;
            this.label7.Text = "Factory Overhead :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 519);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 25);
            this.label6.TabIndex = 55;
            this.label6.Text = "Direct Labor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(334, 454);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 25);
            this.label5.TabIndex = 53;
            this.label5.Text = "Direct Material :";
            // 
            // textBoxNoJobOrder
            // 
            this.textBoxNoJobOrder.Location = new System.Drawing.Point(531, 61);
            this.textBoxNoJobOrder.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxNoJobOrder.MaxLength = 12;
            this.textBoxNoJobOrder.Name = "textBoxNoJobOrder";
            this.textBoxNoJobOrder.Size = new System.Drawing.Size(290, 31);
            this.textBoxNoJobOrder.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 254);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 52;
            this.label3.Text = "Quantity :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(345, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "No. Job Order:";
            // 
            // dateTimePickerTglMulai
            // 
            this.dateTimePickerTglMulai.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTglMulai.Location = new System.Drawing.Point(533, 311);
            this.dateTimePickerTglMulai.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerTglMulai.Name = "dateTimePickerTglMulai";
            this.dateTimePickerTglMulai.Size = new System.Drawing.Size(290, 31);
            this.dateTimePickerTglMulai.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 311);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tanggal Mulai Produksi:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DarkSalmon;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(-3, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1237, 89);
            this.label4.TabIndex = 96;
            this.label4.Text = "JOB ORDER";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormJobOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1235, 995);
            this.Controls.Add(this.buttonKeluar);
            this.Controls.Add(this.buttonSimpan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Name = "FormJobOrder";
            this.Text = "FormJobOrder";
            this.Load += new System.EventHandler(this.FormJobOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFactoryOverhead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDirectLabor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDirectMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonKeluar;
        private System.Windows.Forms.Button buttonSimpan;
        private System.Windows.Forms.ComboBox comboBoxNoNota;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxIDBarang;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePickerTglSelesai;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxNoJobOrder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerTglMulai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownFactoryOverhead;
        private System.Windows.Forms.NumericUpDown numericUpDownDirectLabor;
        private System.Windows.Forms.NumericUpDown numericUpDownDirectMaterial;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
    }
}