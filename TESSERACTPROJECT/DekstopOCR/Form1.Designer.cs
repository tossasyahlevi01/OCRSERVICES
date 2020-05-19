namespace DekstopOCR
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
            this.pb = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.path = new System.Windows.Forms.TextBox();
            this.nik = new System.Windows.Forms.TextBox();
            this.nama = new System.Windows.Forms.TextBox();
            this.alamat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Location = new System.Drawing.Point(26, 12);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(260, 156);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(26, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(26, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(267, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Proses";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(26, 262);
            this.path.Multiline = true;
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(267, 78);
            this.path.TabIndex = 3;
            // 
            // nik
            // 
            this.nik.Location = new System.Drawing.Point(337, 39);
            this.nik.Name = "nik";
            this.nik.Size = new System.Drawing.Size(267, 26);
            this.nik.TabIndex = 4;
            // 
            // nama
            // 
            this.nama.Location = new System.Drawing.Point(337, 119);
            this.nama.Multiline = true;
            this.nama.Name = "nama";
            this.nama.Size = new System.Drawing.Size(267, 60);
            this.nama.TabIndex = 5;
            // 
            // alamat
            // 
            this.alamat.Location = new System.Drawing.Point(337, 224);
            this.alamat.Multiline = true;
            this.alamat.Name = "alamat";
            this.alamat.Size = new System.Drawing.Size(277, 160);
            this.alamat.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Alamat";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(639, 404);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.alamat);
            this.Controls.Add(this.nama);
            this.Controls.Add(this.nik);
            this.Controls.Add(this.path);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pb);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.TextBox nik;
        private System.Windows.Forms.TextBox nama;
        private System.Windows.Forms.TextBox alamat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

