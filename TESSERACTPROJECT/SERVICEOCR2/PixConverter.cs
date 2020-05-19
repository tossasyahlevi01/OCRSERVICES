using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesseract;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SERVICEOCR2
{
    public class PixConverter
    {
        private static readonly BitmapToPixConverter bitmapConverter = new BitmapToPixConverter();
        private static readonly PixToBitmapConverter pixConverter = new PixToBitmapConverter();

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