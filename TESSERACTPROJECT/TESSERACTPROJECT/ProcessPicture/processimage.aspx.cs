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
using System.Xml;
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
namespace TESSERACTPROJECT.ProcessPicture
{
    public partial class processimage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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

        public void process3(Bitmap p)
        {
          
            Bitmap process_3 = this.changetowhite(p);
            MemoryStream ms = new MemoryStream();
            process_3.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            img2.Src = "data:image/Jpeg;base64," + base64Data;
          
        }

        public void process2(Bitmap p)
        {
           
            Bitmap process_2 = this.MakeGrayscale3(new Bitmap(p));


            MemoryStream ms = new MemoryStream();

            process_2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            img1.Src = "data:image/Jpeg;base64," + base64Data;

           
            
        }

        public static Bitmap AdjustContrast(Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);

            return NewBitmap;
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


        public int contrast()
        {
            XmlDocument doc = new XmlDocument();
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "config.xml");
            XmlNode node = doc.DocumentElement.SelectSingleNode("/configurasi/contrast");
            int a = Convert.ToInt32(node.InnerText);
            return a;
        }

        public int brightness()
        {
            XmlDocument doc = new XmlDocument();
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "config.xml");
            XmlNode node = doc.DocumentElement.SelectSingleNode("/configurasi/brightness");
            int b = Convert.ToInt32(node.InnerText);
            return b;

        }


        public void contas2(Bitmap p)
        {
            Bitmap process_con = AdjustBrightnessContrast(new Bitmap(p), contrast(),brightness());


            MemoryStream ms = new MemoryStream();

            process_con.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            img3.Src = "data:image/Jpeg;base64," + base64Data;


        }

        public void processcontrast(Bitmap p)
        {
             
            Bitmap process_con = AdjustContrast(new Bitmap(p),10);


            MemoryStream ms = new MemoryStream();

            process_con.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            img3.Src = "data:image/Jpeg;base64," + base64Data;



        }

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            string folderpath = Server.MapPath("~/IMAGESCONVERT/");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            upload.SaveAs(folderpath + Path.GetFileName(upload.FileName));
            string filepath = folderpath + Path.GetFileName(upload.FileName);
            imgori.ImageUrl = "~/IMAGESCONVERT/" + Path.GetFileName(upload.FileName);

            string ext = System.IO.Path.GetExtension(upload.FileName);
            if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
            {
                process2(new Bitmap(filepath));

              
            }
            else
            {
                Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string folderpath = Server.MapPath("~/IMAGESCONVERT/");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            upload.SaveAs(folderpath + Path.GetFileName(upload.FileName));
            string filepath = folderpath + Path.GetFileName(upload.FileName);
            imgori.ImageUrl = "~/IMAGESCONVERT/" + Path.GetFileName(upload.FileName);

            string ext = System.IO.Path.GetExtension(upload.FileName);
            if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
            {
                process3(new Bitmap(filepath));


            }
            else
            {
                Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string folderpath = Server.MapPath("~/IMAGESCONVERT/");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            upload.SaveAs(folderpath + Path.GetFileName(upload.FileName));
            string filepath = folderpath + Path.GetFileName(upload.FileName);
            imgori.ImageUrl = "~/IMAGESCONVERT/" + Path.GetFileName(upload.FileName);

            string ext = System.IO.Path.GetExtension(upload.FileName);
            if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
            {
              // processcontrast(new Bitmap(filepath));
                contas2(new Bitmap(filepath));

            }
            else
            {
                Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            }

        }






    }
}