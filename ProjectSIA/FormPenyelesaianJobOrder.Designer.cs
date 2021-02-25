namespace ProjectSIA
{
    partial class FormPenyelesaianJobOrder
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNoJobOrder = new System.Windows.Forms.TextBox();
            this.buttonSelesai = new System.Windows.Forms.Button();
            this.dataGridViewJobOrder = new System.Windows.Forms.DataGridView();
            this.textBoxKeterangan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonKeluar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobOrder)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkSalmon;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1264, 75);
            this.label1.TabIndex = 101;
            this.label1.Text = "Penyelesaian Job Order Dan Pengiriman Barang";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNoJobOrder
            // 
            this.textBoxNoJobOrder.Location = new System.Drawing.Point(227, 90);
            this.textBoxNoJobOrder.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.textBoxNoJobOrder.MaxLength = 5;
            this.textBoxNoJobOrder.Name = "textBoxNoJobOrder";
            this.textBoxNoJobOrder.Size = new System.Drawing.Size(216, 31);
            this.textBoxNoJobOrder.TabIndex = 38;
            this.textBoxNoJobOrder.TextChanged += new System.EventHandler(this.textBoxNoJobOrder_TextChanged);
            // 
            // buttonSelesai
            // 
            this.buttonSelesai.BackColor = System.Drawing.Color.MistyRose;
            this.buttonSelesai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelesai.ForeColor = System.Drawing.Color.Black;
            this.buttonSelesai.Location = new System.Drawing.Point(23, 428);
            this.buttonSelesai.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSelesai.Name = "buttonSelesai";
            this.buttonSelesai.Size = new System.Drawing.Size(277, 69);
            this.buttonSelesai.TabIndex = 97;
            this.buttonSelesai.Text = "SELESAI";
            this.buttonSelesai.UseVisualStyleBackColor = false;
            this.buttonSelesai.Click += new System.EventHandler(this.buttonSelesai_Click);
            // 
            // dataGridViewJobOrder
            // 
            this.dataGridViewJobOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJobOrder.Location = new System.Drawing.Point(23, 189);
            this.dataGridViewJobOrder.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.dataGridViewJobOrder.Name = "dataGridViewJobOrder";
            this.dataGridViewJobOrder.Size = new System.Drawing.Size(1113, 195);
            this.dataGridViewJobOrder.TabIndex = 94;
            // 
            // textBoxKeterangan
            // 
            this.textBoxKeterangan.Location = new System.Drawing.Point(683, 55);
            this.textBoxKeterangan.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxKeterangan.Multiline = true;
            this.textBoxKeterangan.Name = "textBoxKeterangan";
            this.textBoxKeterangan.Size = new System.Drawing.Size(453, 98);
            this.textBoxKeterangan.TabIndex = 102;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(499, 91);
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
            this.label6.Location = new System.Drawing.Point(50, 64);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 60);
            this.label6.TabIndex = 39;
            this.label6.Text = "No. \r\nJob Order :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(28, 92);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1210, 624);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSalmon;
            this.groupBox2.Controls.Add(this.textBoxKeterangan);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxNoJobOrder);
            this.groupBox2.Controls.Add(this.buttonSelesai);
            this.groupBox2.Controls.Add(this.dataGridViewJobOrder);
            this.groupBox2.Location = new System.Drawing.Point(26, 32);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1158, 551);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Job Order";
            // 
            // buttonKeluar
            // 
            this.buttonKeluar.BackColor = System.Drawing.Color.DarkSalmon;
            this.buttonKeluar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKeluar.ForeColor = System.Drawing.Color.Black;
            this.buttonKeluar.Location = new System.Drawing.Point(1036, 752);
            this.buttonKeluar.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.buttonKeluar.Name = "buttonKeluar";
            this.buttonKeluar.Size = new System.Drawing.Size(176, 65);
            this.buttonKeluar.TabIndex = 104;
            this.buttonKeluar.Text = "KELUAR";
            this.buttonKeluar.UseVisualStyleBackColor = false;
            this.buttonKeluar.Click += new System.EventHandler(this.buttonKeluar_Click);
            // 
            // FormPenyelesaianJobOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1266, 853);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonKeluar);
            this.Name = "FormPenyelesaianJobOrder";
            this.Text = "FormPenyelesaianJobOrder";
            this.Load += new System.EventHandler(this.FormPenyelesaianJobOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJobOrder)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNoJobOrder;
        private System.Windows.Forms.Button buttonSelesai;
        private System.Windows.Forms.DataGridView dataGridViewJobOrder;
        private System.Windows.Forms.TextBox textBoxKeterangan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonKeluar;
    }
}