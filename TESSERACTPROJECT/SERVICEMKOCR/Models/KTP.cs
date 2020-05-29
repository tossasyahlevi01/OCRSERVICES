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
using System.ServiceProcess;
namespace SERVICEMKOCR.Models
{
    
    public class KTP
    {
        //public string line0 { get; set; }
        //public string line1 { get; set; }
        //public string line2 { get; set; }
        //public string line3 { get; set; }
        //public string line4 { get; set; }
        //public string line5 { get; set; }
        //public string line6 { get; set; }
        //public string line7 { get; set; }
        //public string line8 { get; set; }
        //public string line9 { get; set; }
        //public string line10 { get; set; }
        //public string line11{ get; set; }
        //public string line12 { get; set; }
        //public string line13 { get; set; }
        //public string line14 { get; set; }
        //public string line15 { get; set; }
        //public string line16  { get; set; }
        //public string line17 { get; set; }

        public string provinsi { get; set; }
        public string kota { get; set; }
        public string nik { get; set; }
        public string nama { get; set; }
        public string ttl { get; set; }
        public string jkel { get; set; }
        public string alamat { get; set; }
        public string rtrw { get; set; }
        public string keldesa { get; set; }
        public string kecamatan { get; set; }
        public string agama { get; set; }
        public string statuskawin { get; set; }
        public string pekerjaan { get; set; }
        public string kewarganegaraan { get; set; }
        public string berlaku { get; set; }

        //public string lineprovinsi { get; set; }
        //public string linekota { get; set; }
        //public string linenik { get; set; }
        //public string linenama { get; set; }
        //public string linenamab { get; set; }
        //public string linettl { get; set; }
        //public string linettlb { get; set; }
        //public string linejkel { get; set; }
        //public string linealamat { get; set; }
        //public string linealamatb { get; set; }
        //public string linertrw { get; set; }
        //public string linekeldesa { get; set; }
        //public string linekecamatan { get; set; }
        //public string lineagama { get; set; }
        //public string linestatuskawin { get; set; }
        //public string linepekerjaan { get; set; }
        //public string linekewarganegaraan { get; set; }
        //public string lineberlaku { get; set; }

        public string error { get; set; }
        OCRService ocrs = new OCRService();
      
        RowKTP KTPRow = new RowKTP();

        public string hasilteks(string text)
        {
            string resultext = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline).Trim();
        return resultext;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        public List<KTP>  WordingProcess(string text)
        {
            try
            {
                // string hasil = RemoveSpecialCharacters(text);
                string resultString = Regex.Replace(text, @"^\s+$!”[\r\n]*", string.Empty, RegexOptions.Multiline);
                string a = resultString.Replace("KeVvDesa", "").Replace("!", "").Replace("IK", "").Replace("BIk", "").Replace("RTIRW", "").Replace("Gol Darah", "").Replace("GA1", "").Replace("MIK", "").Replace("NIK", "").Replace("TempaiTgi Lahir", "").Replace("TempatTgi Lahir “", "").Replace("Gol Darah", "").Replace("wnmsnusu", "").Replace("TempatTgi Lahir", "").Replace("—", "").Replace("(M", "").Replace("Jeniskelamin", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("KevDesa", "").Replace("senis Kelamn — ", "").Replace("denisKelamin ", "").Replace("Name", "").Replace("KellDesa", "").Replace("Gol Darah", "").Replace("Gol Darah ", "").Replace("& ne Kelaman", "").Replace("Mama", "").Replace("ATAW", "").Replace("Status Perkawmnan", "").Replace("ampatTgi Lahu", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("Name", "").Replace("'erkawinan", "").Replace("Jenis kelamin", "").Replace("RTIW", "").Replace("TempatTgi Lahir", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("GoiDarah O", "").Replace("Kelesa — ", "").Replace("Status Perkawinar", "").Replace("gol.darah", "").Replace("Alamat", "").Replace("Berlaku Hingga", "").Replace("Jenis Kelamin", "").Replace("TempauTgi Lahir", "").Replace("RTAW", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("RTRW ", "").Replace("Kewarganegaraan", "").Replace("KeVDesa", "").Replace("RTRW", "").Replace("Gol Darah ", "").Replace("/", "").Replace("Jjene kelamin", "").Replace("Tempai TgiLahu", "").Replace("NIK", "").Replace("Nama", "").Replace("Agama", "").Replace("Pekerjaan", "").Replace("Status Perkawinan", "").Replace("KelDesa", "").Replace("Kecamatan", "").Replace("StatusPerkawinan.", "").Replace(":", "").Replace(".", "").Replace("RT/RW", "").Replace("TempaiTgiLahu-", "").Replace("Jjenekelamin", "").Replace("Gol.Darah", "").Replace("Almat", "").Replace("?", "").Replace("TempaiTgiLahu", "").Replace("MPIalLahu", "").Replace("0rKelamun", "").Replace("Alnal", "").Replace("HIRW", "").Replace("KetDesa", "").Replace("StatusPerkawinan", "").Replace("BerlakuHingga", "").Replace("GolDaran", "").Replace("TemparIgllahu—", "").Replace("seniskelamin", "").Replace("Alnat", "").Replace("TempatTgiLahi", "").Replace("Jeriskelamin", "");
                string[] sep = new string[] { "\n" };
                string[] lines = a.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                String[] sEmails = new String[lines.Count()];
               // KTP data = new KTP();
                List<KTP> daa2 = new List<KTP>();
                String[] sx = new String[daa2.Count()];

                for (int ix = 0; ix < lines.Count(); ix++)
                {
                    KTP data = new KTP();
                    data.provinsi = lines[ix].Trim();
                    data.kota = lines[ix].Trim();
                    data.nik = lines[ix].Trim();
                    data.nama = lines[ix];
                    data.ttl = lines[ix];
                    data.jkel = lines[ix];
                    data.alamat = lines[ix];
                    data.rtrw = lines[ix];
                    data.keldesa = lines[ix];
                    data.kecamatan = lines[ix];
                    data.agama = lines[ix];
                    data.pekerjaan = lines[ix];
                    data.kewarganegaraan = lines[ix];
                    data.berlaku = lines[ix];
                    daa2.Add(data);
                }
              

               


                // yield return sEmails[ix];

                return daa2;
            }
            catch (Exception ex)
            {
                KTP k = new KTP();
                k.error = ex.Message;
                List<KTP> daa2 = new List<KTP>();
                daa2.Add(k);
                return daa2;
            }
        }

   
        public IEnumerable<string> WordingProcess2(string text)
        {
            string resultString = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline).Trim();
            string a = resultString.Replace("TemparTgiLahir","").Replace("KeVvDesa","").Replace("!","").Replace("IK","").Replace("BIk", "").Replace("RTIRW", "").Replace("Gol Darah", "").Replace("GA1", "").Replace("MIK", "").Replace("NIK", "").Replace("TempaiTgi Lahir", "").Replace("TempatTgi Lahir “", "").Replace("Gol Darah", "").Replace("wnmsnusu", "").Replace("TempatTgi Lahir", "").Replace("—", "").Replace("(M", "").Replace("Jeniskelamin", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("KevDesa", "").Replace("senis Kelamn — ", "").Replace("denisKelamin ", "").Replace("Name", "").Replace("KellDesa", "").Replace("Gol Darah", "").Replace("Gol Darah ", "").Replace("& ne Kelaman", "").Replace("Mama", "").Replace("ATAW", "").Replace("Status Perkawmnan", "").Replace("ampatTgi Lahu", "").Replace("TempatrTgi Lahir", "").Replace("RTRW", "").Replace("Name", "").Replace("'erkawinan", "").Replace("Jenis kelamin", "").Replace("RTIW", "").Replace("TempatTgi Lahir", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("GoiDarah O", "").Replace("Kelesa — ", "").Replace("Status Perkawinar", "").Replace("gol.darah", "").Replace("Alamat", "").Replace("Berlaku Hingga", "").Replace("Jenis Kelamin", "").Replace("TempauTgi Lahir", "").Replace("RTAW", "").Replace("Gol Darah", "").Replace("RTRW", "").Replace("RTRW ", "").Replace("Kewarganegaraan", "").Replace("KeVDesa", "").Replace("RTRW", "").Replace("Gol Darah ", "").Replace("/", "").Replace("Jjene kelamin", "").Replace("Tempai TgiLahu", "").Replace("NIK", "").Replace("Nama", "").Replace("Agama", "").Replace("Pekerjaan", "").Replace("Status Perkawinan", "").Replace("KelDesa", "").Replace("Kecamatan", "").Replace("StatusPerkawinan.", "").Replace(":", "").Replace(".", "").Replace("RT/RW", "").Replace("TempaiTgiLahu-", "").Replace("Jjenekelamin", "").Replace("Gol.Darah", "").Replace("Almat", "").Replace("?", "").Replace("TempaiTgiLahu", "").Replace("MPIalLahu", "").Replace("0rKelamun", "").Replace("Alnal", "").Replace("HIRW", "").Replace("KetDesa", "").Replace("StatusPerkawinan", "").Replace("BerlakuHingga", "").Replace("GolDaran", "").Replace("TemparIgllahu—", "").Replace("seniskelamin", "").Replace("Alnat", "").Replace("TempatTgiLahi", "").Replace("Jeriskelamin", "");
            string[] sep = new string[] { "\n" };
            string[] lines = a.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            String[] sEmails = new String[lines.Count()];
            for (int ix = 0; ix < lines.Count(); ix++)
            {
                sEmails[ix] = lines[ix].Trim();

                yield return sEmails[ix];
            }
        }


       
        public static Bitmap cropAtRect(Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(nb);

            g.DrawImage(b, -r.X, -r.Y);
            return nb;

        }

        public Bitmap Crop(string img, int width, int height, int x, int y)
        {

            Image image = Image.FromFile(img);
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

        //Configurasi Posisi Line KTP
        public IEnumerable<int> PopulateObjectKTPConfig()
        {
            XmlDocument doc = new XmlDocument();
            var man = new XmlNamespaceManager(doc.NameTable);
            man.AddNamespace("ns", "http://schemas.microsoft.com/project");
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "DataRowKTP.xml");
            List<string> ls = new List<string>();
            List<List<string>> l = new List<List<string>>();
            // List<int> l = new List<int>();
            XmlNodeList xlist = doc.SelectNodes("DataKTP", man);
            int x = doc.SelectNodes("descendant::*").Count;
            List<int> ax = new List<int>();
            foreach (XmlNode xn in xlist)
            {
                ax.Add( Convert.ToInt32(xn["provinsi"].InnerText));
                ax.Add(Convert.ToInt32(xn["kota"].InnerText));
                ax.Add(Convert.ToInt32(xn["nik"].InnerText));
                ax.Add(Convert.ToInt32(xn["nama"].InnerText));
                ax.Add(Convert.ToInt32(xn["namab"].InnerText));

                ax.Add(Convert.ToInt32(xn["ttla"].InnerText));
                ax.Add(Convert.ToInt32(xn["ttlb"].InnerText));

                ax.Add(Convert.ToInt32(xn["jkel"].InnerText));
                ax.Add(Convert.ToInt32(xn["alamat"].InnerText));
                ax.Add(Convert.ToInt32(xn["alamatb"].InnerText));

                ax.Add(Convert.ToInt32(xn["rtrw"].InnerText));
                ax.Add(Convert.ToInt32(xn["keldesa"].InnerText));
                ax.Add(Convert.ToInt32(xn["kecamatan"].InnerText));
                ax.Add(Convert.ToInt32(xn["agama"].InnerText));
                ax.Add(Convert.ToInt32(xn["statuskawin"].InnerText)); 
                   
                //ls.Add(xn["provinsi"].InnerText);
                //    ls.Add(xn["kota"].InnerText);
                //    ls.Add(xn["nik"].InnerText);
                //    ls.Add(xn["nama"].InnerText);
                //    ls.Add(xn["ttl"].InnerText);
                //    ls.Add(xn["jkel"].InnerText);
                //    ls.Add(xn["alamat"].InnerText);
                //    ls.Add(xn["rtrw"].InnerText);
                //    ls.Add(xn["keldesa"].InnerText);
                //    ls.Add(xn["kecamatan"].InnerText);
                //    ls.Add(xn["agama"].InnerText);
                //    ls.Add(xn["statuskawin"].InnerText);
                //    ls.Add(xn["pekerjaan"].InnerText);
                //    ls.Add(xn["kewarganegaraan"].InnerText);
                //    ls.Add(xn["berlaku"].InnerText);
                }
           

         

            return ax;

        }

        //Function menghitung Object KTP Pada Config
        public int HitungRowKTPConfig()
        {                   
            XmlDocument doc = new XmlDocument();        
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "DataRowKTP.xml");
              int x = doc.SelectNodes("descendant::*").Count;                     
            return x;

        }

       
        //Function Menambah Object KTP Pada Config
        public IEnumerable<int> rowktp()
        {
            XmlDocument doc = new XmlDocument();
            var config = System.Web.Hosting.HostingEnvironment.MapPath("~/config");
            doc.Load(config + "\\" + "DataRowKTP.xml");
            XmlNode provinsinode = doc.DocumentElement.SelectSingleNode("/DataKTP/provinsi");
            XmlNode kotanode = doc.DocumentElement.SelectSingleNode("/DataKTP/kota");
            XmlNode niknode = doc.DocumentElement.SelectSingleNode("/DataKTP/nik");
            XmlNode namanode = doc.DocumentElement.SelectSingleNode("/DataKTP/nama");

            XmlNode ttlnode = doc.DocumentElement.SelectSingleNode("/DataKTP/ttl");

            XmlNode jkelnode = doc.DocumentElement.SelectSingleNode("/DataKTP/jkel");
            XmlNode alamatnode = doc.DocumentElement.SelectSingleNode("/DataKTP/alamat");

            XmlNode rtrwnode = doc.DocumentElement.SelectSingleNode("/DataKTP/rtrw");
            XmlNode keldesanode = doc.DocumentElement.SelectSingleNode("/DataKTP/keldesa");
            XmlNode kecamatannode = doc.DocumentElement.SelectSingleNode("/DataKTP/kecamatan");
            XmlNode agamanode = doc.DocumentElement.SelectSingleNode("/DataKTP/agama");
            XmlNode statuskawinnode = doc.DocumentElement.SelectSingleNode("/DataKTP/statuskawin");
            XmlNode pekerjaannode = doc.DocumentElement.SelectSingleNode("/DataKTP/pekerjaan");
            XmlNode kewarganegaraannode = doc.DocumentElement.SelectSingleNode("/DataKTP/kewarganegaraan");
            XmlNode berlakunode = doc.DocumentElement.SelectSingleNode("/DataKTP/berlaku");

            KTPRow.provinsirow = Convert.ToInt32(provinsinode.InnerText);
            KTPRow.kotarow = Convert.ToInt32(kotanode.InnerText);
            KTPRow.nikrow= Convert.ToInt32(niknode.InnerText);
            KTPRow.namarow= Convert.ToInt32(namanode.InnerText);

            KTPRow.ttlrow = Convert.ToInt32(ttlnode.InnerText);

            KTPRow.jkelrow = Convert.ToInt32(jkelnode.InnerText);
            KTPRow.alamatrow = Convert.ToInt32(alamatnode.InnerText);

            KTPRow.rtrwrow = Convert.ToInt32(rtrwnode.InnerText);
            KTPRow.keldesarow = Convert.ToInt32(keldesanode.InnerText);
            KTPRow.kecamatanrow = Convert.ToInt32(kecamatannode.InnerText);
            KTPRow.agamarow = Convert.ToInt32(agamanode.InnerText);
            KTPRow.statuskawinrow = Convert.ToInt32(statuskawinnode.InnerText);
            KTPRow.pekerjaanrow = Convert.ToInt32(pekerjaannode.InnerText);
            KTPRow.kewarganegaraanrow = Convert.ToInt32(kewarganegaraannode.InnerText);
            KTPRow.berlakurow = Convert.ToInt32(berlakunode.InnerText);

            List<int> row = new List<int>();
            row.Add(KTPRow.provinsirow);
            row.Add(KTPRow.kotarow);
            row.Add(KTPRow.nikrow);
            row.Add(KTPRow.namarow);
            row.Add(KTPRow.ttlrow);

            row.Add(KTPRow.jkelrow);
            row.Add(KTPRow.alamatrow);

            row.Add(KTPRow.rtrwrow);
            row.Add(KTPRow.keldesarow);
            row.Add(KTPRow.kecamatanrow);
            row.Add(KTPRow.agamarow);
            row.Add(KTPRow.statuskawinrow);
            row.Add(KTPRow.pekerjaanrow);
            row.Add(KTPRow.kewarganegaraanrow);
            row.Add(KTPRow.berlakurow);
            return row;
         
        }
        
            
        public IEnumerable<string> hasil(string namafile, string jenis, int widts, int height)
        {
            
               
                IEnumerable<string> hasil = UploadKTPBackup(jenis, namafile, widts, height);
                return hasil;
          
        }

        //Proses OCR KTP UPLOAD
        public KTP ProsesKTPUpload(string namafile, string jenis, int widts, int height)
        {
            if (jenis == "KTP")
            {
                try
                {
                    IEnumerable<int> rowdata = rowktp();
                    var s = rowdata.ToList();
                    String[] datar = new String[rowdata.Count()];
                  //  IEnumerable<string> hasil = UploadKTP(jenis, namafile, widts, height);
                    IEnumerable<string> hasil = UploadKTPBackup(jenis, namafile, widts, height);

                   
                    var provinsi2 = hasil.Skip(s[0]).Take(1).FirstOrDefault().ToString();
                    var kota2 = hasil.Skip(s[1]).Take(1).FirstOrDefault().ToString();
                    var nik2 = hasil.Skip(s[2]).Take(1).FirstOrDefault().ToString();
                 
                      
                    
                 //   var nik2 = niks;

                    var nama2 = hasil.Skip(s[3]).Take(1).FirstOrDefault().ToString();

                    var ttl2 = hasil.Skip(s[4]).Take(1).FirstOrDefault().ToString();

                    var jkel2 = hasil.Skip(s[5]).Take(1).FirstOrDefault().ToString();
                    var alamat2 = hasil.Skip(s[6]).Take(1).FirstOrDefault().ToString();

                    var rtrw2 = hasil.Skip(s[7]).Take(1).FirstOrDefault().ToString();
                    var keldesa2 = hasil.Skip(s[8]).Take(1).FirstOrDefault().ToString();
                    var kecamatan2 = hasil.Skip(s[9]).Take(1).FirstOrDefault().ToString();
                    var agama2 = hasil.Skip(s[10]).Take(1).FirstOrDefault().ToString();
                    var statuskawin2 = hasil.Skip(s[11]).Take(1).FirstOrDefault().ToString();
                    var pekerjaan2 = hasil.Skip(s[12]).Take(1).FirstOrDefault().ToString();
                    var kewarganegaraan2 = hasil.Skip(s[13]).Take(1).FirstOrDefault().ToString();
                    var berlaku2 = hasil.Skip(s[14]).Take(1).FirstOrDefault().ToString();
                   KTP data = new KTP { provinsi = provinsi2, kota = kota2, nik = nik2, nama = nama2,  ttl = ttl2,   jkel = jkel2, alamat = alamat2, rtrw = rtrw2, keldesa = keldesa2, kecamatan = kecamatan2, agama = agama2, statuskawin = statuskawin2, pekerjaan = pekerjaan2, kewarganegaraan = kewarganegaraan2, berlaku = berlaku2};

                  //  KTP data = new KTP { line0 = provinsi2, line1 = kota2, line3 = nama2, line4 = ttl2, line5 = jkel2, line6 = alamat2, line7 = rtrw2, line8 = keldesa2, line9 = kecamatan2, line10 = agama2, line11 = statuskawin2, line12 = pekerjaan2, line13 = kewarganegaraan2, line14 = berlaku2 };
                   // KTP data = new KTP { lineprovinsi = provinsi2, linekota = kota2, linenik = nik2, linenama = nama2, linenamab = nama22, linettl = ttl2, linettlb = ttl22, linejkel = jkel2, linealamat = alamat2, linertrw = rtrw2, linekeldesa = keldesa2, linekecamatan = kecamatan2, lineagama = agama2, linestatuskawin = statuskawin2, linepekerjaan = pekerjaan2, linekewarganegaraan = kewarganegaraan2, lineberlaku = berlaku2 };

                    
                    return data;
                }
                catch (Exception ex)
                {
                    KTP data = new KTP { error= ex.Message };
                    return data;
                }
            }
            else
            {
                KTP ks = new KTP { error="Saat Ini Hanya Tersedia Fungsi KTP Saja" };
                return ks;
            }
        }


        public string ProsesKTPUploadString(string namafile, string jenis, int widts, int height)
        {
            if (jenis == "KTP")
            {
                try
                {
                    IEnumerable<int> rowdata = rowktp();
                    var s = rowdata.ToList();
                    String[] datar = new String[rowdata.Count()];
                    
                    string hasil = UploadKTPBackupString(jenis, namafile, widts, height);

                 
                    return hasil;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Khusus KTP";
            }
        }

        public string UploadKTPBackupString(string jenis, string namafile, int widths, int heights)
        {
            string filename;
            Uri uri = new Uri(namafile);
            System.Net.WebRequest request =
       System.Net.WebRequest.Create(
       namafile);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);

            string hasil = ocrs.process2(new Bitmap(bitmap2),namafile);

           // IEnumerable<string> hasil2 = WordingProcess(hasil);
            string hasil2 = hasilteks(hasil);
            return hasil2;


        }

        public IEnumerable<string> UploadKTPBackup(string jenis, string namafile, int widths, int heights)
        {
            string filename;
            Uri uri = new Uri(namafile);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
             System.Net.WebRequest request =
        System.Net.WebRequest.Create(
        namafile);
             System.Net.WebResponse response = request.GetResponse();
             System.IO.Stream responseStream =
                 response.GetResponseStream();
             Bitmap bitmap2 = new Bitmap(responseStream);
          
            string hasil = ocrs.process2(new Bitmap(bitmap2),namafile);

            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;
           

        }

        public IEnumerable<string> UploadKTP(string jenis, string namafile,int widths,int heights)
        {
            string filename;
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/");
            Uri uri = new Uri(namafile);
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(namafile), paths + System.IO.Path.GetFileName(uri.LocalPath));
            }
            var paths2 = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/" + System.IO.Path.GetFileName(uri.LocalPath));
            //string hasil = ocrs.process2(new Bitmap(paths2));
            string hasil = ocrs.process2(new Bitmap(paths2),namafile);

            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;

        }

        //end


        //PROCESS OCR DARI PATH FILE LOCAL UPLOAD FILE       
        public KTP ProsesKTPLocal(string namafile, string jenis, int widts , int heigth)
        {
            KTP data2 = new KTP();

            if (jenis == "KTP" || jenis == "ktp")
            {
              
                IEnumerable<int> rowdata = rowktp();
                IEnumerable<string> hasil = OCRService(jenis, namafile);
                var s = rowdata.ToList();
                 String[] datar = new String[rowdata.Count()];

                 var provinsi2 = hasil.Skip(s[0]).Take(1).FirstOrDefault();
                 var kota2 = hasil.Skip(s[1]).Take(1).FirstOrDefault();
                 var nik2 = hasil.Skip(s[2]).Take(1).FirstOrDefault();
                 var nama2 = hasil.Skip(s[3]).Take(1).FirstOrDefault();
                 var ttl2 = hasil.Skip(s[4]).Take(1).FirstOrDefault();
                 var jkel2 = hasil.Skip(s[5]).Take(1).FirstOrDefault();
                 var alamat2 = hasil.Skip(s[6]).Take(1).FirstOrDefault();
                 var rtrw2 = hasil.Skip(s[7]).Take(1).FirstOrDefault();
                 var keldesa2 = hasil.Skip(s[8]).Take(1).FirstOrDefault();
                 var kecamatan2 = hasil.Skip(s[9]).Take(1).FirstOrDefault();
                 var agama2 = hasil.Skip(s[10]).Take(1).FirstOrDefault();
                 var statuskawin2 = hasil.Skip(s[11]).Take(1).FirstOrDefault();
                 var pekerjaan2 = hasil.Skip(s[12]).Take(1).FirstOrDefault();
                 var kewarganegaraan2 = hasil.Skip(s[13]).Take(1).FirstOrDefault();
                 var berlaku2 = hasil.Skip(s[14]).Take(1).FirstOrDefault();
                // KTP data = new KTP { line0 = provinsi2, line1 = kota2, line3 = nama2, line4 = ttl2, line5 = jkel2, line6 = alamat2, line7 = rtrw2, line8 = keldesa2, line9 = kecamatan2, line10 = agama2, line11 = statuskawin2, line12 = pekerjaan2, line13 = kewarganegaraan2, line14 = berlaku2 };

                KTP data = new KTP { provinsi = provinsi2, kota = kota2, nik = nik2, nama = nama2, ttl = ttl2, jkel = jkel2, alamat = alamat2, rtrw = rtrw2, keldesa = keldesa2, kecamatan = kecamatan2, agama = agama2, statuskawin = statuskawin2, pekerjaan = pekerjaan2, kewarganegaraan = kewarganegaraan2, berlaku = berlaku2 };
                 return data;
               
            }
               
            else
            {
                KTP data = new KTP { error="Fungsi Hanya KTP"};
                return data;
            }

        }


        public IEnumerable<string> GetWhiteOCR(string namafile)
        {
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES2/" + namafile);
            string hasil = ocrs.process2(new Bitmap(namafile), namafile);
            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;
        }

        public IEnumerable<string> OCRService(string jenis, string namafile)
        {
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES2/" + namafile);
            string hasil = ocrs.process2(new Bitmap(paths), namafile);
           IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;

        }


        public IEnumerable<string> OCRUPLOADSERVICE(string jenis,string namafile)
        {
            string filename;
            Uri uri = new Uri(namafile);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebRequest request =
       System.Net.WebRequest.Create(
       namafile);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);

            string hasil = ocrs.process2(new Bitmap(bitmap2),namafile);

            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;

        }

        public List<KTP> OCRService2(string jenis, string namafile, int width, int height)
        {
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES2/" + namafile);
            string hasil = ocrs.process2(new Bitmap(paths),namafile);
            List<KTP> hasil2 = WordingProcess(hasil);
            return hasil2;

        }


        //end


        public IEnumerable<string> ListObjectKTP(string jenis, string namafile, int widts, int heights)
        {
            KTP data2 = new KTP();
            IEnumerable<int> rowdata = rowktp();
            var s = rowdata.ToList();
            IEnumerable<string> hasil = OCRService(jenis, namafile);
            String[] datar = new String[rowdata.Count()];           
            for (int x = 0; x < s.Count(); x++)
            {
                datar[x] = hasil.Skip(s[x]).Take(1).SingleOrDefault();
                yield return datar[x];
           
            }
        }

        public List<string> ListObjectKTP2(string jenis, string namafile, int widts, int height)
        {
            KTP ks2 = new KTP();
            IEnumerable<int> rowdata = PopulateObjectKTPConfig();
          //  List<List<string>> rowdata = PopulateObjectKTPConfig();
            var list = rowdata.ToList();
            int hitungrow = HitungRowKTPConfig();
           // List<string> hasil = OCRService(jenis, namafile, widts, height);
            String[] datar = new String[hitungrow];           
            KTP K = new KTP();
            List<string> data1 = new List<string>();
            for (int a = 0; a <hitungrow; a++)
            {
              //  daftar[a]=System.Linq.Enumerable.Skip(rowdata[a]).Take(1).SingleOrDefault();
             //  datar[a] = hasil.Skip(list[a]).Take(1).SingleOrDefault();
               data1.Add(datar[a]);
                
                // datar[a] = hasil.ElementAt(4);
             
               //yield return datar[a];
            }

            return data1;
            //return ks2;
        }

      


        //Ambil Semua MetaData Local
        public IEnumerable<string> OCRGetAllMetaDataLocal(string namafile, int widts, int height)
        {
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/" + namafile);
            string hasil = ocrs.process2(new Bitmap(paths),namafile);
            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;
        }

        //Ambil Semua MetaData Local Background White
        public IEnumerable<string> OCRGetAllMetaDataLocalWhite(string namafile)
        {
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/" + namafile);
            string hasil = ocrs.NormalBackground(new Bitmap(paths));
            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;
        }




        //ambilurl
        public IEnumerable<string> UploadKTPBackupGrey(string jenis, string namafile, int widths, int heights)
        {
            string filename;
            Uri uri = new Uri(namafile);
            System.Net.WebRequest request =
       System.Net.WebRequest.Create(
       namafile);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);

            string hasil = ocrs.process2(new Bitmap(bitmap2),namafile);

            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;


        }

        //Ambil Semua MetaData Upload
        public IEnumerable<string> OCRGetAllMetaDataUpload(string namafile, int widts, int height)
        {
            string filename;
            var paths = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/");
            Uri uri = new Uri(namafile);
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(namafile), paths + System.IO.Path.GetFileName(uri.LocalPath));
            }
            var paths2 = System.Web.Hosting.HostingEnvironment.MapPath("~/IMAGES/" + System.IO.Path.GetFileName(uri.LocalPath));
            string hasil = ocrs.process2(new Bitmap(paths2),namafile);
            IEnumerable<string> hasil2 = WordingProcess2(hasil);
            return hasil2;

        }


    }
}