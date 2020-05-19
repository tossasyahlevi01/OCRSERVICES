<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page.aspx.cs" ValidateRequest="false" Inherits="TESSERACTPROJECT.Page.Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="stylepage.css" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <title> Tesseract OCR</title>
     <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">

        <div class="title">
            <h3> OCR SERVICES</h3>
               <div class="cara">
                  <ul>
                      <li> Pastikan Gambar berekstensi .jpg</li>
                      <li> Pastikan Hasil Gambar 90 Derajat</li>
                      <li> Pastikan Resolusi Teks terbaca dan gambar tidak buram</li>
                      <li> Pada Saat pengambilan gambar pastikan jarak tidak terlalu jauh atau terlalu dekat</li>

                      <li> Bila hasil kurang maksimal, gunakan fasilitas crop pada Smartphone anda, untuk membuang background yang tidak diperlukan</li>
                  
                      <li> Jika Diperlukan Set Gambar menjadi Greyscale Pada Konfigurasi Camera Anda</li>

                  </ul>
              </div>
            <div class="copy">
                Copyright@2020 tossasyahlevi03@gmail.com
            </div>
          

        </div>
        <div class="ok1">
       
         

      <div class="container">
             <div class="bts">
                <asp:Button runat="server" ID="btn1" Text="Json" CssClass="bt" OnClick="btn1_Click" />
           <asp:Button runat="server" ID="Button1" Text="Txt" CssClass="bt" OnClick="Button1_Click" />
         <asp:Button runat="server" ID="Button3" Text="PDF" CssClass="bt"  />
           <asp:Button runat="server" ID="Button46" Text="WORD" CssClass="bt" />
      

                 <label runat="server" id="lbs"></label>

            </div>
          <div class="form-group">
               <div class="header">
                    <h3> original Process (90 Derajat)</h3>
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
                <asp:Button runat="server" ID="btns" Text="Process" CssClass="btn btn-light" OnClick="btns_Click" />
            </div>
            <div class="form-group">
                <div class="text">
                <textarea runat="server" id="txtarea" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>
           </div>

                 <div class="container">
                               <div class="bts">
                <asp:Button runat="server" ID="Button4" Text="Json" CssClass="bt" OnClick="btn1_Click" />
           <asp:Button runat="server" ID="Button5" Text="Txt" CssClass="bt" />
     

                 <label runat="server" id="Label1"></label>

            </div>
            <div class="form-group">
                <div class="header">
                    <h3> GreyScale Process</h3>
                </div>
            </div>
            <div class="form-group ">
                <div class="ass">
          
                <div class="ims">
                      <img runat="server" id="img1" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                </div>
                    </div>
              
            </div>
            <div class="form-group">
                <div class="text">
                <textarea runat="server" id="Textarea1" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>

        </div>
           
            </div>


<%--          <div class="ok2">
       
      <div class="container">
          <div class="form-group">
               <div class="header">
                    <h3> original Process (>90 Derajat)</h3>
                </div>
              <div class="cara">
                  <ul>
                      <li> Pastikan Gambar berekstensi .jpg</li>
                      <li> Pastikan Hasil Gambar >90 Derajat</li>
                      <li> Pastikan Resolusi Teks terbaca dan gambar tidak buram</li>
                  </ul>
              </div>
          </div>
            <div class="form-group ">
                <div class="ass">
                <div class="ims">
                     <asp:Image runat="server" ID="Image1"  AlternateText="gambar original"/>
                    
                </div>
            
                    </div>
               <asp:FileUpload runat="server" ID="FileUpload1" />
                <label runat="server" id="Label1"></label>
                <asp:Button runat="server" ID="Button1" Text="Process" CssClass="btn btn-light" OnClick="Button1_Click"/>
            </div>
            <div class="form-group">
                <div class="text">
                <textarea runat="server" id="Textarea2" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>
           </div>

                 <div class="container">
            <div class="form-group">
                <div class="header">
                    <h3> GreyScale Process</h3>
                </div>
            </div>
            <div class="form-group ">
                <div class="ass">
          
                <div class="ims">
                      <img runat="server" id="img2" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                </div>
                    </div>
              
            </div>
            <div class="form-group">
                <div class="text">
                <textarea runat="server" id="Textarea3" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>

        </div>
           
            </div>--%>

   <%--     <div class="ok2">
          <div class="container">
            <div class="form-group">
                <div class="header">
                    <h3> Background White Process</h3>
                </div>
            </div>
            <div class="form-group ">
                <div class="ass">
              
                <div class="ims">
                      <img runat="server" id="img2" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                </div>
                    </div>
             
            </div>
            <div class="form-group">
                <div class="text">
                <textarea runat="server" id="Textarea2" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>
           


        </div>


           <div class="container">
            <div class="form-group">
                <div class="header">
                    <h3> Deskew image</h3>
                </div>
            </div>
            <div class="form-group ">
                <div class="ass">
              
                <div class="ims">
                      <img runat="server" id="img3" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                </div>
                    </div>
             
            </div>
            <div class="form-group">
                <div class="text">
                <textarea runat="server" id="Textarea3" class="form-control" rows="10"  ></textarea>
                    </div>
            </div>
           


        </div>
            </div>--%>


    
    </div>
    </form>
</body>
</html>
