<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserImagesFeed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.UserImagesFeed" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">

    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclUserImagesFeed" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="left">
            <br />
            <asp:Label ID="lblNoImages" runat="server" Text="<%$ Resources: Common, lblNoImages %>"></asp:Label>
            <br />
            <asp:GridView ID="gvUserImages" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvUserImages_PageIndexChanging" GridLines="Horizontal" Width="346px" meta:resourcekey="gvUserImagesResource1">
                <Columns>
                    <asp:TemplateField HeaderText="Image" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImagePath") %>' meta:resourcekey="Image1Resource1" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Title" DataField="Title" meta:resourcekey="BoundFieldResource1" />
                    <asp:BoundField HeaderText="Category" DataField="CategoryName" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField HeaderText="Likes" DataField="Likes" ItemStyle-HorizontalAlign="Center" meta:resourcekey="BoundFieldResource3">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <br />
</asp:Content>