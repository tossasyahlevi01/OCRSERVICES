using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using TESTOCRWEB.Models;
namespace TESTOCRWEB.Page
{
    public partial class Page : System.Web.UI.Page
    {
        KTP data = new KTP();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void UrlFromGoogle()
        {

            try
            {
                //string id = DropDownListProvinsi.SelectedItem.Value.ToString();
                string uri = "http://localhost:9012/";
                string urlhost = "https://mk-cideng.ddns.net:4321/";
                string satuhost = "http://localhost:4321/";

                string parameter = "api/OCR/";
                HttpClient clients = new HttpClient();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("MKOCR:1234");
                string val = System.Convert.ToBase64String(plainTextBytes);
                clients.BaseAddress = new Uri(uri);
                clients.DefaultRequestHeaders.Accept.Clear();

                //  clients.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
               // clients.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                // string urlinfo = string.Format("OCRProsesUpload?namafile={0}&jenis={1}", txtpath.Text, "KTP");
                string urlinfo = string.Format("OCRProsesUpload?namafile={0}&jenis={1}&widts={2}&height={3}", txturl.Value , "KTP",21,54);

                HttpResponseMessage response = clients.GetAsync(uri + parameter + urlinfo).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var p = JsonConvert.DeserializeObject<KTP>(content);
                
                    data.provinsi = p.provinsi;
                    data.kota = p.kota;
                    data.nik = p.nik;
                    data.nama = p.nama;
                    data.ttl = p.ttl;
                    data.jkel = p.jkel;
                    data.alamat = p.alamat;
                    data.rtrw = p.rtrw;
                    data.keldesa = p.keldesa;
                    data.kecamatan = p.kecamatan;
                    data.agama = p.agama;
                    data.statuskawin = p.statuskawin;
                    data.pekerjaan = p.pekerjaan;
                    data.kewarganegaraan = p.kewarganegaraan;
                    data.berlaku = p.berlaku;

                    TXTHASIL.Text = data.provinsi + Environment.NewLine + data.kota + Environment.NewLine + data.nik + Environment.NewLine + data.nama+Environment.NewLine+data.ttl+Environment.NewLine+data.jkel+Environment.NewLine+data.alamat+Environment.NewLine+data.rtrw+Environment.NewLine+data.keldesa+Environment.NewLine+data.kecamatan+Environment.NewLine+data.agama+Environment.NewLine+data.statuskawin+Environment.NewLine+data.pekerjaan+Environment.NewLine+data.kewarganegaraan+Environment.NewLine+data.berlaku;


                }
                else
                {
                    TXTHASIL.Text = response.ReasonPhrase;

                }
            }
            catch (Exception ex)
            {
                TXTHASIL.Text = ex.Message;
            }

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            UrlFromGoogle();
        }

    }
}