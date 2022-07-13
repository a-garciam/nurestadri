<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ImageFeed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ImageFeed" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">

    <asp:Label ID="lclMenuExplanation" runat="server" meta:resourcekey="lclImageFeed" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="left">
            <asp:Button ID="btnFilterImages" runat="server" Text="Filter" OnClick="btnFilterImages_Click" Height="33px" Width="57px" meta:resourcekey="btnFilterImagesResource1" />
            <br />
            <asp:Label ID="lblNoImages" runat="server" Text="<%$ Resources: Common, lblNoImages %>"></asp:Label>
            <br />
            <asp:GridView ID="gvImageFeed" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvImageFeed_PageIndexChanging" ShowHeaderWhenEmpty="True" Style="margin-right: 0px" meta:resourcekey="gvImageFeedResource1">
                <Columns>
                    <asp:ImageField DataImageUrlField="imagePath" HeaderText="Image" meta:resourcekey="ImageFieldResource1" ControlStyle-CssClass="img-feed">
                    </asp:ImageField>
                    <asp:BoundField DataField="title" HeaderText="Title" meta:resourcekey="BoundFieldResource1" />
                    <asp:BoundField DataField="likes" HeaderText="Likes" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="categoryName" HeaderText="Category" SortExpression="categoryName" meta:resourcekey="BoundFieldResource3" />
                    <asp:HyperLinkField DataNavigateUrlFields="userId" DataNavigateUrlFormatString="UserImagesFeed.aspx?userID={0}" HeaderText="userName" DataTextField="userName" meta:resourcekey="HyperLinkFieldResource1" />
                    <asp:HyperLinkField DataNavigateUrlFields="imageId" DataNavigateUrlFormatString="ImageDetails.aspx?imageID={0}" meta:resourcekey="HyperLinkImageDetails" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>