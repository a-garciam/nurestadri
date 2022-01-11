<%@ Page Language="C#"  MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.UploadImage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
        <div class="field">
            <asp:Label ID="lblTitle" runat="server" Text="Title" meta:resourcekey="lblTitleResource1" CssClass="label"></asp:Label>
            <asp:TextBox ID="tbTitle" runat="server" meta:resourcekey="tbTitleResource1" CssClass="entry"></asp:TextBox>
        </div>
        <div  class="field">
        <asp:Label ID="lblDescription" runat="server" Text="Description" meta:resourcekey="lblDescriptionResource1" CssClass="label"></asp:Label>
        <asp:TextBox ID="tbDescription" runat="server" Height="79px" Width="264px" OnTextChanged="tbDescription_TextChanged" meta:resourcekey="tbDescriptionResource1" CssClass="entry"></asp:TextBox>
        </div>
        <div class="field">
            <asp:Label ID="lblAperture" runat="server" Text="Aperture:" meta:resourcekey="lblApertureResource1" CssClass="label"></asp:Label>
            <asp:TextBox ID="tbAperture" runat="server" Height="17px" Width="129px" meta:resourcekey="tbApertureResource1" CssClass="entry"></asp:TextBox>
            <br />
            <asp:Label ID="lblExposure" runat="server" Text="Exposure" meta:resourcekey="lblExposureResource1" CssClass="label"></asp:Label>
            <asp:TextBox ID="tbExposure" runat="server" Height="17px" Width="129px" meta:resourcekey="tbExposureResource1" CssClass="entry"></asp:TextBox>
            <br />
            <asp:Label ID="lblBalance" runat="server" Text="Balance" meta:resourcekey="lblBalanceResource1" CssClass="label"></asp:Label>
            <asp:TextBox ID="tbBalance" runat="server" Height="17px" Width="129px" meta:resourcekey="tbBalanceResource1" CssClass="entry"></asp:TextBox>
        </div>
        <div class="field">
        <asp:Label ID="lblCategory" runat="server" Text="Category" meta:resourcekey="lblCategoryResource1" CssClass="label"></asp:Label>
        <asp:DropDownList ID="ddlCategory" runat="server" meta:resourcekey="ddlCategoryResource1" CssClass="entry">
        </asp:DropDownList>
        </div>
        <div class="field">
            <asp:Label ID="lblImageUpload" runat="server" CssClass="label" Text="Label"></asp:Label>
        <asp:FileUpload ID="fuImageUpload" runat="server" Height="78px" Width="351px" meta:resourcekey="fuImageUploadResource1" CssClass="entry" />
        </div>
            <asp:Button ID="btnUploadImage" runat="server" Height="31px" Text="Upload" Width="63px" OnClick="btnUploadImage_Click" meta:resourcekey="btnUploadImageResource1" />
            <br />
            <asp:Label ID="lblUploadCompleted" runat="server" meta:resourcekey="lblUploadCompletedResource1" CssClass="info"></asp:Label>
        <br />
    </form>
</asp:Content>
