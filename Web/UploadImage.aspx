<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.UploadImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 182px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:FileUpload ID="fuImageUpload" runat="server" Height="34px" Width="332px" />
        <div>
            <asp:Button ID="btnUploadImage" runat="server" Height="31px" Text="Button" Width="63px" OnClick="btnUploadImage_Click" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
