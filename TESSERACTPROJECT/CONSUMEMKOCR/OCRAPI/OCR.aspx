<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OCR.aspx.cs" Inherits="CONSUMEMKOCR.OCRAPI.OCR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="styleocr.css" />
    <title> CONSUME API OCR</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div class="header">
            Consume API OCR
        </div>
    <div class="box">
        <div class="boxisi">
            <div class="isi">
            <div class="form-group">
                <asp:TextBox runat="server" ID="txtpath" placeholder="Url From Google" CssClass="form-control"></asp:TextBox>
            </div>
       
        <div class="form-group">
            <asp:Button runat="server" ID="btn" OnClick="btn_Click" Text="Proses" CssClass="btn btn-dark" />
        </div>
                </div>
     
            </div>
         <div class="boxisi">
            <div class="isi">
            <div class="form-group">
                <asp:TextBox runat="server" ID="txtpathlocal" placeholder="Url From Local" CssClass="form-control"></asp:TextBox>
            </div>
       
        <div class="form-group">
            <asp:Button runat="server" ID="Button1" OnClick="Button1_Click" Text="Proses" CssClass="btn btn-dark" />
      <asp:LinkButton runat="server" ID="lb" Text="Lihat Gambar"></asp:LinkButton>
              </div>
                </div>
     
            </div>
          
     
          <div class="xs">
              <div class="data1">
         <div class="form-group">
                  Provinsi
                    <label runat="server" id="lbprovinsi"></label>

              </div>
               <div class="form-group">
                   Kota
                    <label runat="server" id="lbkota"></label>
                   </div>
               <div class="form-group">
                 NIK
                    <label runat="server" id="lbnik"></label>
                   </div>
               <div class="form-group">
                   Nama
                    <label runat="server" id="lbnama"></label>
                   </div>
                  </div>

              <div class="data1">
               <div class="form-group">
                    Tempat Tanggal Lahir
                    <label runat="server" id="lbttl"></label>
                   </div>
               <div class="form-group">
                    Jenis Kelamin
                    <label runat="server" id="lbjkel"></label>
                   </div>
               <div class="form-group">
                    Alamat
                    <label runat="server" id="lbalamat"></label>
                   </div>
                <div class="form-group">
                   RT/RW
                  <label runat="server" id="lbrt"></label>
                    </div>   
              </div>

              <div class="data1">
               <div class="form-group">           
                    Kelurahan / Desa
                   <label runat="server" id="lbkel"></label>
                   </div>
              <div class="form-group">
                 Kecamatan
                   <label runat="server" id="lbkecamatan"></label>
                  </div>
             
               <div class="form-group">
                   Agama
                    <label runat="server" id="lbagama"></label>
                   </div>
               <div class="form-group">
                  Status Kawin
                    <label runat="server" id="lbkawin"></label>
                   </div>
             

               <div class="form-group">
                   Pekerjaan
                    <label runat="server" id="lbjob"></label>
                   </div>
              </div>

              <div class="data1">

               <div class="form-group">
                   Kewarganegaraan
                   <label runat="server" id="lbpkn"></label>
                   </div>
              
               <div class="form-group">
                   Berlaku
                   <label runat="server" id="lbberlaku"></label>
                   </div>
              
               <div class="form-group">
                    Error
                   <label runat="server" id="lberror"></label>
                   </div>
            
              </div>
                </div>
    </div>
    </div>
    </form>
</body>
</html>
