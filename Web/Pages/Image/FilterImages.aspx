<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="FilterImages.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.FilterImages" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">

    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclFilterImages" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div class="field">
            <asp:Label ID="lblError" runat="server" CssClass="errorMessage" Text="Error"></asp:Label>
            <br />
            <asp:Label ID="lblFindByKeyword" runat="server" Text="Keyword" meta:resourcekey="lblFindByKeywordResource1"></asp:Label>
            <br />
            <asp:TextBox ID="tbKeyword" runat="server" MaxLength="50" meta:resourcekey="tbKeywordResource1"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvKeyword" runat="server" ControlToValidate="tbKeyword" CssClass="errorMessage" ErrorMessage="Campo Obligatorio" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lblFindByCategory" runat="server" Text="Category" meta:resourcekey="lblFindByCategoryResource1"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" meta:resourcekey="ddlCategoryResource1">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="BtnFilterImages" runat="server" Text="Find" OnClick="BtnFilterImages_Click" meta:resourcekey="BtnFilterImagesResource1" />
        </div>
    </form>
</asp:Content>