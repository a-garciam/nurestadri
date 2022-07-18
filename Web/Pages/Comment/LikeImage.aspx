<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="LikeImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.LikeImage" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblError" runat="server" CssClass="errorMessage" Text="<%$Resources: Common, lblError %>"></asp:Label>
        <br />
        <asp:Image ID="imgFile" runat="server" CssClass="img-details" meta:resourcekey="imgFile" />
        <br />
        <asp:Label ID="lblUserTitle" runat="server" meta:resourcekey="lblUserTitle"></asp:Label>
        <asp:Label ID="lblColon" runat="server" Text=": "></asp:Label>
        <asp:Label ID="lblUser" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblTitleTitle" runat="server" meta:resourcekey="lblTitleTitle"></asp:Label>
        <asp:Label ID="lblColon1" runat="server" Text=": "></asp:Label>
        <asp:Label ID="lblTitle" runat="server"></asp:Label>

        <br />
        <br />

        <asp:Label ID="lblLikeImage" runat="server" Text="Label" meta:resourcekey="lblLikeImageResource1"></asp:Label>
        <asp:Label ID="lblAlreadyLike" runat="server" Text="Label" meta:resourcekey="lblAlreadyLike"></asp:Label>
        <br />
        <asp:Button ID="btnLike" runat="server" Text="Button" OnClick="btnLike_Click" meta:resourcekey="btnLikeResource1" />
    </form>
</asp:Content>