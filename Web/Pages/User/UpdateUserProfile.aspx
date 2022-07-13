<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="UpdateUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UpdateUserProfile"
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="UpdateProfile" runat="server" meta:resourcekey="updateProfileTitle"></asp:Localize>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <div class="field">
            <span class="label">
                <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclFirstName" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="100px"
                        Columns="16"></asp:TextBox>
                </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtSurname" runat="server" Width="100px" Columns="16"></asp:TextBox>
                </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtEmail" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        meta:resourcekey="revEmail" CssClass="errorMessage"></asp:RegularExpressionValidator></span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span
                    class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True"
                        Width="100px" OnSelectedIndexChanged="comboLanguage_SelectedIndexChanged">
                    </asp:DropDownList></span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span
                    class="entry">
                    <asp:DropDownList ID="comboCountry" runat="server" Width="100px">
                    </asp:DropDownList></span>
        </div>
        <div class="button">
            <asp:Button ID="btnUpdate" runat="server" meta:resourcekey="btnUpdate" OnClick="btnUpdate_Click" />
        </div>
    </form>
</asp:Content>