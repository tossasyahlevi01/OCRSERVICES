using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CONSUMEMKOCR.Models;
namespace CONSUMEMKOCR.OCRAPI
{
    public partial class OCR : System.Web.UI.Page
    {
        KTP KT = new KTP();
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
                clients.BaseAddress = new Uri(satuhost);
                clients.DefaultRequestHeaders.Accept.Clear();

                //  clients.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                clients.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                // string urlinfo = string.Format("OCRProsesUpload?namafile={0}&jenis={1}", txtpath.Text, "KTP");
                string urlinfo = string.Format("OCRProsesUpload?namafile={0}&jenis={1}", txtpath.Text, "KTP");

                HttpResponseMessage response = clients.GetAsync(satuhost + parameter + urlinfo).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var p = JsonConvert.DeserializeObject<KTP>(content);
                    lbprovinsi.InnerText = p.provinsi;
                    lbkota.InnerText = p.kota;
                    lbnik.InnerText = p.nik;
                    lbnama.InnerText = p.nama;
                    lbttl.InnerText = p.ttl;
                    lbjkel.InnerText = p.jkel;
                    lbalamat.InnerText = p.alamat;
                    lbrt.InnerText = p.rtrw;
                    lbkel.InnerText = p.keldesa;
                    lbkecamatan.InnerText = p.kecamatan;
                    lbagama.InnerText = p.agama;
                    lbkawin.InnerText = p.statuskawin;
                    lbpkn.InnerText = p.kewarganegaraan;
                    lbberlaku.InnerText = p.berlaku;



                }
                else
                {
                    lberror.InnerText = response.ReasonPhrase;

                }
            }
            catch (Exception ex)
            {
                lberror.InnerText = ex.Message;
            }

        }


          public void UrlFromLocal()
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
                    clients.BaseAddress = new Uri(satuhost);
                    clients.DefaultRequestHeaders.Accept.Clear();

                  //  clients.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    clients.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                    string urlinfo = string.Format("OCRProsesLocal?namafile={0}&jenis={1}", txtpathlocal.Text,"KTP");

                    HttpResponseMessage response = clients.GetAsync(satuhost+parameter+urlinfo).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var p = JsonConvert.DeserializeObject<KTP>(content);
                        lbprovinsi.InnerText = p.provinsi;
                        lbkota.InnerText = p.kota;
                        lbnik.InnerText = p.nik;
                        lbnama.InnerText = p.nama;
                        lbttl.InnerText = p.ttl;
                        lbjkel.InnerText = p.jkel;
                        lbalamat.InnerText = p.alamat;
                        lbrt.InnerText = p.rtrw;
                        lbkel.InnerText = p.keldesa;
                        lbkecamatan.InnerText = p.kecamatan;
                        lbagama.InnerText = p.agama;
                        lbkawin.InnerText = p.statuskawin;
                        lbpkn.InnerText = p.kewarganegaraan;
                        lbberlaku.InnerText = p.berlaku;
                        lberror.InnerText = "Tidak Ada";
                      
                        
                    }
                    else
                    {
                        lberror.InnerText = response.ReasonPhrase;
                       
                    }
                }
                catch (Exception ex)
                {
                    lberror.InnerText = ex.Message;
                }
            
        }

          protected void btn_Click(object sender, EventArgs e)
          {
              UrlFromGoogle();

          }

          protected void Button1_Click(object sender, EventArgs e)
          {
              UrlFromLocal();
          }

    }
}