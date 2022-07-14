<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserFollowers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UserFollowers" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server" class="label">
        <div align="left">
            <asp:Label ID="lblUserName" runat="server" CssClass="title"></asp:Label>
            <br />
            <asp:Label ID="lblNumberFollowersTitle" runat="server" meta:resourcekey="lblNumberFollowersTitle"></asp:Label>
            <asp:Label ID="lblNumberFollowers" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblNoFollows" runat="server" Text="<%$ Resources:Common, lblNoFollows %>"></asp:Label>
        </div>
        <br />
        <asp:GridView ID="gvFollowers" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="value" DataNavigateUrlFormatString="~/Pages/Image/UserImagesFeed.aspx?userID={0}" DataTextField="key" HeaderText="Followers" meta:resourcekey="HyperLinkFieldResource1" />
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>