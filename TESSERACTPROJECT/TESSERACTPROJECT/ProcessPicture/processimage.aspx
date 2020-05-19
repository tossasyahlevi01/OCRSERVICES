<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="processimage.aspx.cs" Inherits="TESSERACTPROJECT.ProcessPicture.processimage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../Page/stylepage.css" />
     <link rel="stylesheet" href="../Content/bootstrap.min.css" />
     <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title> Process Gambar</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
       <div class="title">
            <h3> IMAGE PROCESS SERVICES</h3>
               <div class="cara">
                  <ul>
                     <li> Service untuk merekayasa Image Processing Agar mendapatkan hasil maksimal untuk Service OCR</li>
                   <li> Pastikan Gambar berekstensi .jpg</li>
                      <li> Pastikan Hasil Gambar 90 Derajat</li>
                      <li> Pastikan Resolusi Teks terbaca dan gambar tidak buram</li>
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
<%--             <div class="bts">
                <asp:Button runat="server" ID="btn1" Text="Json" CssClass="bt"  />
           <asp:Button runat="server" ID="Button1" Text="Txt" CssClass="bt"  />
         <asp:Button runat="server" ID="Button3" Text="PDF" CssClass="bt"  />
           <asp:Button runat="server" ID="Button46" Text="WORD" CssClass="bt" />
      

                 <label runat="server" id="lbs"></label>

            </div>--%>
          <div class="form-group">
               <div class="header">
                    <h3> Gambar Original </h3>
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
            </div>
          
           </div>

                 <div class="container">
        <%--                       <div class="bts">
                <asp:Button runat="server" ID="Button4" Text="Json" CssClass="bt"  />
           <asp:Button runat="server" ID="Button5" Text="Txt" CssClass="bt" />
     

                 <label runat="server" id="Label1"></label>

            </div>--%>
            <div class="form-group">
                <div class="header">
                    <h3> GreyScale Process</h3>
                </div>
            </div>
            <div class="form-group ">
               <asp:Button runat="server" OnClick="Button1_Click" ID="Button1" Text="Process" CssClass="btn btn-light"  />

                <div class="ass">
          
                <div class="ims">
                      <img runat="server" id="img1" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                         

                     </div>
                    </div>
              
            </div>
          

        </div>
           

              <div class="container">
            <div class="form-group">
                <div class="header">
                    <h3> Background White Process</h3>
                </div>
            </div>
            <div class="form-group ">
                               <asp:Button runat="server" OnClick="Button2_Click" ID="Button2" Text="Process" CssClass="btn btn-light"  />

                <div class="ass">
              
                <div class="ims">
                      <img runat="server" id="img2" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                </div>
                    </div>
             
            </div>
      
           


        </div>


           <div class="container">
            <div class="form-group">
                <div class="header">
                    <h3> Adjust Contrast image</h3>
                </div>
            </div>
            <div class="form-group ">
                               <asp:Button OnClick="Button3_Click" runat="server" ID="Button3" Text="Process" CssClass="btn btn-light"  />

                <div class="ass">
              
                <div class="ims">
                      <img runat="server" id="img3" src="data:image/Jpeg;base64" alt="gambarprocess"/>
                </div>
                    </div>
             
            </div>
           
           


        </div>
            


            </div>
    </div>
    </form>
</body>
</html>
