<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserFollowed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UserFollowed" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="left">
            <asp:Label ID="lblUserName" runat="server" CssClass="title"></asp:Label>
            <br />
            <asp:Label ID="lblNoFollows" runat="server" Text="<%$ Resources:Common, lblNoFollows %>"></asp:Label>

            <br />
            <asp:GridView ID="gvFollowed" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="value" DataNavigateUrlFormatString="~/Pages/Image/UserImagesFeed.aspx?userID={0}" DataTextField="key" meta:resourcekey="HyperLinkFieldResource1" />
                </Columns>
            </asp:GridView>
            <div class="previousNextLinks">
                <span class="previousLink">
                    <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                        Visible="False"></asp:HyperLink>
                </span><span class="nextLink">
                    <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
                </span>
            </div>
        </div>
    </form>
</asp:Content>