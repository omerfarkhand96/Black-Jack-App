<%@ Page Title="Blackjack" Language="C#" MasterPageFile="~/CasinoWebApp.Master" AutoEventWireup="true" CodeBehind="Blackjack.aspx.cs" Inherits="CasinoWebApp.AuthenticatedPages.Blackjack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../styles/Blackjack.css" type="text/css" />
    <div class="gridArea">
        <div class="gameBoard">
            <div class="boardImage">
                <asp:Image ID="dealerCard1" runat="server" Style="left: 625px" CssClass="cardSettings dealerCard" />
                <asp:Image ID="dealerCard2" runat="server" Style="left: 650px" CssClass="cardSettings dealerCard" />
                <asp:Image ID="dealerCard3" runat="server" Style="left: 675px" CssClass="cardSettings dealerCard" />
                <asp:Image ID="dealerCard4" runat="server" Style="left: 700px" CssClass="cardSettings dealerCard" />
                <asp:Image ID="dealerCard5" runat="server" Style="left: 725px" CssClass="cardSettings dealerCard" />
                <asp:Image ID="dealerCard6" runat="server" Style="left: 750px" CssClass="cardSettings dealerCard" />
                <asp:Image ID="dealerCard7" runat="server" Style="left: 775px" CssClass="cardSettings dealerCard" />

            <asp:Image ID="deckOfCards" runat="server" CssClass="cardSettings deckCard" />

                <asp:Image ID="playerCard1" runat="server" Style="left: 555px; top: 540px; transform: rotate(45deg);" CssClass="cardSettings playerCard" />
                <asp:Image ID="playerCard2" runat="server" Style="left: 670px; top: 615px; transform: rotate(30deg);" CssClass="cardSettings playerCard" />
                <asp:Image ID="playerCard3" runat="server" Style="left: 785px; top: 650px; transform: rotate(10deg);" CssClass="cardSettings playerCard" />
                <asp:Image ID="playerCard4" runat="server" Style="left: 900px; top: 660px; transform: rotate(0deg);" CssClass="cardSettings playerCard" />
                <asp:Image ID="playerCard5" runat="server" Style="left: 1015px; top: 650px; transform: rotate(170deg);" CssClass="cardSettings playerCard" />
                <asp:Image ID="playerCard6" runat="server" Style="left: 1130px; top: 625px; transform: rotate(160deg);" CssClass="cardSettings playerCard" />
                <asp:Image ID="playerCard7" runat="server" Style="left: 1245px; top: 570px; transform: rotate(145deg);" CssClass="cardSettings playerCard" />
            </div>
        </div>

        <div class="gameOutput">
            <asp:Label ID="lblDealerBalance" runat="server" Text="Dealer Balance: "></asp:Label>
            <br />
            <asp:Label ID="lblDealerWager" runat="server" Text="Dealer Wager: "></asp:Label>
            <br />
            <asp:Label ID="lblDealerScore" runat="server" Text="Dealer Score: "></asp:Label>
            <br />
            <hr />
            <asp:Label ID="lblMessage" runat="server" Text="Welcome to BlackJack!"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblWinnings" runat="server" Text="Current Winnings: "></asp:Label>
            <br />
            <hr />
            <asp:Label ID="lblPlayerBalance" runat="server" Text="Player Balance: "></asp:Label>
            <br />
            <asp:Label ID="lblPlayerWager" runat="server" Text="Player Wager: "></asp:Label>
            <br />
            <asp:Label ID="lblPlayerScore" runat="server" Text="Player Score: "></asp:Label>
        </div>

        <div class="gameUI">
            <div class="wager">
                <asp:Label ID="lblBet" Text="Place a Wager" runat="server"></asp:Label><br />
                <br />
                <asp:TextBox ID="txtWager" TextMode="Number" runat="server" minimumvalue="1" step="50"></asp:TextBox>
                <asp:Button ID="btnWager" runat="server" Text="Place Wager" OnClick="btnWager_Click" />
            </div>
            <hr />
            <div id="Buttons" class="buttons">
                <asp:Button ID="btnNewGame" runat="server" Text="Play Again" OnClick="btnNewGame_Click" />
                <asp:Button ID="btnPlay" runat="server" Text="Play" OnClick="btnPlay_Click" />
                <hr />
                <asp:Button ID="btnHit" runat="server" Text="Hit" OnClick="btnHit_Click" /><br />
                <asp:Button ID="btnDoubleDown" runat="server" Text="Double Down" OnClick="btnDoubleDown_Click" /><br />
                <asp:Button ID="btnStand" runat="server" Text="Stand" OnClick="btnStand_Click" />
            </div>

        </div>
    </div>
</asp:Content>
