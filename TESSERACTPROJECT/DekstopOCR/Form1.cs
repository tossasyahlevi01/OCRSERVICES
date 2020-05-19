using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesseractLibrary;
using System.Text.RegularExpressions;
namespace DekstopOCR
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OCRService OCR = new OCRService();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pb.Image = new Bitmap(ofd.FileName);
                // image file path  
                path.Text = ofd.FileName;
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
                string hasil = OCR.process2(new Bitmap(path.Text),4,5);
                var stringWithoutEmptyLines = hasil.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                string resultString = Regex.Replace(hasil, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                string[] sep = new string[] { "\n" };
                string[] lines = stringWithoutEmptyLines.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                nik.Text = lines[2].Trim();
                nama.Text = lines[3].Trim();
                alamat.Text = lines[6].Trim() + Environment.NewLine + lines[7].Trim() + Environment.NewLine + lines[8].Trim() + Environment.NewLine + lines[9].Trim();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Peringatan", "Gambar yang anda Masukkan Bukan KTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
