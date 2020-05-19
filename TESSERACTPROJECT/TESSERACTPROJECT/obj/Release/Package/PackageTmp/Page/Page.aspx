<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page.aspx.cs" Inherits="TESSERACTPROJECT.Page.Page" %>

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
        <div class="container">
            <div class="form-group">
                <div class="header">
                    <h3> Tesseract OCR</h3>
                </div>
            </div>
            <div class="form-group ">
                <div class="ass">
                <div class="ims">
                     <asp:Image runat="server" ID="imgori"  AlternateText="gambar original"/>
                    
                </div>
                <div class="ims">
                      <img runat="server" id="imgCtrl" src="data:image/Jpeg;base64" alt="gambarprocess"/>
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
    
    </div>
    </form>
</body>
</html>
