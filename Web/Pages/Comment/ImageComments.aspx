<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ImageComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.ImageComments" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblNoComments" runat="server" Text="<%$ Resources:Common, lblNoComments %>" meta:resourcekey="lblNoCommentsResource1"></asp:Label>

        <br />
        <div align="left">
            <asp:HyperLink ID="hlCreateComment" runat="server" meta:resourcekey="hlCreateComment"></asp:HyperLink>
        </div>
        <asp:GridView ID="gv_ImageComments" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_ImageComments_RowDataBound">
            <Columns>
                <asp:BoundField DataField="CommentText" HeaderText="Comment" meta:resourcekey="commentTitle" ItemStyle-HorizontalAlign="Justify" ItemStyle-Wrap="True" ControlStyle-Width="300px" ItemStyle-CssClass="maxWidthGrid" />
                <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="~/Pages/Image/UserImagesFeed.aspx?userID={0}" DataTextField="UserName" meta:resourcekey="userNameTitle" />
                <asp:BoundField DataField="CreationDate" HeaderText="Date" meta:resourcekey="creationDateTitle" />
                <asp:HyperLinkField DataNavigateUrlFields="commentId" DataNavigateUrlFormatString="~/Pages/Comment/UpdateComment.aspx?commentID={0}" meta:resourcekey="hlUpdate" />
                <asp:HyperLinkField DataNavigateUrlFields="commentId" DataNavigateUrlFormatString="~/Pages/Comment/DeleteComment.aspx?commentID={0}" Text="<%$ Resources: Common, btnDelete %>" />
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