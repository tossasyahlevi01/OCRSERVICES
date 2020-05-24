using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Tesseract;
using System.Web;
using System.Drawing.Drawing2D;

using Accord;
using Accord.Imaging.Formats;
using Accord.Imaging.Converters;
using Accord.Imaging.ColorReduction;
using System.Text.RegularExpressions;
using Accord.Imaging.ComplexFilters;
using AForge;
using System.Web.Http;
using Newtonsoft.Json;
using System.Xml;
using System.Net;
using System.IO;
using System.Data;
using Accord.Imaging.Filters;
using AForge.Math;
using Accord.Imaging;

namespace TesseractLibrary
{
    public class OCRService
    {

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

        public string bahasa()
        {
            XmlDocument doc = new XmlDocument();
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "config.xml");
            XmlNode node = doc.DocumentElement.SelectSingleNode("/configurasi/bahasa");
            string b = node.InnerText;
            return b;

        }

        public Bitmap setresolusi(Bitmap im, int width, int height)
        {
            System.Drawing.Bitmap b0 = new Bitmap(im);
            double scale = width / height;
            int x = (int)(b0.Width * scale);
            int y = (int)(b0.Height * scale);
            return b0;
        }

        public string NormalBackground(Bitmap p)
        {
            var TessractData = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata");
            using (TesseractEngine processor = new TesseractEngine(TessractData, bahasa(), EngineMode.Default))
            {
                using (Bitmap bmp = p)
                {
                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        string hasiltext = page.GetText();
                        return hasiltext;
                    }
                }
            }
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

        public static System.Drawing.Image Resize(System.Drawing.Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }

        public  Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            destImage.SetResolution(96.0F, 96.0F);
            return destImage;
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


        public string process2(Bitmap p, int widt, int height)
        {
            var TessractData = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata");
             
            //  Bitmap p1 = this.setresolusi(p, widt, height);
           // Bitmap p2 = new Bitmap(p, new Size(680, 421));
            Bitmap px = this.ResizeImage(new Bitmap(p), 680, 421);
           // Bitmap desk = this.DeskewImage(px);
           Bitmap process_2 = this.MakeGrayscale3(px);
          // Bitmap bw = this.make_bw(process_2);

         //   Bitmap process_3 = this.AdjustBrightnessContrast(desk, contrast(), brightness());
            
            //Bitmap p1 = this.setresolusi(process_2, widt, height);
            // Bitmap bw = this.make_bw(process_2);
            // p.MakeTransparent(Color.White);

            //Bitmap process_3 = this.AdjustBrightnessContrast(new Bitmap(process_2), 300, 200);

            using (TesseractEngine processor = new TesseractEngine(TessractData, bahasa(), EngineMode.Default))
            {
                using (Bitmap bmp = process_2)
                {
                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

                        string hasiltext = page.GetText();

                        return hasiltext;
                    }
                }
            }
        }

                // public string  process2(Bitmap p, int widt, int height)
                // {
                //     var TessractData = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata");
                //   //  Bitmap desk = this.DeskewImage(p);
                // //  Bitmap p1 = this.setresolusi(p, widt, height);
                //   Bitmap p2 = new Bitmap (p, new Size(680,421));

                //     Bitmap process_2 = this.MakeGrayscale3(p2);
                //     //Bitmap p1 = this.setresolusi(process_2, widt, height);
                //    // Bitmap bw = this.make_bw(process_2);
                //// p.MakeTransparent(Color.White);

                //    // Bitmap process_3 = this.AdjustBrightnessContrast(process_2, contrast() , brightness());
                //     //Bitmap process_3 = this.AdjustBrightnessContrast(new Bitmap(process_2), 300, 200);

                //     using (TesseractEngine processor = new TesseractEngine(TessractData, bahasa(), EngineMode.Default))
                //     {            
                //         using (Bitmap bmp = process_2)
                //         {
                //             using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                //             {
                //                 //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                //                 //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");




                //                 string hasiltext = page.GetText();

                //                     // processor.SetVariable("tessedit_char_whitelist", "0123456789,.$¢TempauTgi LahuATRW ——  —  Jaris kelamin  ");
                //                     return hasiltext;
                //                 }
                //             }
                //         }
                //     }
            }

        }
    



