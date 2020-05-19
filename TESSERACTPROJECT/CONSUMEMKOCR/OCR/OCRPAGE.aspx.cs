using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TesseractLibrary;
using System.Text.RegularExpressions;
namespace CONSUMEMKOCR.OCR
{
    public partial class OCRPAGE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        OCRService OCR = new OCRService();

        protected void bn_Click(object sender, EventArgs e)
        {
            string folderpath = Server.MapPath("~/IMAGES/");
            /// string folderpath2 = Server.MapPath("~/IMAGES2/");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            fu.SaveAs(folderpath + Path.GetFileName(fu.FileName));
            string filepath = folderpath + Path.GetFileName(fu.FileName);
            //string filepath2 = folderpath2 + Path.GetFileName(upload.FileName);
            ims.ImageUrl = "~/IMAGES/" + Path.GetFileName(fu.FileName);

            string ext = System.IO.Path.GetExtension(fu.FileName);
            if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")
            {
              

                string hasil = OCR.process2(new Bitmap(filepath),4,5);
                var stringWithoutEmptyLines = hasil.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                string resultString = Regex.Replace(hasil, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                string[] sep = new string[] { "\n" };
                string[] lines = stringWithoutEmptyLines.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                nik.Text = lines[2].Trim();
                Nama.Text = lines[3].Trim();
                Alamat.Text = lines[6].Trim() + Environment.NewLine + lines[7].Trim() + Environment.NewLine + lines[8].Trim() + Environment.NewLine + lines[9].Trim();
               
                
                //}


            }
            else
            {
                Response.Write("<script>alert('File Extention Harus JPG || jpg || jpeg');</script>");

            }
        }
    }
}