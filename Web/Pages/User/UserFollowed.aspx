<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserFollowed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UserFollowed" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="left">
            <asp:Label ID="lblUserName" runat="server" CssClass="title"></asp:Label>
            <br />
            <asp:Label ID="lblNumberFollowedTitle" runat="server" meta:resourcekey="lblNumberFollowedTitle"></asp:Label>
            <asp:Label ID="lblNumberFollowed" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblNoFollows" runat="server" Text="<%$ Resources:Common, lblNoFollows %>"></asp:Label>
        </div>
        <br />
        <asp:GridView ID="gvFollowed" runat="server" AutoGenerateColumns="False">
            <columns>
                <asp:HyperLinkField DataNavigateUrlFields="value" DataNavigateUrlFormatString="~/Pages/Image/UserImagesFeed.aspx?userID={0}" DataTextField="key" meta:resourcekey="HyperLinkFieldResource1" />
            </columns>
        </asp:GridView>
    </form>
</asp:Content>