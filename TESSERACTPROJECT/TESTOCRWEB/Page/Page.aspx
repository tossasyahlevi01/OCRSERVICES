<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page.aspx.cs" Inherits="TESTOCRWEB.Page.Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page OCR</title>
    <link rel="stylesheet" href="stylepage.css" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
    <div class="header">
        <div class="title">
            <h3> OCR Web Page</h3>
        </div>
    </div>

        <div class="box-content">
           <div class="box-url">
               <input type="text" runat="server" id="txturl" class="form-control" placeholder="Masukkan URL Gambar dari Internet"  />

           <asp:Button runat="server" ID="btn" Text="Proses" CssClass="btn btn-light" OnClick="btn_Click" />
           </div>
            <div class="box-local">
               <asp:FileUpload runat="server" ID="upload" CssClass="form-control" />
             <asp:Button runat="server" ID="Button2" Text="Proses" CssClass="btn btn-light"  />

            </div>
            <div class="box-result">
                <asp:TextBox runat="server" placeholder="Hasil" ID="TXTHASIL" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

    </div>
    </form>
</body>
</html>
