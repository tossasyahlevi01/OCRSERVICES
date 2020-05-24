using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using TesseractLibrary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using SERVICEMKOCR.Models;
using System.Text.RegularExpressions;
using System.Web.Cors;
using System.Web.Http.Cors;
namespace SERVICEMKOCR.Controllers
{
         //[BasicAuthentication]  
    //[Authorize]
         [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class OCRController : ApiController
    {
        OCRService ocrs = new OCRService();
        KTP ModelKTP= new KTP();
        Ijazah ModelIjazah = new Ijazah();
                     
        //MENGAMBIL NILAI DARI CONFIGFILE
        [HttpGet]
        public string nilai()
        {
            int a = ocrs.brightness();
            int b = ocrs.contrast();
            string c = a.ToString() + b.ToString();
            return c;
        }

      

        //PROSES UPLAOD FILE
        [HttpGet]
        public string OCRProsesUploadString(string namafile, string jenis, int widts, int height)
        {
            string data = ModelKTP.ProsesKTPUploadString(namafile, jenis, widts, height);
            return data;
        }

             //GET OBJECT KTP
        [HttpGet]
        public KTP OCRProsesUpload(string namafile,string jenis, int widts, int height)
         {
             KTP data = ModelKTP.ProsesKTPUpload(namafile, jenis, widts, height);
                 return data;           
         }


             [HttpGet]
             public List<KTP> GetLocal(string jenis,string namafile, int widts, int height)
        {
            List<KTP> data = ModelKTP.OCRService2(jenis, namafile, widts, height);
            return data;
        }

             [HttpGet]
             public KTP data(string jenis,string namafile, int widts, int height)
             {
                 IEnumerable<string> data = ModelKTP.OCRService(jenis, namafile, widts, height);
                 data.ToList();
                 KTP d = new KTP 
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
                 berlaku=data.Skip(14).FirstOrDefault()
                 
                 
                 };
                 return d;

             }

             [HttpGet]
             public KTP dataUpload(string jenis, string namafile, int widts, int height)
             {
                 IEnumerable<string> data = ModelKTP.OCRUPLOADSERVICE(jenis, namafile, widts, height);
                 data.ToList();
                 KTP d = new KTP
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


             [HttpGet]
             public IEnumerable<string> GetLocalString(string jenis, string namafile, int widts, int height)
             {
                 IEnumerable<string> data = ModelKTP.OCRService(jenis, namafile, widts, height);
                 return data;
             }

       [HttpGet]
             public IEnumerable<string> GetAll(string namafile,string jenis, int widts, int height)
        {
            IEnumerable<string> data = ModelKTP.hasil(namafile, jenis, widts, height);
            return data;
        }
      
        //PROSES LOCAL FILE
        [HttpGet]
        public KTP OCRProsesLocal(string namafile, string jenis, int widts , int height)
        {
            KTP data = ModelKTP.ProsesKTPLocal(namafile, jenis, widts, height);
            return data;
        }


       //Ambil Row Config KTP
        [HttpGet]
        public IEnumerable<int> GetRow()
        {
            IEnumerable<int> row = ModelKTP.rowktp();
            return row;
        }

              //AMBIL LIST OBJECT KTP
        [HttpGet]
        public IEnumerable<string> GetStringRow(string jenis, string namafile, int widts, int height)
        {
            IEnumerable<string> row = ModelKTP.ListObjectKTP(jenis, namafile, widts, height);
            return row;
        }

        [HttpGet]
        public IEnumerable<string> GetAllMetadataLocal(string namafile, int widts, int height)
        {
            IEnumerable<string> data = ModelKTP.OCRGetAllMetaDataLocal(namafile, widts, height);
            return data;
        }

        [HttpGet]
        public IEnumerable<string> GetAllMetadataLocalWhite(string namafile, int widts, int height)
        {
            IEnumerable<string> data = ModelKTP.OCRGetAllMetaDataLocal(namafile, widts, height);
            return data;
        }

              [HttpGet]
              public int tesdatas()
        {
          
            int haha = ModelKTP.HitungRowKTPConfig();
            return haha;
        }

        [HttpGet]
        public IEnumerable<string> coba(string jenis, string namafile, int widts, int heigt)
        {
           IEnumerable<string> data = ModelKTP.ListObjectKTP2(jenis, namafile, widts, heigt);
            //KTP data = ModelKTP.ListObjectKTP2(jenis, namafile);

            return data;

        }

        //RETURN STRING KTP
        [HttpGet]
        public IEnumerable<string> GetAllMetadataUpload(string namafile, int widts, int height)
        {
            IEnumerable<string> data = ModelKTP.UploadKTPBackupGrey("KTP",namafile, widts, height);
            return data;
        }

       
        [HttpGet]
        public IEnumerable<string> GetAllMetadataUploadString(string namafile, int widts, int height)
        {
            IEnumerable<string> data = ModelKTP.OCRGetAllMetaDataUpload(namafile, widts, height);
            return data;
        }


        //    [HttpGet]
        //     public Ijazah GetIjazah(string namafile, string jenis)
        //{
        //    Ijazah Data = ModelIjazah.ProsesIjazahUpload(namafile, jenis);
        //    return Data;
        //}

             [HttpGet]
             public List<string> GetAllIjazah(string namafile, string jenis)
            {
                List<string> data = ModelIjazah.IjazahOCRService(namafile, jenis);
                return data;
            }

             [HttpGet]
             public string cek(string namafile)
             {
                 string a = ModelIjazah.cekbackground(namafile);
                 return a;
             }
    

      

    }
}
