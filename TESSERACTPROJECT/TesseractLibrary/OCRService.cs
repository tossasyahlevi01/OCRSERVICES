using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Tesseract;
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



        public string process2(Bitmap p)
        {
            const string language = "eng";
            const string TessractData = @"c:\users\wiwik\documents\visual studio 2013\Projects\TESSERACTPROJECT\TesseractLibrary\tessdata";
            Bitmap process_2 = this.MakeGrayscale3(p);
            Bitmap process_3 = this.AdjustBrightnessContrast(process_2, 300, 100);
            using (TesseractEngine processor = new TesseractEngine(TessractData, "ind", EngineMode.Default))
            {            
                using (Bitmap bmp = process_2)
                {
                    using (var page = processor.Process(PixConverter.ToPix(bmp), PageSegMode.Auto))
                    {
                        string hasiltext = page.GetText();                       
                        return hasiltext;
                    }
                }
            }
        }

    }
}
