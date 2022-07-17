<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.UpdateComment" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <asp:Label ID="lblError" runat="server" CssClass="errorMessage" Text="<%$ Resources:Common, lblError %>" meta:resourcekey="lblErrorResource1"></asp:Label>
        <br />
        <asp:Label ID="lblErrorLength" runat="server" CssClass="errorMessage" meta:resourcekey="lblErrorLength"></asp:Label>
        <br />
        <asp:Label ID="lblUpdateComment" runat="server" meta:resourcekey="lblUpdateCommentResource1"></asp:Label>
        <br />
        <asp:TextBox ID="tbComment" runat="server" Rows="4" Width="500px" TextMode="MultiLine" MaxLength="200" meta:resourcekey="tbCommentResource1"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="rfvComment" runat="server" Text="<%$ Resources:Common, mandatoryField %>" ControlToValidate="tbComment" CssClass="errorMessage" meta:resourcekey="rfvCommentResource1"></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="btnUpdateComment" runat="server" meta:resourcekey="btnCommentResource1" OnClick="btnUpdateComment_Click" />
        <br />
        <asp:Label ID="lblSuccess" runat="server" meta:resourcekey="lblSuccess"></asp:Label>
    </form>
</asp:Content>