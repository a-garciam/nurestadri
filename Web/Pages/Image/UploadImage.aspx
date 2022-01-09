<%@ Page Language="C#"  MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.UploadImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
        <asp:TextBox ID="tbTitle" runat="server" Height="17px" Width="129px"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
        <asp:TextBox ID="tbDescription" runat="server" Height="79px" Width="264px" OnTextChanged="tbDescription_TextChanged"></asp:TextBox>
        <br />
        <asp:Label ID="lblAperture" runat="server" Text="Aperture:"></asp:Label>
        <asp:TextBox ID="tbAperture" runat="server" Height="17px" Width="129px"></asp:TextBox>
        <asp:Label ID="lblExposure" runat="server" Text="Exposure"></asp:Label>
        <asp:TextBox ID="tbExposure" runat="server" Height="17px" Width="129px"></asp:TextBox>
        <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label>
        <asp:TextBox ID="tbBalance" runat="server" Height="17px" Width="129px"></asp:TextBox>
        <br />
        <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
        <asp:DropDownList ID="ddlCategory" runat="server">
        </asp:DropDownList>
        <div>
        <asp:FileUpload ID="fuImageUpload" runat="server" Height="34px" Width="332px" />
        </div>
        <p>
            <asp:Button ID="btnUploadImage" runat="server" Height="31px" Text="Upload" Width="63px" OnClick="btnUploadImage_Click" />
            <asp:Label ID="lblUploadCompleted" runat="server"></asp:Label>
        </p>
    </form>
</asp:Content>
