<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CreateComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.CreateComment" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <asp:Label ID="lblError" runat="server" CssClass="errorMessage" Text="<%$Resources: Common, lblError %>"></asp:Label>
        <br />
        <asp:Label ID="lblErrorLength" runat="server" CssClass="errorMessage" meta:resourcekey="lblErrorLength"></asp:Label>
        <br />
        <asp:Label ID="lblNewComment" runat="server" meta:resourcekey="lblNewCommentResource1"></asp:Label>
        <br />
        <asp:TextBox ID="tbComment" runat="server" Rows="4" Width="500px" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="rfvComment" runat="server" Text="<%$ Resources: Common, mandatoryField %>" ControlToValidate="tbComment" CssClass="errorMessage"></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="btnComment" runat="server" meta:resourcekey="btnCommentResource1" OnClick="btnComment_Click" />
    </form>
</asp:Content>