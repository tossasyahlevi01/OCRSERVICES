using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace TesseractLibrary
{
   public  class PixConverter
    {

        private static readonly BitmapToPixConverter bitmapConverter = new BitmapToPixConverter();
        private static readonly PixToBitmapConverter pixConverter = new PixToBitmapConverter();

        /// <summary>
        /// Converts the specified <paramref name="pix"/> to a Bitmap.
        /// </summary>
        /// <param name="pix">The source image to be converted.</param>
        /// <returns>The converted pix as a <see cref="Bitmap"/>.</returns>
        public static Bitmap ToBitmap(Pix pix)
        {
            return pixConverter.Convert(pix);
        }

        /// <summary>
        /// Converts the specified <paramref name="img"/> to a Pix.
        /// </summary>
        /// <param name="img">The source image to be converted.</param>
        /// <returns>The converted bitmap image as a <see cref="Pix"/>.</returns>
        public static Pix ToPix(Bitmap img)
        {
            return bitmapConverter.Convert(img);
        }
    }
}
