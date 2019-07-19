<%@ Page Title="Leaderboard" Language="C#" MasterPageFile="~/CasinoWebApp.Master" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs" Inherits="CasinoWebApp.Leaderboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="styles/Leaderboard.css" type="text/css" />
    <header style="text-align: center">
        <h1>Leaderboard</h1>
    </header>
    <div>
        <asp:GridView ID="GVLeaderboard" runat="server" CellPadding="3" GridLines="Horizontal" HorizontalAlign="Center"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" dth="1576px" Height="78px" Width="85%">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>

                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="GamesPlayed" HeaderText="Games Played" />
                <asp:BoundField DataField="Wins" HeaderText="Wins" />
                <asp:BoundField DataField="Losses" HeaderText="Loss" />
                <asp:BoundField DataField="Draws" HeaderText="Draw" />
                <asp:BoundField DataField="WinLossRatio" HeaderText="Win/Loss Ratio" />

            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#2c3e50" Font-Bold="True" HorizontalAlign="Left" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" />
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
    </div>
</asp:Content>
