using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using TesseractLibrary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using SERVICEMKOCR.Models;
using System.Xml;
using System.Text.RegularExpressions;
using SERVICEMKOCR.ModelRow;
namespace SERVICEMKOCR.Models
{
    public class Ijazah
    {
        public string GelarDepan { get; set; }
        public string NamaLengkap { get; set; }
        public string GelarBelakang { get; set; }
        public string ttl { get; set; }
        public string universitas { get; set; }
        public string NomorIjazah { get; set; }
        public string TanggalIjazah { get; set; }
        public string TahunTerbit { get; set; }
        public string error { get; set; }
        EkstraksiText ocrs = new EkstraksiText();
        IjazahRow IjazahRows = new IjazahRow();

        public IEnumerable<string> WordingProcess(string text)
        {
            string resultString = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline).Trim();
            string a = resultString.Replace("gol.darah", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("RTRW ", "").Replace("Kewarganegaraan", "").Replace("KeVDesa", "").Replace("RTRW", "").Replace("Gol Darah ", "").Replace("/", "").Replace("Jjene kelamin", "").Replace("Tempai TgiLahu", "").Replace("NIK", "").Replace("Nama", "").Replace("Agama", "").Replace("Pekerjaan", "").Replace("Status Perkawinan", "").Replace("KelDesa", "").Replace("Kecamatan", "").Replace("StatusPerkawinan.", "").Replace(":", "").Replace(".", "").Replace("RT/RW", "").Replace("-", "").Replace("TempaiTgiLahu-", "").Replace("Jjenekelamin", "").Replace("Gol.Darah", "").Replace("Almat", "").Replace("?", "").Replace("TempaiTgiLahu", "").Replace("MPIalLahu", "").Replace("0rKelamun", "").Replace("Alnal", "").Replace("HIRW", "").Replace("KetDesa", "").Replace("StatusPerkawinan", "").Replace("BerlakuHingga", "").Replace("GolDaran", "").Replace("TemparIgllahu—", "").Replace("seniskelamin", "").Replace("Alnat", "").Replace("TempatTgiLahi", "").Replace("Jeriskelamin", "");
            string[] sep = new string[] { "\n" };
            string[] lines = a.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            String[] sEmails = new String[lines.Count()];
            for (int ix = 0; ix < lines.Count(); ix++)
            {
                sEmails[ix] = lines[ix];

                yield return sEmails[ix];
            }
        }

        //Function Menambah Object Ijazah Pada Config
        public IEnumerable<int> RowIjazah()
        {
            XmlDocument doc = new XmlDocument();
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "DataRowIjazah.xml");
            XmlNode GelarDepanNode= doc.DocumentElement.SelectSingleNode("/DataIjazah/GelarDepan");
            XmlNode NamaLengkapNode = doc.DocumentElement.SelectSingleNode("/DataIjazah/NamaLengkap");
            XmlNode GelarBelakangNode = doc.DocumentElement.SelectSingleNode("/DataIjazah/GelarBelakang");
            XmlNode ttlNode = doc.DocumentElement.SelectSingleNode("/DataIjazah/ttl");
            XmlNode UniversitasNode = doc.DocumentElement.SelectSingleNode("/DataIjazah/Universitas");
            XmlNode NomorIjazahNode = doc.DocumentElement.SelectSingleNode("/DataIjazah/NomorIjazah");
            XmlNode TanggalIjazahNode = doc.DocumentElement.SelectSingleNode("/DataKTP/TanggalIjazah");
            XmlNode TahunTerbitNode = doc.DocumentElement.SelectSingleNode("/DataKTP/TahunTerbit");
         
            IjazahRows.GelarDepanRow= Convert.ToInt32(GelarDepanNode.InnerText);
            IjazahRows.NamaLengkapRow = Convert.ToInt32(NamaLengkapNode.InnerText);
            IjazahRows.GelarBelakangRow = Convert.ToInt32(GelarBelakangNode.InnerText);
            IjazahRows.ttlRow = Convert.ToInt32(ttlNode.InnerText);
            IjazahRows.universitasRow = Convert.ToInt32(NomorIjazahNode.InnerText);
            IjazahRows.NomorIjazahRow = Convert.ToInt32(NomorIjazahNode.InnerText);
            IjazahRows.TanggalIjazahRow = Convert.ToInt32(TanggalIjazahNode.InnerText);
            IjazahRows.TahunTerbitRow = Convert.ToInt32(TahunTerbitNode.InnerText);
           

            List<int> row = new List<int>();
            row.Add(IjazahRows.GelarDepanRow);
            row.Add(IjazahRows.NamaLengkapRow);
            row.Add(IjazahRows.GelarBelakangRow);
            row.Add(IjazahRows.ttlRow);
            row.Add(IjazahRows.universitasRow);
            row.Add(IjazahRows.NomorIjazahRow);
            row.Add(IjazahRows.TanggalIjazahRow);
            row.Add(IjazahRows.TahunTerbitRow);
          
            return row;

        }


        public List<string> IjazahOCRService(string namafile, string jenis)
        {
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/" + namafile);
            string hasil = ocrs.NormalBackground(new Bitmap(paths));
            IEnumerable<string> hasil2 = WordingProcess(hasil);
            return hasil2.ToList();

        }

        ////Proses OCR Ijazah UPLOAD
        //public Ijazah ProsesIjazahUpload(string namafile, string jenis)
        //{
        //    if (jenis == "Ijazah")
        //    {
        //        try
        //        {
        //            IEnumerable<int> rowdata = RowIjazah();
        //            var s = rowdata.ToList();
        //            String[] datar = new String[rowdata.Count()];
        //            IEnumerable<string> hasil = UploadIjazah(jenis, namafile);
        //            var GelarDepanData = hasil.Skip(s[0]).Take(1).FirstOrDefault();
        //            var NamaLengkapData = hasil.Skip(s[1]).Take(1).FirstOrDefault();
        //            var GelarBelakangData = hasil.Skip(s[2]).Take(1).FirstOrDefault();
        //            var ttlData = hasil.Skip(s[3]).Take(1).FirstOrDefault();
        //            var UniversitasData = hasil.Skip(s[4]).Take(1).FirstOrDefault();
        //            var NomorIjazahData = hasil.Skip(s[5]).Take(1).FirstOrDefault();
        //            var TanggalIjazahData = hasil.Skip(s[6]).Take(1).FirstOrDefault();
        //            var TahunTerbitIjazahData = hasil.Skip(s[7]).Take(1).FirstOrDefault();

        //            Ijazah data = new Ijazah
        //            { 
        //                GelarDepan=GelarDepanData, NamaLengkap=NamaLengkapData,GelarBelakang=GelarBelakangData,
        //                ttl=ttlData, 
        //                universitas=UniversitasData, NomorIjazah=NomorIjazahData, TanggalIjazah=TanggalIjazahData, TahunTerbit=TahunTerbitIjazahData
        //            };
        //            return data;
        //        }
        //        catch (Exception ex)
        //        {
        //            Ijazah data = new Ijazah { error = ex.Message };
        //            return data;
        //        }
        //    }
        //    else
        //    {
        //        Ijazah ks = new Ijazah { error = "Fungsi Ini Hanya Untuk Ijazah" };
        //        return ks;
        //    }
        //}

        public string cekbackground(string namafile)
        {
             var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/"+"//"+namafile);

            bool cek = IsBackgroundWhite(new Bitmap(paths));
            if (cek==true)
            {
                return "White";
            }
            else
            {
                return "Bukan White";
            }
        }

        public bool IsBackgroundWhite(Bitmap theImageBitmap)
        {
            Bitmap bmp = new Bitmap(theImageBitmap);
            int weight = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                weight += GetWeight(bmp.GetPixel(x, 0));
                weight += GetWeight(bmp.GetPixel(x, bmp.Height - 1));
            }

            for (int y = 0; y < bmp.Height; y++)
            {
                weight += GetWeight(bmp.GetPixel(0, y));
                weight += GetWeight(bmp.GetPixel(bmp.Width - 1, y));
            }

            if (weight > 255)
                return true;
            return false;
        }

        private int GetWeight(Color c)
{
    if (c.R >= 200 && c.B >= 200 && c.G >= 200)
    {
        int n1 = 255 - c.R;
        int n2 = 255 - c.G;
        int n3 = 255 - c.B;

        return (int)((n1 + n2 + n3) / 3);
    }
            else
    {
        return 0;
    }
}


        //public IEnumerable<string> UploadIjazah(string jenis, string namafile)
        //{
        //    string filename;
        //    var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/");
        //    Uri uri = new Uri(namafile);
        //    using (WebClient client = new WebClient())
        //    {
        //        client.DownloadFile(new Uri(namafile), paths + System.IO.Path.GetFileName(uri.LocalPath));
        //    }
        //    var paths2 = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/" + System.IO.Path.GetFileName(uri.LocalPath));
        //    string hasil = ocrs.process2(new Bitmap(paths2),4,5);
        //    IEnumerable<string> hasil2 = WordingProcess(hasil);
        //    return hasil2;

        //}

        //end





    }
}