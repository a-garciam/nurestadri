<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserImagesFeed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.UserImagesFeed" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">

    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclUserImagesFeed" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="left">
            <br />
            <div class="label">
                <asp:Label ID="lblUserNameTitle" runat="server" CssClass="label-profile" meta:resourcekey="lblUserNameTitle"></asp:Label>
                <asp:Label ID="lblUserName" runat="server" CssClass="label-profile"></asp:Label>
                <br />
                <asp:Label ID="lblNameTitle" runat="server" CssClass="label-profile" meta:resourcekey="lblNameTitle"></asp:Label>
                <asp:Label ID="lblFirstName" runat="server" CssClass="label-profile"></asp:Label>
                <asp:Label ID="lblSurname" runat="server" CssClass="label-profile"></asp:Label>
                <br />
                <asp:Label ID="lblEmailTitle" runat="server" CssClass="label-profile" meta:resourcekey="lblEmailTitle"></asp:Label>
                <asp:Label ID="lblEmail" runat="server" CssClass="label-profile"></asp:Label>
                <br />
                <asp:Button ID="btnFollow" runat="server" meta:resourcekey="btnFollow" OnClick="btnFollow_Click" />
                <asp:Button ID="btnUnfollow" runat="server" meta:resourcekey="btnUnfollow" OnClick="btnFollow_Click" />
            </div>
            <br />
            <asp:HyperLink ID="hlFollowers" runat="server" meta:resourcekey="hlFollowers"></asp:HyperLink>
            &nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="hlFollowed" runat="server" meta:resourcekey="hlFollowed"></asp:HyperLink>
            <br />
            <asp:Label ID="lblNoImages" runat="server" Text="<%$ Resources: Common, lblNoImages %>"></asp:Label>
            <br />
            <asp:GridView ID="gvUserImages" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" meta:resourcekey="gvUserImagesResource1">
                <Columns>
                    <asp:TemplateField HeaderText="Image" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImagePath") %>' meta:resourcekey="Image1Resource1" CssClass="img-feed" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Title" DataField="Title" meta:resourcekey="BoundFieldResource1" />
                    <asp:BoundField HeaderText="Category" DataField="CategoryName" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField HeaderText="Likes" DataField="Likes" ItemStyle-HorizontalAlign="Center" meta:resourcekey="BoundFieldResource3">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="imageId" DataNavigateUrlFormatString="ImageDetails.aspx?imageID={0}" meta:resourcekey="HyperLinkImageDetails" />
                    <asp:HyperLinkField DataNavigateUrlFields="imageId" DataNavigateUrlFormatString="~/Pages/Comment/LikeImage.aspx?imageID={0}" meta:resourcekey="btnLike" />
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
    <br />
</asp:Content>