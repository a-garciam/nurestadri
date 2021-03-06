<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.UploadImage" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">

    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclUploadImage" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
        <div class="field">
            <asp:Label ID="lblError" runat="server" CssClass="errorMessage" Text="<%$Resources: Common, lblError %>"></asp:Label>
            <br />
            <asp:Label ID="lblTitle" runat="server" Text="Title" meta:resourcekey="lblTitleResource1" CssClass="label"></asp:Label>
            <br />
            <asp:TextBox ID="tbTitle" runat="server" CssClass="entry" MaxLength="50"></asp:TextBox>

            <br />
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="tbTitle" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
        </div>
        <div class="field">
            <asp:Label ID="lblDescription" runat="server" Text="Description" meta:resourcekey="lblDescriptionResource1" CssClass="label"></asp:Label>
            <br />
            <asp:TextBox ID="tbDescription" runat="server" Height="79px" Width="264px" meta:resourcekey="tbDescriptionResource1" CssClass="entry" TextMode="MultiLine" MaxLength="500" Style="resize: none"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="tbDescription" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
            <br />
        </div>
        <div class="field">
            <asp:Label ID="lblAperture" runat="server" Text="Aperture:" meta:resourcekey="lblApertureResource1" CssClass="label"></asp:Label>
            <br />
            <asp:Label ID="lblApertureFormat" runat="server" CssClass="label" Text="f/" meta:resourcekey="lblApertureFormatResource1"></asp:Label>
            <asp:TextBox ID="tbAperture" placeholder="Ex.: 3.0" runat="server" Height="17px" Width="129px" Min="0" Type="number" meta:resourcekey="tbApertureResource1" CssClass="entry" MaxLength="10"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvTitle1" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="tbAperture" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
            <asp:Label ID="lblErrorAperture" runat="server" CssClass="errorMessage" Text="Error" meta:resourcekey="lblErrorApertureResource1"></asp:Label>
            <br />
            <asp:Label ID="lblExposure" runat="server" Text="Exposure" meta:resourcekey="lblExposureResource1" CssClass="label"></asp:Label>
            <br />
            <asp:TextBox ID="tbExposure" placeholder="Ex.: 1/100" runat="server" Height="17px" Width="129px" meta:resourcekey="tbExposureResource1" CssClass="entry" MaxLength="10"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvTitle2" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="tbExposure" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
            <asp:Label ID="lblErrorExposure" runat="server" CssClass="errorMessage" Text="Error" meta:resourcekey="lblErrorExposureResource1"></asp:Label>
            <br />
            <asp:Label ID="lblBalance" runat="server" Text="Balance" meta:resourcekey="lblBalanceResource1" CssClass="label"></asp:Label>
            <br />
            <asp:TextBox ID="tbBalance" runat="server" Height="17px" Width="129px" Min="0" Type="number" meta:resourcekey="tbBalanceResource1" CssClass="entry" MaxLength="10"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvTitle3" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="tbBalance" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
            <asp:Label ID="lblErrorBalance" runat="server" CssClass="errorMessage" Text="Error" meta:resourcekey="lblErrorBalanceResource1"></asp:Label>
        </div>
        <div class="field">
            <asp:Label ID="lblCategory" runat="server" Text="Category" meta:resourcekey="lblCategoryResource1" CssClass="label"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlCategory" runat="server" meta:resourcekey="ddlCategoryResource1" CssClass="entry">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvTitle4" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="ddlCategory" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
        </div>
        <div class="field">
            <br />
            <asp:FileUpload ID="fuImageUpload" runat="server" Height="36px" Width="351px" meta:resourcekey="fuImageUploadResource1" CssClass="entry" />
            <br />
            <asp:RequiredFieldValidator ID="rfvTitle5" runat="server" Display="Dynamic" ErrorMessage="Campo Obligatorio" ControlToValidate="fuImageUpload" CssClass="errorMessage" Text="<%$ Resources: Common, mandatoryField %>"></asp:RequiredFieldValidator>
        </div>
        <br />
        <asp:Button ID="btnUploadImage" runat="server" Text="Upload" OnClick="btnUploadImage_Click" meta:resourcekey="btnUploadImageResource1" CssClass="button" />
        <br />
        <asp:Label ID="lblUploadCompleted" runat="server" meta:resourcekey="lblUploadCompletedResource1" CssClass="info"></asp:Label>
        <br />
    </form>
</asp:Content>