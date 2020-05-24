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

        public string WordingProcess(string text)
        {
            // string hasil = RemoveSpecialCharacters(text);
            string resultString = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            string a = resultString.Replace("senis Kelamn — ", "").Replace("denisKelamin ", "").Replace("Name", "").Replace("KellDesa", "").Replace("Gol Darah", "").Replace("Gol Darah ", "").Replace("& ne Kelaman", "").Replace("Mama", "").Replace("ATAW", "").Replace("Status Perkawmnan", "").Replace("ampatTgi Lahu", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("Name", "").Replace("'erkawinan", "").Replace("Jenis kelamin", "").Replace("RTIW", "").Replace("TempatTgi Lahir", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("GoiDarah O", "").Replace("Kelesa — ", "").Replace("Status Perkawinar", "").Replace("gol.darah", "").Replace("Alamat", "").Replace("Berlaku Hingga", "").Replace("Jenis Kelamin", "").Replace("TempauTgi Lahir", "").Replace("RTAW", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("RTRW ", "").Replace("Kewarganegaraan", "").Replace("KeVDesa", "").Replace("RTRW", "").Replace("Gol Darah ", "").Replace("/", "").Replace("Jjene kelamin", "").Replace("Tempai TgiLahu", "").Replace("NIK", "").Replace("Nama", "").Replace("Agama", "").Replace("Pekerjaan", "").Replace("Status Perkawinan", "").Replace("KelDesa", "").Replace("Kecamatan", "").Replace("StatusPerkawinan.", "").Replace(":", "").Replace(".", "").Replace("RT/RW", "").Replace("TempaiTgiLahu-", "").Replace("Jjenekelamin", "").Replace("Gol.Darah", "").Replace("Almat", "").Replace("?", "").Replace("TempaiTgiLahu", "").Replace("MPIalLahu", "").Replace("0rKelamun", "").Replace("Alnal", "").Replace("HIRW", "").Replace("KetDesa", "").Replace("StatusPerkawinan", "").Replace("BerlakuHingga", "").Replace("GolDaran", "").Replace("TemparIgllahu—", "").Replace("seniskelamin", "").Replace("Alnat", "").Replace("TempatTgiLahi", "").Replace("Jeriskelamin", "");

            return a;
            //string[] sep = new string[] { "\n" };
            //string[] lines = a.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            //String[] sEmails = new String[lines.Count()];
            //for (int ix = 0; ix < lines.Count(); ix++)
            //{
            //    if (lines[ix] == null || lines[ix] == "")
            //    {
            //        sEmails[ix] = lines[ix].Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            //    }
            //    else
            //    {
            //        sEmails[ix] = lines[ix].Trim('\r', '\n');
            //    }


            //    yield return sEmails[ix];
            //}
        }


        public Bitmap DeskewImage(Bitmap bmp)
        {
            DocumentSkewChecker sc = new DocumentSkewChecker();
            Bitmap workmap = new Bitmap(bmp);
            Bitmap workmap1 = new Bitmap(bmp);
            Bitmap tempWorkmap = new Bitmap(workmap.Width, workmap.Height);
            using (Graphics g = Graphics.FromImage(tempWorkmap))
                g.DrawImage(workmap, 0, 0);
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            workmap = filter.Apply(tempWorkmap);
            double angle = sc.GetSkewAngle(workmap);
            RotateBilinear rf = new RotateBilinear(-angle);
            rf.FillColor = Color.White;
            workmap1 = rf.Apply(workmap);
            return workmap1;
        }

        public Bitmap converttobw(Bitmap px)
        {
            Bitmap bmp = new Bitmap(px);
            int width = bmp.Width;
            int height = bmp.Height;
            int[] arr = new int[225];
            int i = 0;
            Color p;

            //Grayscale
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    p = bmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    int avg = (r + g + b) / 3;
                    avg = avg < 128 ? 0 : 255;     // Converting gray pixels to either pure black or pure white
                    bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                }
            }
            return bmp;
        }

        public  Bitmap sharpen(Bitmap image)
        {
            Bitmap sharpenImage = new Bitmap(image.Width, image.Height);

            int filterWidth = 3;
            int filterHeight = 3;
            int w = image.Width;
            int h = image.Height;

            double[,] filter = new double[filterWidth, filterHeight];

            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;

            double factor = 1.0;
            double bias = 0.0;

            Color[,] result = new Color[image.Width, image.Height];

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;

                

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + w) % w;
                            int imageY = (y - filterHeight / 2 + filterY + h) % h;

                            //=====[INSERT LINES]========================================================
                            // Get the color here - once per fiter entry and image pixel.
                            Color imageColor = image.GetPixel(imageX, imageY);
                            //===========================================================================

                            red += imageColor.R * filter[filterX, filterY];
                            green += imageColor.G * filter[filterX, filterY];
                            blue += imageColor.B * filter[filterX, filterY];
                        }
                        int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb(r, g, b);
                    }
                }
            }
            for (int i = 0; i < w; ++i)
            {
                for (int j = 0; j < h; ++j)
                {
                    sharpenImage.SetPixel(i, j, result[i, j]);
                }
            }
            return sharpenImage;
        }

        public void process2(Bitmap p)
        {
            const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";


           // Bitmap x = this.sharpen(new Bitmap(p));

            //Bitmap pxx = this.converttobw(new Bitmap(p));

           // Bitmap process_2 = this.DeskewImage(p);
          // Bitmap pr = this.MakeGrayscale3(p);
        //   Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
          // Bitmap sx = filter.Apply(p);
           // Bitmap process_3 = this.AdjustBrightnessContrast(pr, 400,100);


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

                using (Bitmap bmp = p)
                {

                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                       // hasiltext = page.GetText();
                        string text = page.GetText();
                       // string data = WordingProcess(text);
                      //  string a = data.ToString();
                       // Session["hasil"] = a;
                        var stringWithoutEmptyLines = text.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                        string resultString = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

                        string a = resultString.Replace("— “ ","").Replace("senis Kelamn — ", "").Replace("denisKelamin ", "").Replace("Name", "").Replace("KellDesa", "").Replace("Gol Darah", "").Replace("Gol Darah ", "").Replace("& ne Kelaman", "").Replace("Mama", "").Replace("ATAW", "").Replace("Status Perkawmnan", "").Replace("ampatTgi Lahu", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("Name", "").Replace("'erkawinan", "").Replace("Jenis kelamin", "").Replace("RTIW", "").Replace("TempatTgi Lahir", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("GoiDarah O", "").Replace("Kelesa — ", "").Replace("Status Perkawinar", "").Replace("gol.darah", "").Replace("Alamat", "").Replace("Berlaku Hingga", "").Replace("Jenis Kelamin", "").Replace("TempauTgi Lahir", "").Replace("RTAW", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("RTRW ", "").Replace("Kewarganegaraan", "").Replace("KeVDesa", "").Replace("RTRW", "").Replace("Gol Darah ", "").Replace("/", "").Replace("Jjene kelamin", "").Replace("Tempai TgiLahu", "").Replace("NIK", "").Replace("Nama", "").Replace("Agama", "").Replace("Pekerjaan", "").Replace("Status Perkawinan", "").Replace("KelDesa", "").Replace("Kecamatan", "").Replace("StatusPerkawinan.", "").Replace(":", "").Replace(".", "").Replace("RT/RW", "").Replace("TempaiTgiLahu-", "").Replace("Jjenekelamin", "").Replace("Gol.Darah", "").Replace("Almat", "").Replace("?", "").Replace("TempaiTgiLahu", "").Replace("MPIalLahu", "").Replace("0rKelamun", "").Replace("Alnal", "").Replace("HIRW", "").Replace("KetDesa", "").Replace("StatusPerkawinan", "").Replace("BerlakuHingga", "").Replace("GolDaran", "").Replace("TemparIgllahu—", "").Replace("seniskelamin", "").Replace("Alnat", "").Replace("TempatTgiLahi", "").Replace("Jeriskelamin", "");

                        
                        string[] sep = new string[] { "\n" };
                        string[] lines = stringWithoutEmptyLines.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
                        //foreach (string s in charactersToReplace)
                        //{
                        //    text = text.Replace(s, "");
                        //}
                        //text = Regex.Replace(text, @"\n+", string.Empty);
                        txtarea.InnerText = a;
                        
                       // hasil2 h = new hasil2{hasil=text};
                        //var b = h.hasil.Contains("NIK");
                      //  TextBox1.Text = h.hasil.Select(text.=="NIK");

                        

                        
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
            //if (ext.ToLower() == ".jpg" || ext.ToLower()==".jpeg")
            //{
                Bitmap bs = new Bitmap(filepath);
                
                   // StartOCR(bs);
                    process2(bs);
                   // process(bs);
            
            //}
            //else
            //{
            //    Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            //}

       
        
        }
    }
}