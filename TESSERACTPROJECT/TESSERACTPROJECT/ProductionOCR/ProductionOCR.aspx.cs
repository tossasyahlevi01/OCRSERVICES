using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using Accord.Imaging;
using System.Web.Hosting;
using Accord;
using Accord.Imaging.Formats;
using Accord.Imaging.Converters;
using Accord.Imaging.ColorReduction;
using System.Text.RegularExpressions;
using Accord.Imaging.ComplexFilters;
using AForge;

using Accord.Imaging.Filters;
using AForge.Math;

using Tesseract;
namespace TESSERACTPROJECT.ProductionOCR
{
    public partial class ProductionOCR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        //public class hasil2
        //{
        //    public string hasil { get { return  hasil; } set { hasil = Session["hasil"].ToString(); } }
        //}

        public void process(Bitmap p)
        {
            const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

         


         

            using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
            {
                //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

                using (Bitmap bmp =p)
                {

                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        // hasiltext = page.GetText();
                        string text = page.GetText();
                        Session["hasil"] = text;

                        txtarea.InnerText = text.Trim();


                        var stringWithoutEmptyLines = text.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                        string resultString = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                        string[] sep = new string[] { "\n" };
                        string[] lines = stringWithoutEmptyLines.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }
        }

        public void process2(Bitmap p)
        {
            const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

            Bitmap process_2 = this.MakeGrayscale3(new Bitmap(p));
            Bitmap process_3 = this.AdjustBrightnessContrast(new Bitmap(process_2), 0, 0);


           // MemoryStream ms = new MemoryStream();
            //  process_2.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

           // process_2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            //TAMPILKAN GAMBAR HASIL GREYSCALE
          //  var base64Data = Convert.ToBase64String(ms.ToArray());
           // img1.Src = "data:image/Jpeg;base64," + base64Data;

            using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))

            {
                //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

                using (Bitmap bmp = process_3)
                {

                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                       // hasiltext = page.GetText();
                        string text = page.GetText();
                        Session["hasil"] = text;
                        //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
                        //foreach (string s in charactersToReplace)
                        //{
                        //    text = text.Replace(s, "");
                        //}
                        //text = Regex.Replace(text, @"\n+", string.Empty);
                        txtarea.InnerText= text.Trim();
                        
                       // hasil2 h = new hasil2{hasil=text};
                        //var b = h.hasil.Contains("NIK");
                      //  TextBox1.Text = h.hasil.Select(text.=="NIK");
                        var stringWithoutEmptyLines = text.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                       string resultString = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                      string[] sep = new string[] {"\n"};
                      string[] lines = stringWithoutEmptyLines.Split(sep, StringSplitOptions.RemoveEmptyEntries);
//txtnik.Text = lines[2].Trim();
//txtnama.Text = lines[3].Trim();
//txttanggal.Text = lines[4].Trim();
//txtalamat.InnerText = lines[6].Trim() + Environment.NewLine + lines[7].Trim() + Environment.NewLine + lines[8].Trim() + Environment.NewLine + lines[9].Trim();

                      
                        }
                       // btn1.Text = txtarea.Value;
                    }
                }
            }
        


        protected void btns_Click(object sender, EventArgs e)
        {
              string folderpath = Server.MapPath("~/IMAGES/");
           /// string folderpath2 = Server.MapPath("~/IMAGES2/");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            upload.SaveAs(folderpath + Path.GetFileName(upload.FileName));
            string filepath = folderpath + Path.GetFileName(upload.FileName);
            //string filepath2 = folderpath2 + Path.GetFileName(upload.FileName);
            imgori.ImageUrl = "~/IMAGES/" + Path.GetFileName(upload.FileName);

            string ext = System.IO.Path.GetExtension(upload.FileName);
            if (ext.ToLower() == ".jpg" || ext.ToLower()==".jpeg")
            {
                Bitmap bs = new Bitmap(filepath);
                
                   // StartOCR(bs);
                    process2(bs);
                    process(bs);
            
            }
            else
            {
                Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            }

       
        
        }
    }
}