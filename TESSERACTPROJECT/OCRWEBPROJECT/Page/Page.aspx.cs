using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using AForge.Imaging.Filters;
using TesseractLibrary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using OCRWEBPROJECT.Models;
namespace OCRWEBPROJECT.Page
{
    public partial class Page : System.Web.UI.Page
    {
        Data d = new Data();
        OCRService ocrs = new OCRService();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        


        public IEnumerable<string> WordingProcess2(string text)
        {
            string resultString = Regex.Replace(text, @"^\s+$\w\d[\r\n]*", string.Empty, RegexOptions.Multiline);
            string a = resultString.Replace("ama","").Replace("RTRW","").Replace("RTRW  ","").Replace("TemparTgl Lahir","").Replace("RTURW","").Replace("TemparTgiLahir", "").Replace("KeVvDesa", "").Replace("!", "").Replace("IK", "").Replace("BIk", "").Replace("RTIRW", "").Replace("Gol Darah", "").Replace("GA1", "").Replace("MIK", "").Replace("NIK", "").Replace("TempaiTgi Lahir", "").Replace("TempatTgi Lahir “", "").Replace("Gol Darah", "").Replace("wnmsnusu", "").Replace("TempatTgi Lahir", "").Replace("—", "").Replace("(M", "").Replace("Jeniskelamin", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("KevDesa", "").Replace("senis Kelamn — ", "").Replace("denisKelamin ", "").Replace("Name", "").Replace("KellDesa", "").Replace("Gol Darah", "").Replace("Gol Darah ", "").Replace("& ne Kelaman", "").Replace("Mama", "").Replace("ATAW", "").Replace("Status Perkawmnan", "").Replace("ampatTgi Lahu", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("Name", "").Replace("'erkawinan", "").Replace("Jenis kelamin", "").Replace("RTIW", "").Replace("TempatTgi Lahir", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("GoiDarah O", "").Replace("Kelesa — ", "").Replace("Status Perkawinar", "").Replace("gol.darah", "").Replace("Alamat", "").Replace("Berlaku Hingga", "").Replace("Jenis Kelamin", "").Replace("TempauTgi Lahir", "").Replace("RTAW", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("RTRW ", "").Replace("Kewarganegaraan", "").Replace("KeVDesa", "").Replace("RTRW", "").Replace("Gol Darah ", "").Replace("/", "").Replace("Jjene kelamin", "").Replace("Tempai TgiLahu", "").Replace("NIK", "").Replace("Nama", "").Replace("Agama", "").Replace("Pekerjaan", "").Replace("Status Perkawinan", "").Replace("KelDesa", "").Replace("Kecamatan", "").Replace("StatusPerkawinan.", "").Replace(":", "").Replace(".", "").Replace("RT/RW", "").Replace("TempaiTgiLahu-", "").Replace("Jjenekelamin", "").Replace("Gol.Darah", "").Replace("Almat", "").Replace("?", "").Replace("TempaiTgiLahu", "").Replace("MPIalLahu", "").Replace("0rKelamun", "").Replace("Alnal", "").Replace("HIRW", "").Replace("KetDesa", "").Replace("StatusPerkawinan", "").Replace("BerlakuHingga", "").Replace("GolDaran", "").Replace("TemparIgllahu—", "").Replace("seniskelamin", "").Replace("Alnat", "").Replace("TempatTgiLahi", "").Replace("Jeriskelamin", "");
            string[] sep = new string[] { "\n" };
            string[] lines = a.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            String[] sEmails = new String[lines.Count()];
            for (int ix = 0; ix < lines.Count(); ix++)
            {
                sEmails[ix] = lines[ix].Trim();

                yield return sEmails[ix];
            }
        }



        public  IEnumerable<string>GetWhiteOCR(string namafile)
        {
          //  var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES2/" + namafile);
            string hasil = ocrs.process2(new Bitmap(namafile), namafile);
            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;
        }


        public string urlgambar()
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
            pb.ImageUrl = "~/IMAGES/" + Path.GetFileName(upload.FileName);

            string ext = System.IO.Path.GetExtension(upload.FileName);
            upload.Dispose();
            return filepath;
        }

        public Data ds()
        {

            string urls = urlgambar();
            IEnumerable<string> data = GetWhiteOCR(urls);

            data.ToList();
            //string xz = data.Skip(2).Take(1).FirstOrDefault();
            //string p = xz.Replace("IK", string.Empty);

            //string z = data.Skip(5).Take(1).FirstOrDefault();
            //string pz = z.Replace("Gol Darah", string.Empty);

            //string pl = data.Skip(4).Take(1).FirstOrDefault();
            //string plx = pl.Replace("TempaiTgi Lahir ", string.Empty);

            //string lx = data.Skip(7).Take(1).FirstOrDefault();
            //string lxx = lx.Replace("RTRW ", string.Empty);

            //string po = data.Skip(8).Take(1).FirstOrDefault();
            //string pox = po.Replace("KeVDesa", string.Empty);


      //           //if (data.Skip(0).Take(1).FirstOrDefault().Contains("Provinsi"))
      //           //{
                     Data d = new Data
                     {
                         provinsi = data.Skip(0).Take(1).FirstOrDefault(),
                         kota = data.Skip(1).Take(1).FirstOrDefault(),
                         nik = data.Skip(2).Take(1).FirstOrDefault(),
                         nama = data.Skip(3).Take(1).FirstOrDefault(),
                         ttl = data.Skip(4).Take(1).FirstOrDefault(),
                         jkel = data.Skip(5).Take(1).FirstOrDefault(),
                         alamat = data.Skip(6).Take(1).FirstOrDefault(),
                         rtrw = data.Skip(7).Take(1).FirstOrDefault(),
                         keldesa = data.Skip(8).Take(1).FirstOrDefault(),
                         kecamatan = data.Skip(9).Take(1).FirstOrDefault(),
                         agama = data.Skip(10).Take(1).FirstOrDefault(),
                         statuskawin = data.Skip(11).Take(1).FirstOrDefault(),
                         pekerjaan = data.Skip(12).Take(1).FirstOrDefault(),
                         kewarganegaraan = data.Skip(13).Take(1).FirstOrDefault(),
                         berlaku = data.Skip(14).Take(1).FirstOrDefault()


                     };
                     return d;
                 }
          //else
          //       {
          //           Data d = new Data { error = "Posisi Pengambilan Gambar Tidak Valid" };
                    
          //           return d;
          //       }


      private Bitmap DeCaptcha(Bitmap img)
      {
          Bitmap bmp = new Bitmap(img);
          bmp = bmp.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
          Erosion erosion = new Erosion();
          Dilatation dilatation = new Dilatation();
          Invert inverter = new Invert();
          ColorFiltering cor = new ColorFiltering();
          cor.Blue = new AForge.IntRange(200, 255);
          cor.Red = new AForge.IntRange(200, 255);
          cor.Green = new AForge.IntRange(200, 255);
          Opening open = new Opening();
          BlobsFiltering bc = new BlobsFiltering() { MinHeight = 10 };
          Closing close = new Closing();
          GaussianSharpen gs = new GaussianSharpen();
          ContrastCorrection cc = new ContrastCorrection();
          FiltersSequence seq = new FiltersSequence(gs, inverter, open, inverter, bc, inverter, open, cc, cor, bc, inverter);
          return seq.Apply(bmp);
          
          // pictureBox1.Image = seq.Apply(bmp);
        //  return OCR((Bitmap)pictureBox1.Image);
      }
        
        protected void btn_Click(object sender, EventArgs e)
        {
            //string urls = urlgambar();
            //string x = GetWhiteOCR(urls);

            //provinsi.Value = x;
            Data pdata = ds();

            provinsi.Value = pdata.provinsi;
            kota.Value = pdata.kota;
            nik.Value = pdata.nik;
            nama.Value = pdata.nama;
            ttl.Value = pdata.ttl;
            jkel.Value = pdata.jkel;
            alamat.Value = pdata.alamat;
            rtrw.Value = pdata.rtrw;
            keldesa.Value = pdata.keldesa;
            kecamatan.Value = pdata.kecamatan;
            agama.Value = pdata.agama;
            statuskawin.Value = pdata.statuskawin;

            pekerjaan.Value = pdata.pekerjaan;
            kewarganegaraan.Value = pdata.kewarganegaraan;
            berlaku.Value = pdata.berlaku;
        }
    }
}