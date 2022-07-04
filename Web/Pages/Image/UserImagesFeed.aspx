<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserImagesFeed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.UserImagesFeed" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div align="center">
            <asp:GridView ID="gvUserImages" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvUserImages_PageIndexChanging" Width="192px">
                <Columns>
                   <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImagePath") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Title" DataField="Title" />
                    <asp:BoundField HeaderText="Id" DataField="CategoryId" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <br />
</asp:Content>
