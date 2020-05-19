<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionOCR.aspx.cs" Inherits="TESSERACTPROJECT.ProductionOCR.ProductionOCR"  validateRequest="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Production OCR</title>
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
   <link rel="stylesheet" href="../Page/stylepage.css" />
      <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
     <div class="title">
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
          

        </div>
        <div class="ok1">
       
         

      <div class="container">
             <div class="bts">
                <asp:Button runat="server" ID="btn1" Text="Json" CssClass="bt"  />
           <asp:Button runat="server" ID="Button1" Text="Txt" CssClass="bt"  />
         <asp:Button runat="server" ID="Button3" Text="PDF" CssClass="bt"  />
           <asp:Button runat="server" ID="Button46" Text="WORD" CssClass="bt" />
      

                 <label runat="server" id="lbs"></label>

            </div>
          <div class="form-group">
               <div class="header">
                    <h3> UPLOAD GAMBAR (90 Derajat)</h3>
                </div>
           
          </div>
            <div class="form-group ">
                <div class="ass">
                <div class="ims">
                     <asp:Image runat="server" ID="imgori"  AlternateText="gambar original"/>
                    
                </div>
            
                    </div>
               <asp:FileUpload runat="server" ID="upload" />
                <label runat="server" id="lbpath"></label>
                <asp:Button runat="server" ID="btns" Text="Process" OnClick="btns_Click" CssClass="btn btn-light"  />
            </div>
          <div class="form-group">
              <label runat="server" id="nik">NIK</label>
              <asp:TextBox runat="server" ID="txtnik" CssClass="form-control"></asp:TextBox>
          </div>
           <div class="form-group">
              <label runat="server" id="nama">Nama</label>
              <asp:TextBox runat="server" ID="txtnama" CssClass="form-control"></asp:TextBox>
          </div>
             <div class="form-group">
              <label runat="server" id="Label2">Alamat</label>
                <textarea runat="server" id="txtalamat" class="form-control" rows="4"></textarea>
          </div>
           <div class="form-group">
              <label runat="server" id="Label3">Tanggal Lahir</label>
              <asp:TextBox runat="server" ID="txttanggal" CssClass="form-control"></asp:TextBox>
          </div>
            <div class="form-group">
                <div class="text">
<%--                    <asp:TextBox  runat="server" ID="txtarea2" TextMode="MultiLine"  CssClass="form-control" Rows="10"></asp:TextBox>--%>
                <textarea runat="server" id="txtarea" visible="true" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>
           </div>

                 <div class="container">
                               <div class="bts">
                <asp:Button runat="server" ID="Button4" Text="Json" CssClass="bt"  />
           <asp:Button runat="server" ID="Button5" Text="Txt" CssClass="bt" />
     

                 <label runat="server" id="Label1"></label>

            </div>
         
        
    </div>
            </div>
        </div>
        
    </form>
</body>
</html>
