<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NPWPPage.aspx.cs" Inherits="OCRWEBPROJECT.Page.NPWPPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NPWP Page</title>
      <link rel="stylesheet" href="stylepage.css" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
       <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">
  
     <div id="wrapper">
       
        <div class="content-atas">
    <div class="header">
        <h3> Try OCR From Web - Mobile</h3>
    </div>
            </div>
<%--        <div class="content-box">--%>
            <%-- <div class="title">
            <h3> PRODUCTION OCR SERVICES</h3>
               <div class="cara">
                  <ul>
                      <li> Pastikan Gambar berekstensi .jpg</li>
                      <li> Pastikan Hasil Gambar 90 Derajat</li>
                      <li> Pastikan Resolusi Teks terbaca dan gambar tidak buram</li>
                      <li> Jangan Gunakan Blitz / Light Camera</li>
                      <li> Pada Saat pengambilan gambar pastikan jarak tidak terlalu jauh atau terlalu dekat</li>

                      <li> Bila hasil kurang maksimal, gunakan fasilitas crop pada Smartphone anda, untuk membuang background yang tidak diperlukan</li>
                  

                  </ul>
              </div>
            <div class="copy">
                Copyright@2020 tossasyahlevi03@gmail.com
            </div>
          

        </div>--%>
        <div class="content">
         <%--   <div class="content-header-1">
                <h4> Upload Your Picture Here</h4>
            </div>--%>
            <div class="content-upload">
                <div class="ims">
                <asp:Image runat="server" ID="pb" BorderStyle="Solid" Height="186px" Width="186px"  />
                    </div>
                <div class="content-img-upload">
                <asp:FileUpload runat="server" ID="upload" />
                <asp:Button runat="server" ID="btn" Text="Proses" CssClass="btn btn-light" OnClick="btn_Click" />
            </div>
                </div>
        
       <%-- <div class="data">
          --%>
   <input type="text" runat="server" id="provinsi" placeholder="provinsi" class="form-control" />
  <input type="text" runat="server" id="kota" class="form-control" placeholder="kota" />
    <input type="text" runat="server" id="nik" class="form-control" placeholder="nik" />
   <input type="text" runat="server" id="nama" class="form-control" placeholder="nama" />
       
       <input type="text" runat="server" id="ttl" placeholder="Tempat Tanggal Lahir" class="form-control" />
  <input type="text" runat="server" id="jkel" class="form-control" placeholder="Jenis Kelamin" />
    <input type="text" runat="server" id="alamat" class="form-control" placeholder="Alamat" />
   <input type="text" runat="server" id="rtrw" class="form-control" placeholder="RT/RW" />
       
                 <input type="text" runat="server" id="keldesa" placeholder="Kelurahan/Desa" class="form-control" />
  <input type="text" runat="server" id="kecamatan" class="form-control" placeholder="Kecamatan" />
    <input type="text" runat="server" id="agama" class="form-control" placeholder="Agama" />
   <input type="text" runat="server" id="statuskawin" class="form-control" placeholder="Status Kawin" />
       
         <input type="text" runat="server" id="pekerjaan" placeholder="Pekerjaan" class="form-control" />
  <input type="text" runat="server" id="kewarganegaraan" class="form-control" placeholder="Kewarganegaraan" />
    <input type="text" runat="server" id="berlaku" class="form-control" placeholder="Berlaku" />
       
      
            
            
             </div>
           
       <%-- </div>--%>
            </div>
<%--    </div>--%>
    </form>
</body>
</html>
