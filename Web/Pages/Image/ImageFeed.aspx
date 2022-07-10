<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" Trace="true" CodeBehind="ImageFeed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ImageFeed" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="left">
            <asp:Button ID="btnFilterImages" runat="server" Text="Filter" OnClick="btnFilterImages_Click" Height="33px" Width="57px" />
            <br />
            <asp:GridView ID="gvImageFeed" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvImageFeed_PageIndexChanging" ShowHeaderWhenEmpty="True" Style="margin-right: 0px">
                <Columns>
                    <asp:ImageField DataImageUrlField="imagePath" HeaderText="Image">
                    </asp:ImageField>
                    <asp:BoundField DataField="title" HeaderText="Title" />
                    <asp:BoundField DataField="likes" HeaderText="Likes" />
                    <asp:BoundField DataField="categoryName" HeaderText="Category" SortExpression="categoryName" />
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="UserImagesFeed.aspx?userID={0}" HeaderText="userName" DataTextField="userName" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>