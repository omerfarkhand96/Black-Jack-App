<%@ Page Title="Login" Language="C#" MasterPageFile="~/CasinoWebApp.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CasinoWebApp.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="styles/Login.css" type="text/css" />
    <script src="scripts/TextBoxScripting.js" type="text/javascript"></script>
    <div class="login">
        <header>
            <h1>Login</h1>
        </header>
            <asp:TextBox ID="txtUsername" runat="server" Text="Username" ForeColor="Gray" onfocus="clearPlaceholder('Username', this)" onblur="resetPlaceHolder('Username', this)"></asp:TextBox>
            <asp:TextBox ID="txtPass" runat="server" Text="Password" ForeColor="Gray" onfocus="clearPlaceholder('Password', this)" onblur="resetPlaceHolder('Password', this)"></asp:TextBox>

            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"/>
    </div>
</asp:Content>