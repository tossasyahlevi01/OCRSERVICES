<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OCRPAGE.aspx.cs" Inherits="CONSUMEMKOCR.OCR.OCRPAGE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OCR </title>
    <link rel="stylesheet" href="STYLEOC.css" />
    <link rel="stylesheet" href="../Content/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div class="header">
            OCR CONSUME DLL
        </div>

        <div class="box">
            <div class="atas">
                <div class="form-group">
                <asp:Image runat="server" ID="ims" BorderStyle="Solid" CssClass="plx" Height="175px" Width="354px" />
                    </div>
               <%-- <div class="form-group">
                    <asp:TextBox runat="server" ID="path" CssClass="form-control"></asp:TextBox>
                </div>--%>
                <div class="form-group">
                 <asp:FileUpload runat="server" ID="fu" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <asp:Button runat="server" ID="bn" Text="Process" OnClick="bn_Click"  CssClass="btn btn-light"/>
                </div>
                </div>
            <div class="atas">
                <div class="form-group">
                    NIK
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" ID="nik" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group">
                    Nama
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" ID="Nama" CssClass="form-control"></asp:TextBox>
                </div>

                  <div class="form-group">
                    Alamat
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" ID="Alamat" CssClass="form-control"></asp:TextBox>
                </div>
             <%--   <div class="form-group">

                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txthasil" CssClass="form-control" Rows="10"></asp:TextBox>
                </div>--%>
            </div>
            </div>
        </div>
    
    </form>
</body>
</html>
