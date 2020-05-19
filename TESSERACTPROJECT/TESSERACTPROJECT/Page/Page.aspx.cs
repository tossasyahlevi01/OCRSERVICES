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
namespace TESSERACTPROJECT.Page
{
    public partial class Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }
        public string hasiltext { get; set; }
        public string hasiltext2 { get; set; }

        public Bitmap make_bw(Bitmap original)
        {

            Bitmap output = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            return output;

        }


        public void convertxtoriginal()
        {
            TextWriter txt = new StreamWriter(@"C:\Users\WIWIK\Documents\Visual Studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\doc.txt");
            txt.Write(txtarea.InnerText);
            txt.Close();  
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

        public Bitmap ToGrayScale(Bitmap Bmp)
        {
            Bitmap output = new Bitmap(Bmp.Width, Bmp.Height);

            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)Math.Round(.299 * c.R + .587 * c.G + .114 * c.B);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return output;
        }

        public Bitmap make_bw2(Bitmap original)
        {

            Bitmap output = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            return output;

        }

        public Bitmap ScaleByPercent(Bitmap imgPhoto, int Percent)
        {
            string folderpath = Server.MapPath("~/IMAGES2/");
            string filepath = folderpath + Path.GetFileName(upload.FileName);

            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                  imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                              new System.Drawing.Rectangle(0, 0, destWidth, destHeight),
                              new System.Drawing.Rectangle(0, 0, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);
            bmPhoto.Save(filepath, System.Drawing.Imaging.ImageFormat.Png);
            grPhoto.Dispose();
            return bmPhoto;
        }

        public Bitmap changetowhite(Bitmap bit)
        {
            Bitmap bmp = bit;
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

        //public Bitmap rr(Bitmap s)
        //{
        //    Bitmap tempImage = AForge.Imaging.Image.FromFile(imagePath);
        //    Bitmap image;
        //    if (tempImage.PixelFormat.ToString().Equals("Format8bppIndexed"))
        //    {
        //        image = tempImage;
        //    }
        //    else
        //    {
        //        image = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(tempImage);
        //    }

        //    tempImage.Dispose();

        //    AForge.Imaging.DocumentSkewChecker skewChecker = new AForge.Imaging.DocumentSkewChecker();
        //    // get documents skew angle
        //    double angle = skewChecker.GetSkewAngle(image);
        //    // create rotation filter
        //    AForge.Imaging.Filters.RotateBilinear rotationFilter = new AForge.Imaging.Filters.RotateBilinear(-angle);
        //    rotationFilter.FillColor = Color.Black;
        //    // rotate image applying the filter
        //    Bitmap rotatedImage = rotationFilter.Apply(image);

        //    var deskewedImagePath = folderSavePath + filename + "_deskewed.tiff";
        //    rotatedImage.Save(deskewedImagePath, System.Drawing.Imaging.ImageFormat.Tiff);

        //    image.Dispose();
        //    rotatedImage.Dispose();
        //}

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



        //public void process4(string p)
        //{
        //    const string language = "eng";
        //    const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

        //    Bitmap process_4 = this.DeskewImage(new Bitmap(p));



        //    MemoryStream ms = new MemoryStream();
        //    process_4.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

        //    process_4.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    var base64Data = Convert.ToBase64String(ms.ToArray());
        //    img3.Src = "data:image/Jpeg;base64," + base64Data;

        //    using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
        //    //   using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
        //    {
        //        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

        //        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

        //        Pix pix;


        //        using (Bitmap bmp = process_4)
        //        {
        //            using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
        //            {
        //                string text = page.GetText();
        //                string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
        //                foreach (string s in charactersToReplace)
        //                {
        //                    text = text.Replace(s, "");
        //                }
        //                Textarea3.InnerText = text;

        //            }
        //        }
        //    }
        //}


        //public void process3(string p)
        //{
        //    const string language = "eng";
        //    const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

        //    Bitmap process_3 = this.changetowhite(new Bitmap(p));



        //    MemoryStream ms = new MemoryStream();
        //    process_3.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

        //    process_3.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    var base64Data = Convert.ToBase64String(ms.ToArray());
        //    img2.Src = "data:image/Jpeg;base64," + base64Data;
        //    process_3.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

        //    using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))

        //    //   using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
        //    {
        //        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

        //        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");


        //        using (Bitmap bmp = process_3)
        //        {
        //            using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
        //            {
        //                string text = page.GetText();
        //                string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
        //                foreach (string s in charactersToReplace)
        //                {
        //                    text = text.Replace(s, "");
        //                }
        //                Textarea2.InnerText = text;
        //            }
        //        }
        //    }
        //}


        //public void process2180(Bitmap p)
        //{
        //    const string language = "eng";
        //    const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

        //    Bitmap process_2 = this.MakeGrayscale3(new Bitmap(p));


        //    MemoryStream ms = new MemoryStream();
        //    process_2.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

        //    process_2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    var base64Data = Convert.ToBase64String(ms.ToArray());
        //    img2.Src = "data:image/Jpeg;base64," + base64Data;

        //    using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))

        //    //   using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
        //    {
        //        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

        //        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

        //        using (Bitmap bmp = process_2)
        //        {

        //            using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
        //            {
        //                string text = page.GetText();
        //                //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
        //                //foreach (string s in charactersToReplace)
        //                //{
        //                //    text = text.Replace(s, "");
        //                //}
        //                //text = Regex.Replace(text, @"\n+", string.Empty);
        //                Textarea3.InnerText = text;
        //            }
        //        }
        //    }
        //}

       

        //public void StartOCR180(Bitmap path)
        //{
        //    const string language = "eng";
        //    const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";
        //    const string ts = @"C:\Users\WIWIK\Documents\Visual Studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";
        //    Bitmap original = new Bitmap(path);
        //    //Bitmap se = RotateImage(original, 90);

        //    //  Bitmap sc = this.ScaleByPercent(original, 300);

        //    // using (TesseractEngine processor = new TesseractEngine(ts, "eng", EngineMode.Default))

        //    //IPix image2 = Pix.LoadFromFile(image);

        //    //Tesseract.Page text = tess.Process(image2, rect, PageSegMode.Auto);

        //    using (TesseractEngine processor = new TesseractEngine(ts, "ind", EngineMode.Default))
        //    {
        //        //processor.SetVariable("tessedit_char_whitelist", "0123456789,.$¢");
        //        //processor.SetVariable("tessedit_char_whitelist", "xyz");
        //        //processor.SetVariable("classify_bln_numeric_mode", "1 or 0"); 
        //        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

        //        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");


        //        using (Bitmap bmp = original)
        //        {

        //            original.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);


        //            using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
        //            {
        //                string text = page.GetText().Trim();
        //                //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
        //                //foreach (string s in charactersToReplace)
        //                //{
        //                //    text = text.Replace(s, "");
        //                //}

        //                Textarea2.InnerText = text;
        //            }
        //        }
        //    }
        //}
      
         public void process2(Bitmap p)
         {
        const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";

            Bitmap process_2 = this.MakeGrayscale3(new Bitmap(p));

       
            MemoryStream ms = new MemoryStream();
          //  process_2.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

            process_2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            img1.Src = "data:image/Jpeg;base64," + base64Data;

            using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))

         //   using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
            {
                //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
             
                using (Bitmap bmp = process_2)
                {

                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        hasiltext = page.GetText();
                        string text = page.GetText();
                        //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
                        //foreach (string s in charactersToReplace)
                        //{
                        //    text = text.Replace(s, "");
                        //}
                        //text = Regex.Replace(text, @"\n+", string.Empty);
                       Textarea1.InnerText = text;
                    } 
                }
            }
        }

         public static Bitmap RotateImage(Bitmap img, float rotationAngle)
         {
             //create an empty Bitmap image
             Bitmap bmp = new Bitmap(img.Width, img.Height);

             //turn the Bitmap into a Graphics object
             Graphics gfx = Graphics.FromImage(bmp);

             //now we set the rotation point to the center of our image
             gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

             //now rotate the image
             gfx.RotateTransform(rotationAngle);

             gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

             //set the InterpolationMode to HighQualityBicubic so to ensure a high
             //quality image once it is transformed to the specified size
             gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

             //now draw our new image onto the graphics object
             gfx.DrawImage(img, new System.Drawing.Point(0, 0));

             //dispose of our Graphics object
             gfx.Dispose();

             //return the image
             return bmp;
         }


         public string hasil2 (string text)
         {
             string hasils = text;
             return hasils;
         }

        public void StartOCR( Bitmap path)
        {
            const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";
            const string ts = @"C:\Users\WIWIK\Documents\Visual Studio 2013\Projects\TESSERACTPROJECT\TESSERACTPROJECT\tessdata";
            Bitmap original = new Bitmap(path);
            //Bitmap se = RotateImage(original, 90);

            //original.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
          //  Bitmap sc = this.ScaleByPercent(original, 300);

           // using (TesseractEngine processor = new TesseractEngine(ts, "eng", EngineMode.Default))

            //IPix image2 = Pix.LoadFromFile(image);

            //Tesseract.Page text = tess.Process(image2, rect, PageSegMode.Auto);
            
            using (TesseractEngine processor = new TesseractEngine(ts, "ind", EngineMode.Default))
            {
                //processor.SetVariable("tessedit_char_whitelist", "0123456789,.$¢");
                //processor.SetVariable("tessedit_char_whitelist", "xyz");
                //processor.SetVariable("classify_bln_numeric_mode", "1 or 0"); 
                //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                
                
                using (Bitmap bmp = original)
                {

                   

                    using (var page = processor.Process(PixConverter.ToPix(bmp),PageSegMode.Auto))
                    {
                        
                       hasiltext = page.GetText().Trim();
                        //string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " " };
                        //foreach (string s in charactersToReplace)
                        //{
                        //    text = text.Replace(s, "");
                        //}

                       txtarea.InnerText = hasiltext;
                      

                      // hasiltext = this.hasil2(text);
                       //convertjson(txtarea.InnerText);
                    } 
                }
            }
        }

        public class objects
        {
            public string hasil { get; set; }
        }

        

        public void convertjson()
        {
            objects s = new objects { hasil = txtarea.InnerText };
            string a = txtarea.InnerText;
            lbs.InnerText = s.ToString();
           // string op = this.hasil2();
           // objects s = new objects { hasil = hasiltext};
       //Response.Write("<script>alert('"+a+"');</script>");


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
                
                    StartOCR(bs);
                    process2(bs);
                
                //StartOCR(filepath);
                //process2(filepath);
                //string a =this.modi(filepath);
                //Textarea4.InnerText = a;

               // process3(filepath);
                //process4(filepath);
            }
            else
            {
                Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            }

           // process_2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //MemoryStream ms = new MemoryStream();
            //Bitmap newBitmap = new Bitmap(filepath);
            
            //newBitmap.SetResolution(72, 72);
            //newBitmap.Save(ms,System.Drawing.Imaging.ImageFormat.Png);

            //var base64Data = Convert.ToBase64String(ms.ToArray());
          //  img1.Src = "data:image/Jpeg;base64," + base64Data;
            //  string hasil = this.RecognizeTextFromImage(new Bitmap(filepath));

            //   resultText.InnerText = hasil;
          //  Bitmap x = new Bitmap(filepath);
         //  x.MakeTransparent(x.GetPixel(0, 0));
          //  StartOCR(filepath);
         //string a =this.modi(filepath);
         //Textarea4.InnerText = a;
          //  process2(filepath);
            //process3(filepath);
            //process4(filepath);
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            convertjson();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            convertxtoriginal();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{

        //    string folderpath = Server.MapPath("~/IMAGES/");
        //    /// string folderpath2 = Server.MapPath("~/IMAGES2/");
        //    if (!Directory.Exists(folderpath))
        //    {
        //        Directory.CreateDirectory(folderpath);
        //    }
        //    FileUpload1.SaveAs(folderpath + Path.GetFileName(FileUpload1.FileName));
        //    string filepath = folderpath + Path.GetFileName(FileUpload1.FileName);
        //    //string filepath2 = folderpath2 + Path.GetFileName(upload.FileName);
        //    Image1.ImageUrl = "~/IMAGES/" + Path.GetFileName(FileUpload1.FileName);

        //    string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
        //    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
        //    {
        //        Bitmap bs = new Bitmap(filepath);

        //        StartOCR180(bs);
        //        process2180(bs);

        //        //StartOCR(filepath);
        //        //process2(filepath);
        //        //string a =this.modi(filepath);
        //        //Textarea4.InnerText = a;

        //        //process3(filepath);
        //        //process4(filepath);
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

        //    }

        //    // process_2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    //MemoryStream ms = new MemoryStream();
        //    //Bitmap newBitmap = new Bitmap(filepath);

        //    //newBitmap.SetResolution(72, 72);
        //    //newBitmap.Save(ms,System.Drawing.Imaging.ImageFormat.Png);

        //    //var base64Data = Convert.ToBase64String(ms.ToArray());
        //    //  img1.Src = "data:image/Jpeg;base64," + base64Data;
        //    //  string hasil = this.RecognizeTextFromImage(new Bitmap(filepath));

        //    //   resultText.InnerText = hasil;
        //    //  Bitmap x = new Bitmap(filepath);
        //    //  x.MakeTransparent(x.GetPixel(0, 0));
        //    //  StartOCR(filepath);
        //    //string a =this.modi(filepath);
        //    //Textarea4.InnerText = a;
        //    //  process2(filepath);
        //    //process3(filepath);
        //    //process4(filepath);
        //}
    }
}