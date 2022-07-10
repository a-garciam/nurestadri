<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.UpdateUserProfile" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="UpdateProfile" runat="server" meta:resourcekey="updateProfileTitle"></asp:Localize>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName">First name
                </asp:Localize>
            </span>
            <span class="entry">
                <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
            </span>
        </div>
    </form>
</asp:Content>