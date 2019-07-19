<%@ Page Title="Main Menu" Language="C#" MasterPageFile="~/CasinoWebApp.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="CasinoWebApp.MainMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="styles/MainMenu.css" type="text/css" />

    <div class="mainMenu">
        <header>
            <h1>Casino App</h1>
            <h2>By: Omer F and Sharek K</h2>
        </header>
        <asp:Button ID="btnPlay" runat="server" Text="Play" OnClick="btnPlay_Click" />
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
        <asp:Button ID="btnHowToPlay" runat="server" Text="How to Play" OnClick="btnHowToPlay_Click" />
        <asp:Button ID="btnLeaderboard" runat="server" Text="Leaderboard" OnClick="btnLeaderboard_Click" />
    </div>
</asp:Content>