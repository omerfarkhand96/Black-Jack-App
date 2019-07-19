<%@ Page Title="How to Play" Language="C#" MasterPageFile="~/CasinoWebApp.Master" AutoEventWireup="true" CodeBehind="HowToPlay.aspx.cs" Inherits="CasinoWebApp.HowToPlay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="styles/HowToPlay.css" type="text/css" />

    <div class="howTo">
        <header>
            <h1>How To Play</h1>
        </header>
        <p><strong>1: </strong>Blackjack is played between a Player (Human) and the Dealer (AI).</p>
        <p><strong>2: </strong>The Game is played with one single Deck of 52 Cards.</p>
        <p><strong>3: </strong>The Game starts with the Dealer dealing 4 Cards, 2 to the Player and 2 to it-self.</p>
        <p><strong>4: </strong>The Player can only see one Card from the Dealer at the start.</p>
        <p><strong>5: </strong>The Player then has a choice to Hit or Stand.</p>
        <p><strong>6: </strong>Hit means that the Player draws another Card from the Deck.</p>
        <p><strong>7: </strong>Stand means that the Player is content with their score.</p>
        <p><strong>8: </strong>Double Down means that the Player doubles  wager, draws another Card and must stand on the current hand.</p>
        <p><strong>9: </strong>The point of the Game is to get as close as possible to the score of 21 without going over it.</p>
        <p><strong>10: </strong>The Dealer will try to beat or tie with your score depending on the circumstances.</p>
        <p><strong>11: </strong>The Player can win the Game by either having a higher score than the Dealer or the Dealer goes over 21.</p>
        <p><strong>12: </strong>The Player can lose if their score is lower than the Dealers or the Player goes over 21.</p>
        <p>
            <strong>13: </strong>When a Player/Dealer gets a Card set of an Ace and a Card of value 10 at the start of the game. 
         Then the Player/Dealer has achieved a BlackJack set and now has a score of 21.
        </p>
    </div>
</asp:Content>
