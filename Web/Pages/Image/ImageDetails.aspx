<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ImageDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ImageDetails" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="center">
            <asp:Label ID="lblDeleteError" runat="server" meta:resourcekey="lblErrorDeleteImage" CssClass="errorMessage"></asp:Label>
            <asp:Label ID="lblPermissionError" runat="server" meta:resourcekey="lblPermissionError" CssClass="errorMessage"></asp:Label>
            <br />
            <asp:Button ID="btnDeleteImage" runat="server" Text="<%$ Resources: Common, btnDelete %>" CssClass="errorMessage" OnClick="btnDeleteImage_Click" />
            <br />
            <asp:Image ID="imgFile" runat="server" CssClass="img-details" meta:resourcekey="imgFileResource1" />
            <br />
            <div align="left" style="width: 600px">
                <asp:Label ID="lblUserTitle" runat="server" meta:resourcekey="lblUserTitleResource1"></asp:Label>
                <asp:Label ID="lblColon" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblUser" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblTitleTitle" runat="server" meta:resourcekey="lblTitleTitleResource1"></asp:Label>
                <asp:Label ID="lblColon1" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblDescriptionTitle" runat="server" meta:resourcekey="lblDescriptionTitleResource1"></asp:Label>
                <asp:Label ID="lblColon2" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblLikesTitle" runat="server" meta:resourcekey="lblLikesTitleResource1"></asp:Label>
                <asp:Label ID="lblColon3" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblLikes" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblCreationDateTitle" runat="server" meta:resourcekey="lblCreationDateTitleResource1"></asp:Label>
                <asp:Label ID="lblColon4" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblCreationDate" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblCategoryTitle" runat="server" meta:resourcekey="lblCategoryTitleResource1"></asp:Label>
                <asp:Label ID="lblColon5" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblCategory" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblApertureTitle" runat="server" meta:resourcekey="lblApertureTitleResource1"></asp:Label>
                <asp:Label ID="lblColon6" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblAperture" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblExposureTitle" runat="server" meta:resourcekey="lblExposureTitleResource1"></asp:Label>
                <asp:Label ID="lblColon7" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblExposure" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblBalanceTitle" runat="server" meta:resourcekey="lblBalanceTitleResource1"></asp:Label>
                <asp:Label ID="lblColon8" runat="server" Text=": "></asp:Label>
                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                <br />
            </div>
        </div>
    </form>
</asp:Content>