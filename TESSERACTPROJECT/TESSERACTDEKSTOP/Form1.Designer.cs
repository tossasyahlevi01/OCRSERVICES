namespace TESSERACTDEKSTOP
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pbktp = new System.Windows.Forms.PictureBox();
            this.btnpilgambar = new System.Windows.Forms.Button();
            this.btnproses = new System.Windows.Forms.Button();
            this.txpath = new System.Windows.Forms.RichTextBox();
            this.txtnik = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtnama = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtalamat = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbktp)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(829, 74);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "OCR Dekstop Version Khusus KTP";
            // 
            // pbktp
            // 
            this.pbktp.Location = new System.Drawing.Point(12, 92);
            this.pbktp.Name = "pbktp";
            this.pbktp.Size = new System.Drawing.Size(342, 240);
            this.pbktp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbktp.TabIndex = 1;
            this.pbktp.TabStop = false;
            // 
            // btnpilgambar
            // 
            this.btnpilgambar.Location = new System.Drawing.Point(12, 386);
            this.btnpilgambar.Name = "btnpilgambar";
            this.btnpilgambar.Size = new System.Drawing.Size(342, 38);
            this.btnpilgambar.TabIndex = 2;
            this.btnpilgambar.Text = "Pilih Gambar";
            this.btnpilgambar.UseVisualStyleBackColor = true;
            this.btnpilgambar.Click += new System.EventHandler(this.btnpilgambar_Click);
            // 
            // btnproses
            // 
            this.btnproses.Location = new System.Drawing.Point(12, 430);
            this.btnproses.Name = "btnproses";
            this.btnproses.Size = new System.Drawing.Size(342, 38);
            this.btnproses.TabIndex = 3;
            this.btnproses.Text = "Proses";
            this.btnproses.UseVisualStyleBackColor = true;
            this.btnproses.Click += new System.EventHandler(this.btnproses_Click);
            // 
            // txpath
            // 
            this.txpath.Location = new System.Drawing.Point(12, 338);
            this.txpath.Name = "txpath";
            this.txpath.Size = new System.Drawing.Size(342, 42);
            this.txpath.TabIndex = 5;
            this.txpath.Text = "";
            // 
            // txtnik
            // 
            this.txtnik.Location = new System.Drawing.Point(433, 99);
            this.txtnik.Name = "txtnik";
            this.txtnik.Size = new System.Drawing.Size(372, 26);
            this.txtnik.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "NIK";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nama";
            // 
            // txtnama
            // 
            this.txtnama.Location = new System.Drawing.Point(433, 134);
            this.txtnama.Name = "txtnama";
            this.txtnama.Size = new System.Drawing.Size(372, 26);
            this.txtnama.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Alamat";
            // 
            // txtalamat
            // 
            this.txtalamat.Location = new System.Drawing.Point(433, 166);
            this.txtalamat.Multiline = true;
            this.txtalamat.Name = "txtalamat";
            this.txtalamat.Size = new System.Drawing.Size(372, 91);
            this.txtalamat.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(829, 498);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtalamat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtnama);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtnik);
            this.Controls.Add(this.txpath);
            this.Controls.Add(this.btnproses);
            this.Controls.Add(this.btnpilgambar);
            this.Controls.Add(this.pbktp);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbktp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbktp;
        private System.Windows.Forms.Button btnpilgambar;
        private System.Windows.Forms.Button btnproses;
        private System.Windows.Forms.RichTextBox txpath;
        private System.Windows.Forms.TextBox txtnik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtnama;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtalamat;
    }
}

