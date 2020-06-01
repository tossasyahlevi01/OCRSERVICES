using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Tesseract;
using AForge.Imaging.Filters;
using System.Web;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Accord.Imaging.Filters;
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
    public class EkstraksiText
    {
        public string x { get; set; }
        public ResultIterator xx { get; set; }
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


        public Bitmap Crop(string img, int width, int height, int x, int y)
        {

            System.Drawing.Image image = System.Drawing.Image.FromFile(img);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmp.SetResolution(80, 60);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            image.Dispose();
            bmp.Dispose();
            gfx.Dispose();


            return bmp;
        }
        public static Bitmap MakeGrayscale3(Bitmap original)
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

        public static Bitmap ResizeImage(Bitmap image, int width, int height)
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
            Accord.Imaging.Filters.Grayscale filter = new Accord.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721);
            workmap = filter.Apply(tempWorkmap);
            double angle = sc.GetSkewAngle(workmap);
            Accord.Imaging.Filters.RotateBilinear rf = new Accord.Imaging.Filters.RotateBilinear(-angle);
            rf.FillColor = Color.White;
            workmap1 = rf.Apply(workmap);
            return workmap1;
        }


        public static PGM ApplyMedianFilter(PGM inputImage, int iterationCount)
        {
            PGM outputImage = new PGM(inputImage.Size.Width, inputImage.Size.Height);

            for (int iteration = 0; iteration < iterationCount; iteration++)
            {
                for (int x = 0; x < inputImage.Size.Width - 1; x++)
                {
                    for (int y = 0; y < inputImage.Size.Height - 1; y++)
                    {
                        //get all neighbors of current-pixel
                        List<int> validNeighbors = inputImage.GetAllNeighbors(x, y);

                        //sort and find middle intensity value from all neighbors
                        validNeighbors.Sort();
                        int medianIndex = validNeighbors.Count / 2;
                        short medianPixel = (short)validNeighbors[medianIndex];

                        //set median-pixel as intensity of current-pixel
                        outputImage.Pixels[x, y] = medianPixel;
                    }
                }

                //set input to next iteration
                inputImage = outputImage.Clone();
            }

            return (outputImage);
        }


        public static double[,] GaussianBlur(int lenght, double weight)
        {
            double[,] kernel = new double[lenght, lenght];
            double kernelSum = 0;
            int foff = (lenght - 1) / 2;
            double distance = 0;
            double constant = 1d / (2 * Math.PI * weight * weight);
            for (int y = -foff; y <= foff; y++)
            {
                for (int x = -foff; x <= foff; x++)
                {
                    distance = ((y * y) + (x * x)) / (2 * weight * weight);
                    kernel[y + foff, x + foff] = constant * Math.Exp(-distance);
                    kernelSum += kernel[y + foff, x + foff];
                }
            }
            for (int y = 0; y < lenght; y++)
            {
                for (int x = 0; x < lenght; x++)
                {
                    kernel[y, x] = kernel[y, x] * 1d / kernelSum;
                }
            }
            return kernel;
        }

        public static Bitmap Convolve(Bitmap srcImage, double[,] kernel)
        {
            int width = srcImage.Width;
            int height = srcImage.Height;
            BitmapData srcData = srcImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int bytes = srcData.Stride * srcData.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            srcImage.UnlockBits(srcData);
            int colorChannels = 3;
            double[] rgb = new double[colorChannels];
            int foff = (kernel.GetLength(0) - 1) / 2;
            int kcenter = 0;
            int kpixel = 0;
            for (int y = foff; y < height - foff; y++)
            {
                for (int x = foff; x < width - foff; x++)
                {
                    for (int c = 0; c < colorChannels; c++)
                    {
                        rgb[c] = 0.0;
                    }
                    kcenter = y * srcData.Stride + x * 4;
                    for (int fy = -foff; fy <= foff; fy++)
                    {
                        for (int fx = -foff; fx <= foff; fx++)
                        {
                            kpixel = kcenter + fy * srcData.Stride + fx * 4;
                            for (int c = 0; c < colorChannels; c++)
                            {
                                rgb[c] += (double)(buffer[kpixel + c]) * kernel[fy + foff, fx + foff];
                            }
                        }
                    }
                    for (int c = 0; c < colorChannels; c++)
                    {
                        if (rgb[c] > 255)
                        {
                            rgb[c] = 255;
                        }
                        else if (rgb[c] < 0)
                        {
                            rgb[c] = 0;
                        }
                    }
                    for (int c = 0; c < colorChannels; c++)
                    {
                        result[kcenter + c] = (byte)rgb[c];
                    }
                    result[kcenter + 3] = 255;
                }
            }
            Bitmap resultImage = new Bitmap(width, height);
            BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, resultData.Scan0, bytes);
            resultImage.UnlockBits(resultData);
            return resultImage;
        }


        public static Bitmap Sharpen(Bitmap image)
        {
            Bitmap sharpenImage = (Bitmap)image.Clone();

            int filterWidth = 3;
            int filterHeight = 3;
            int width = image.Width;
            int height = image.Height;

            // Create sharpening filter.
            double[,] filter = new double[filterWidth, filterHeight];
            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;

            double factor = 1.0;
            double bias = 0.0;

            Color[,] result = new Color[image.Width, image.Height];

            // Lock image bits for read/write.
            BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Declare an array to hold the bytes of the bitmap.
            int bytes = pbits.Stride * height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

            int rgb;
            // Fill the color array with the new sharpened color values.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + width) % width;
                            int imageY = (y - filterHeight / 2 + filterY + height) % height;

                            rgb = imageY * pbits.Stride + 3 * imageX;

                            red += rgbValues[rgb + 2] * filter[filterX, filterY];
                            green += rgbValues[rgb + 1] * filter[filterX, filterY];
                            blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                        }
                        int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb(r, g, b);
                    }
                }
            }

            // Update the image with the sharpened pixels.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    rgb = y * pbits.Stride + 3 * x;

                    rgbValues[rgb + 2] = result[x, y].R;
                    rgbValues[rgb + 1] = result[x, y].G;
                    rgbValues[rgb + 0] = result[x, y].B;
                }
            }

            // Copy the RGB values back to the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
            // Release image bits.
            sharpenImage.UnlockBits(pbits);

            return sharpenImage;
        }


        public static Bitmap Sharpen2(Bitmap image, double strength)
        {
            using (var bitmap = image as Bitmap)
            {
                if (bitmap != null)
                {
                    var sharpenImage = bitmap.Clone() as Bitmap;

                    int width = image.Width;
                    int height = image.Height;

                    // Create sharpening filter.
                    const int filterSize = 5;

                    var filter = new double[,]
                {
                    {-1, -1, -1, -1, -1},
                    {-1,  2,  2,  2, -1},
                    {-1,  2, 16,  2, -1},
                    {-1,  2,  2,  2, -1},
                    {-1, -1, -1, -1, -1}
                };

                    double bias = 1.0 - strength;
                    double factor = strength / 16.0;

                    const int s = filterSize / 2;

                    var result = new Color[image.Width, image.Height];

                    // Lock image bits for read/write.
                    if (sharpenImage != null)
                    {
                        BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height),
                                                                    ImageLockMode.ReadWrite,
                                                                    PixelFormat.Format24bppRgb);

                        // Declare an array to hold the bytes of the bitmap.
                        int bytes = pbits.Stride * height;
                        var rgbValues = new byte[bytes];

                        // Copy the RGB values into the array.
                        Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

                        int rgb;
                        // Fill the color array with the new sharpened color values.
                        for (int x = s; x < width - s; x++)
                        {
                            for (int y = s; y < height - s; y++)
                            {
                                double red = 0.0, green = 0.0, blue = 0.0;

                                for (int filterX = 0; filterX < filterSize; filterX++)
                                {
                                    for (int filterY = 0; filterY < filterSize; filterY++)
                                    {
                                        int imageX = (x - s + filterX + width) % width;
                                        int imageY = (y - s + filterY + height) % height;

                                        rgb = imageY * pbits.Stride + 3 * imageX;

                                        red += rgbValues[rgb + 2] * filter[filterX, filterY];
                                        green += rgbValues[rgb + 1] * filter[filterX, filterY];
                                        blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                                    }

                                    rgb = y * pbits.Stride + 3 * x;

                                    int r = Math.Min(Math.Max((int)(factor * red + (bias * rgbValues[rgb + 2])), 0), 255);
                                    int g = Math.Min(Math.Max((int)(factor * green + (bias * rgbValues[rgb + 1])), 0), 255);
                                    int b = Math.Min(Math.Max((int)(factor * blue + (bias * rgbValues[rgb + 0])), 0), 255);

                                    result[x, y] = Color.FromArgb(r, g, b);
                                }
                            }
                        }

                        // Update the image with the sharpened pixels.
                        for (int x = s; x < width - s; x++)
                        {
                            for (int y = s; y < height - s; y++)
                            {
                                rgb = y * pbits.Stride + 3 * x;

                                rgbValues[rgb + 2] = result[x, y].R;
                                rgbValues[rgb + 1] = result[x, y].G;
                                rgbValues[rgb + 0] = result[x, y].B;
                            }
                        }

                        // Copy the RGB values back to the bitmap.
                        Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
                        // Release image bits.
                        sharpenImage.UnlockBits(pbits);
                    }

                    return sharpenImage;
                }
            }
            return null;
        }


        public string process22(Bitmap p, int widt, int height)
        {
            var TessractData = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata");

            
            Bitmap px = ResizeImage(new Bitmap(p), 700, 700);
        
            Bitmap sharp2 =Sharpen2(px, 1);
            Bitmap process_2 = MakeGrayscale3(sharp2);

           

         
            //Bitmap process_3 = this.AdjustBrightnessContrast(new Bitmap(process_2), 300, 200);

            using (TesseractEngine processor = new TesseractEngine(TessractData, bahasa(), EngineMode.TesseractAndLstm))
            {
                using (Bitmap bmp = process_2)
                {
                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        page.RegionOfInterest = new Rect(100, 100, 500, 500);

                        var ip = page.GetIterator();
                        string t = ip.GetText(PageIteratorLevel.Block);
                        return t;


                           

                        }
                       

                    }
                }
            }


        public static Bitmap Rescale(Bitmap image, int dpiX, int dpiY)
        {
            Bitmap bm = new Bitmap((int)(image.Width * dpiX /
            image.HorizontalResolution), (int)(image.Height * dpiY /
            image.VerticalResolution));
            bm.SetResolution(dpiX, dpiY);
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = InterpolationMode.Bicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(image, 0, 0);
            g.Dispose();

            return bm;
        }

        public string whiteprocess2(Bitmap p)
        {
            var TessractData = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata");

            //  Bitmap p1 = this.setresolusi(p, widt, height);
            // Bitmap p2 = new Bitmap(p, new Size(680, 421));
            Bitmap px = ResizeImage(p, 800, 600);
            //Bitmap sx = Rescale(new Bitmap(p), 300, 300);
            // Bitmap desk = this.DeskewImage(px);
           Bitmap sharp2 = Sharpen2(px, 1);
            //Bitmap process_2 = this.MakeGrayscale3(sharp2);

            // Bitmap sharp = Sharpen(process_2);


         // Bitmap bw = this.make_bw(sharp2);

             Bitmap process_3 = this.AdjustBrightnessContrast(sharp2, contrast(), brightness());

            // PGM sd = ApplyMedianFilter(new PGM(p), 5);
            //Bitmap p1 = this.setresolusi(process_2, widt, height);
            // Bitmap bw = this.make_bw(process_2);
            // p.MakeTransparent(Color.White);

            //Bitmap process_3 = this.AdjustBrightnessContrast(new Bitmap(process_2), 300, 200);

            using (TesseractEngine processor = new TesseractEngine(TessractData, bahasa(), EngineMode.Default))
            {
                using (Bitmap bmp = process_3)
                {
                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        // page.RegionOfInterest = new Rect(100, 100, 500, 500);
                        //page.GetThresholdedImage();

                        string p1x = page.GetText();
                        return p1x;
                        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

                        // string hasiltext = page.GetText();

                    }
                }
            }
        }


        private Bitmap DeCaptcha(Bitmap img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp = bmp.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            AForge.Imaging.Filters.Erosion erosion = new AForge.Imaging.Filters.Erosion();
            Dilatation dilatation = new Dilatation();
            AForge.Imaging.Filters.Invert inverter = new AForge.Imaging.Filters.Invert();
            AForge.Imaging.Filters.ColorFiltering cor = new AForge.Imaging.Filters.ColorFiltering();
            cor.Blue = new AForge.IntRange(200, 255);
            cor.Red = new AForge.IntRange(200, 255);
            cor.Green = new AForge.IntRange(200, 255);
            AForge.Imaging.Filters.Opening open = new AForge.Imaging.Filters.Opening();
            AForge.Imaging.Filters.BlobsFiltering bc = new AForge.Imaging.Filters.BlobsFiltering() { MinHeight = 10 };
            AForge.Imaging.Filters.Closing close = new AForge.Imaging.Filters.Closing();
            AForge.Imaging.Filters.GaussianSharpen gs = new AForge.Imaging.Filters.GaussianSharpen();
            AForge.Imaging.Filters.ContrastCorrection cc = new AForge.Imaging.Filters.ContrastCorrection();
            AForge.Imaging.Filters.FiltersSequence seq = new AForge.Imaging.Filters.FiltersSequence(gs, inverter, open, inverter, bc, inverter, open, cc, cor, bc, inverter);
            return seq.Apply(bmp);

            // pictureBox1.Image = seq.Apply(bmp);
            //  return OCR((Bitmap)pictureBox1.Image);
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

        public string process2(Bitmap p, string fileName)
        {
            var TessractData = System.Web.Hosting.HostingEnvironment.MapPath("~/tessdata");
             
            //  Bitmap p1 = this.setresolusi(p, widt, height);
           // Bitmap p2 = new Bitmap(p, new Size(680, 421));
            Bitmap px = ResizeImage(p, 1200, 780);
          //  System.Drawing.Image image = System.Drawing.Image.FromFile(fileName);

         //   string nama = fileName + "ASU" + ".jpg";
              //  new Bitmap(image, 800, 600).Save(nama);
            
            //Bitmap sx = Rescale(new Bitmap(p), 300, 300);
           // Bitmap desk = this.DeskewImage(px);
        Bitmap sharp2 = Sharpen2(px, 1);

          //  Bitmap caps = DeCaptcha(sharp2);

           Bitmap process_2 = MakeGrayscale3(sharp2);


         //  Bitmap black = this.converttobw(p);
          // Bitmap sharp = Sharpen(process_2);

          
          // Bitmap bw = this.make_bw(px);

      //  Bitmap process_3 = this.AdjustBrightnessContrast(process_2, contrast(), brightness());

          // PGM sd = ApplyMedianFilter(new PGM(p), 5);
            //Bitmap p1 = this.setresolusi(process_2, widt, height);
            // Bitmap bw = this.make_bw(process_2);
            // p.MakeTransparent(Color.White);

            //Bitmap process_3 = this.AdjustBrightnessContrast(new Bitmap(process_2), 300, 200);

            using (TesseractEngine processor = new TesseractEngine(TessractData, bahasa(),  EngineMode.Default))
            {
                using (Bitmap bmp =  process_2)
                  
                {
                    processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                  processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                    processor.SetVariable("tessedit_char_whitelist", "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                    processor.SetVariable("tessedit_unrej_any_wd", true);
                    processor.SetVariable("tessedit_char_whitelist", "0123456789,.$¢");
                    processor.SetVariable("tessedit_char_whitelist", "xyz");
                    processor.SetVariable("classify_bln_numeric_mode", "1 ");
                   processor.SetVariable("load_system_dawg	", "0");
                    //Rect region;
                  //  var data = new Rect(30,20, 800, 600);

                    using (var page = processor.Process(PixConverter.ToPix(bmp) , PageSegMode.AutoOsd))
                    {
                       // page.RegionOfInterest = new Rect(100, 100, 100, 100);
                        //page.GetThresholdedImage();
                      
                        var p1x = page.GetText();
                       // var ps = page.GetHOCRText(pageNum - 1);

                        bmp.Dispose();
                        process_2.Dispose();
                        sharp2.Dispose();
                        px.Dispose();
                        return p1x;
                        //processor.SetVariable("tessedit_char_whitelist", "!@#$%^&*()_+=-[]}{;:'\"\\|~`,./<>?");

                        //processor.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");

                       // string hasiltext = page.GetText();
                   
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
    



