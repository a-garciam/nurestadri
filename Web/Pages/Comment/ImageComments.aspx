<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ImageComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.ImageComments" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblNoComments" runat="server" Text="<%$ Resources:Common, lblNoComments %>" meta:resourcekey="lblNoCommentsResource1"></asp:Label>
        <br />
        <asp:GridView ID="gv_ImageComments" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="CommentText" HeaderText="Comment" meta:resourcekey="commentTitle" />
                <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="~/Pages/Image/UserImagesFeed.aspx?userID={0}" DataTextField="UserName" meta:resourcekey="userNameTitle" />
                <asp:BoundField DataField="CreationDate" HeaderText="Date" meta:resourcekey="creationDateTitle" />
            </Columns>
        </asp:GridView>
        <div class="previousNextLinks" align="left">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False" meta:resourcekey="lnkPreviousResource1">
                </asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
    </form>
</asp:Content>