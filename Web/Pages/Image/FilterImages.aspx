<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" Trace="true" CodeBehind="FilterImages.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.FilterImages" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div class="field">
            <asp:Label ID="lblFindByKeyword" runat="server" Text="Keyword"></asp:Label>
            <br />
            <asp:TextBox ID="tbKeyword" runat="server" MaxLength="50"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="revKeyword" runat="server" ErrorMessage="Tipo de dato no válido" ControlToValidate="tbKeyword" CssClass="errorMessage"></asp:RegularExpressionValidator>
            <br />
            <asp:RequiredFieldValidator ID="rfvKeyword" runat="server" ControlToValidate="tbKeyword" CssClass="errorMessage" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="lblFindByCategory" runat="server" Text="Category"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="BtnFilterImages" runat="server" Text="Find" OnClick="BtnFilterImages_Click" />
        </div>
    </form>
</asp:Content>