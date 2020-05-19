using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tesseract;
using Accord;
using Accord.Imaging.Formats;
using Accord.Imaging.Converters;
using Accord.Imaging.ColorReduction;
using System.Text.RegularExpressions;
using Accord.Imaging;
using Accord.Imaging.ComplexFilters;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using AForge;
using Accord.Imaging.Filters;
using System.Drawing;
using AForge.Math;
namespace TESSERACTDEKSTOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[] readfile2(string spath2)
        {
            byte[] data = null;


            FileInfo fi = new FileInfo(spath2);
            long numbytes = fi.Length;

            FileStream fs = new FileStream(spath2, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            data = br.ReadBytes((int)numbytes);



            return data;


        }

        private void btnpilgambar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pbktp.Image = new Bitmap(ofd.FileName);
                // image file path  
                txpath.Text = ofd.FileName;
            }  
           
        }
        public Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
             new float[] {.3f, .3f, .3f, 0, 0},
             new float[] {.59f, .59f, .59f, 0, 0},
             new float[] {.11f, .11f, .11f, 0, 0},
             new float[] {0, 0, 0, 1, 0},
             new float[] {0, 0, 0, 0, 1}
                   });

                //create some image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {

                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    //draw the original image on the new image
                    //using the grayscale color matrix
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }

        public Bitmap AdjustBrightnessContrast(Bitmap image, int contrastValue, int brightnessValue)
        {
            float brightness = -(brightnessValue / 100.0f);
            float contrast = contrastValue / 100.0f;
            using (Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb))
            using (Graphics g = Graphics.FromImage(bitmap))
            using (ImageAttributes attributes = new ImageAttributes())
            {
                float[][] matrix = {
            new float[] { contrast, 0, 0, 0, 0},
            new float[] {0, contrast, 0, 0, 0},
            new float[] {0, 0, contrast, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {brightness, brightness, brightness, 1, 1}
        };

                ColorMatrix colorMatrix = new ColorMatrix(matrix);
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);
                return (Bitmap)bitmap.Clone();
            }
        }


        public string process2(Bitmap p)
        {
            const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

            Bitmap process_2 = this.MakeGrayscale3(p);
            Bitmap process_3 = this.AdjustBrightnessContrast(process_2, 300, 100);


            using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))

            //   using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
            {
                //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

                using (Bitmap bmp = process_2)
                {

                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        string hasiltext = page.GetText();
                        //  string text = page.GetText();
                        //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
                        //foreach (string s in charactersToReplace)
                        //{
                        //    text = text.Replace(s, "");
                        //}
                        //text = Regex.Replace(text, @"\n+", string.Empty);
                        return hasiltext;
                        
                    }
                }
            }
        }

        private void btnproses_Click(object sender, EventArgs e)
        {
            try
            {
                string hasil = this.process2(new Bitmap(txpath.Text));
                var stringWithoutEmptyLines = hasil.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                string resultString = Regex.Replace(hasil, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                string[] sep = new string[] { "\n" };
                string[] lines = stringWithoutEmptyLines.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                txtnik.Text = lines[2].Trim();
                txtnama.Text = lines[3].Trim();
                txtalamat.Text = lines[6].Trim() + Environment.NewLine + lines[7].Trim() + Environment.NewLine + lines[8].Trim() + Environment.NewLine + lines[9].Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Peringatan", "Gambar yang anda Masukkan Bukan KTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //richTextBox1.Text = hasil;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
