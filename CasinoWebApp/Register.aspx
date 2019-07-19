<%@ Page Title="Register" Language="C#" MasterPageFile="~/CasinoWebApp.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CasinoWebApp.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="styles/Register.css" type="text/css" />
    <script src="scripts/TextBoxScripting.js" type="text/javascript"></script>

    <div class="register">
        <header>
            <h1>Register</h1>
        </header>
        <asp:TextBox ID="txtUsername" runat="server" Text="Username" ForeColor="Gray" onfocus="clearPlaceholder('Username', this)" onblur="resetPlaceHolder('Username', this)"></asp:TextBox>
        <asp:TextBox ID="txtPass" runat="server" Text="Password" ForeColor="Gray" onfocus="clearPlaceholder('Password', this)" onblur="resetPlaceHolder('Password', this)"></asp:TextBox>
        <asp:TextBox ID="txtFirstName" runat="server" Text="First Name" ForeColor="Gray" onfocus="clearPlaceholder('First Name', this)" onblur="resetPlaceHolder('First Name', this)"></asp:TextBox>
        <asp:TextBox ID="txtLastName" runat="server" Text="Last Name" ForeColor="Gray" onfocus="clearPlaceholder('Last Name', this)" onblur="resetPlaceHolder('Last Name', this)"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" Text="Email" ForeColor="Gray" onfocus="clearPlaceholder('Email', this)" onblur="resetPlaceHolder('Email', this)"></asp:TextBox>

        <asp:Button ID="btnRegister" runat="server" Text="Submit" OnClick="btnRegister_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
    </div>
</asp:Content>
