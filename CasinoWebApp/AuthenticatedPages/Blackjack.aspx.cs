using CasinoLibrary.DAL;
using CasinoLibrary.Entities;
using CasinoLibrary.GameFiles;
using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;

// Last modified 2019-04-18
namespace CasinoWebApp.AuthenticatedPages
{
    public partial class Blackjack : System.Web.UI.Page
    {
        // Creates field for connection string for database relations.
        string connString = WebConfigurationManager.ConnectionStrings["CasinoDBConnString"].ConnectionString;
        // Creates fields for data access objects.
        GameDAO gameDB;
        PlayerDAO playerDB;
        RecordDAO recordDB;
        // Creates fields for a game of BlackJack.
        Dealer dealer;
        Player player;
        Deck deck;
        // Creates a new instance of BlackJack.
        BlackJack blackjack;
        Image[] dealerCards;
        Image[] playerCards;
        // Checks game state.
        Boolean gameOver = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initializes the dealer.
                dealer = new Dealer("Dealer", 1000000);
                // Initializes the player through Session Management.
                player = (Player)Session["Player"];
                // Initializes the deck of cards.
                deck = new Deck();
                // Starts new game of Blackjack.
                blackjack = new BlackJack(deck, dealer, player);
                ViewState["BlackJack"] = blackjack;

                // Loads all cards to the deck in a GUI format.
                foreach (Card card in deck.Cards)
                {
                    deckOfCards.ImageUrl = card.CardBackImgPath;
                }

                // Sets the value of starting message on load.
                lblDealerBalance.Text = $"Dealer Balance: {dealer.Balance}";
                lblDealerWager.Text = "Dealer Wager: ";
                lblDealerScore.Text = "Dealer Score: ";
                lblMessage.Text = "Welcome to BlackJack!";
                lblWinnings.Text = "Current Winnings: ";
                lblPlayerBalance.Text = $"Player Balance: {player.Balance}";
                lblPlayerWager.Text = "Player Wager: ";
                lblPlayerScore.Text = "Player Score: ";

                // Sets the value of starting wage on load.
                txtWager.Text = "0";

                // Sets state of buttons on load.
                btnWager.Enabled = true;
                btnPlay.Enabled = false;
                btnNewGame.Visible = false;
                btnNewGame.Enabled = false;
                btnHit.Enabled = false;
                btnHit.Visible = false;
                btnStand.Enabled = false;
                btnStand.Visible = false;
                btnDoubleDown.Enabled = false;
                btnDoubleDown.Visible = false;

                // Hides the dealers card at on page load
                dealerCard1.Visible = false;
                dealerCard2.Visible = false;
                dealerCard3.Visible = false;
                dealerCard4.Visible = false;
                dealerCard5.Visible = false;
                dealerCard6.Visible = false;
                dealerCard7.Visible = false;
            }
        }

        protected void btnWager_Click(object sender, EventArgs e)
        {
            int playerWager = int.Parse(txtWager.Text);
            if (((BlackJack)ViewState["BlackJack"]).IsPlayersWagerValid(playerWager))
            {
                ((BlackJack)ViewState["BlackJack"]).PlacePlayerWager(playerWager);
                lblMessage.Text = "Wager is set, click Play to start game.";
                lblPlayerWager.Text = $"Player Wager: {((BlackJack)ViewState["BlackJack"]).GameMinWager.ToString()}";
                txtWager.Enabled = false;
                btnWager.Enabled = false;
                btnPlay.Enabled = true;
            }
            else
            {
                if (playerWager == 0)
                {
                    lblMessage.Text = "Wager Equal to Zero: Set wager again.";
                }
                else if (playerWager < 0)
                {
                    lblMessage.Text = "Wager Below Zero: Set wager again.";
                }
                else
                {
                    lblMessage.Text = "Wager Above Player Balance: Set wager again.";
                }

            }
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Enabled = false;
            btnHit.Visible = true;
            btnHit.Enabled = true;
            btnStand.Visible = true;
            btnStand.Enabled = true;
            btnDoubleDown.Visible = true;
            btnDoubleDown.Enabled = true;

            // Sets the dealers wager amount.
            ((BlackJack)ViewState["BlackJack"]).PlaceDealerWager();

            // Sets up the new game
            ((BlackJack)ViewState["BlackJack"]).SetUpGame();

            // Checks to see if either party holds a BlackJack set.
            ((BlackJack)ViewState["BlackJack"]).CheckBlackJackSet();

            // Updates all values to be displayed on GUI.
            RefreshBoard();

            // Alerts the user on possible options.
            lblMessage.Text = "Choose your move Hit/Double Down/Stand.";

        }

        protected void btnHit_Click(object sender, EventArgs e)
        {
            // Player selects another card, function checks it's validity.
            if (((BlackJack)ViewState["BlackJack"]).IsPlayersHitValid())
            {
                // Alerts the user on possible options.
                lblMessage.Text = "Choose your move Hit/Double Down/Stand.";
                RefreshBoard();
            }
            else
            {
                // Changes the state of the game.
                gameOver = true;
                ExecuteBust();
                RefreshBoard();
            }
        }

        protected void btnStand_Click(object sender, EventArgs e)
        {
            // Runs A.I for Dealer.
            ((BlackJack)ViewState["BlackJack"]).DealerAI();

            // Player stand, function checks it's validity.
            if (((BlackJack)ViewState["BlackJack"]).IsPlayersStandValid())
            {
                // Changes the state of the game.
                gameOver = true;
                ((BlackJack)ViewState["BlackJack"]).PlayerEarnsWinnings();
                ExecuteWin();
                RefreshBoard();
            }
            else
            {
                // Checks if game results in a draw.
                if (((BlackJack)ViewState["BlackJack"]).IsGamePointsComparable())
                {
                    // Changes the state of the game.
                    gameOver = true;
                    ((BlackJack)ViewState["BlackJack"]).ShareWinnings();
                    ExecuteDraw();
                    RefreshBoard();
                }
                else
                {
                    // Checks if Dealers move placed by A.I is valid.
                    if (((BlackJack)ViewState["BlackJack"]).IsDealersMoveValid())
                    {
                        // Changes the state of the game.
                        gameOver = true;
                        ((BlackJack)ViewState["BlackJack"]).DealerEarnsWinnings();
                        ExecuteLoss();
                        RefreshBoard();
                    }
                    else
                    {
                        // Changes the state of the game.
                        gameOver = true;
                        ((BlackJack)ViewState["BlackJack"]).PlayerEarnsWinnings();
                        ExecuteWin();
                        RefreshBoard();

                    }
                }
            }
        }

        protected void btnDoubleDown_Click(object sender, EventArgs e)
        {
            // Checks if player can even double down based on balance amount.
            if (((BlackJack)ViewState["BlackJack"]).IsDoubleDownWagerValid())
            {
                // Runs A.I for Dealer.
                ((BlackJack)ViewState["BlackJack"]).DealerAI();
                // Player stand, function checks it's validity.
                if (((BlackJack)ViewState["BlackJack"]).IsPlayersDoubleDownValid())
                {
                    // Changes the state of the game.
                    gameOver = true;
                    ((BlackJack)ViewState["BlackJack"]).PlayerEarnsWinnings();
                    ExecuteWin();
                    RefreshBoard();
                }
                else
                {
                    // Checks if game results in a draw.
                    if (((BlackJack)ViewState["BlackJack"]).IsGamePointsComparable())
                    {
                        // Changes the state of the game.
                        gameOver = true;
                        ((BlackJack)ViewState["BlackJack"]).ShareWinnings();
                        ExecuteDraw();
                        RefreshBoard();
                    }
                    else
                    {
                        // Checks if Dealers move placed by A.I is valid.
                        if (((BlackJack)ViewState["BlackJack"]).IsDealersMoveValid())
                        {
                            // Changes the state of the game.
                            gameOver = true;
                            ((BlackJack)ViewState["BlackJack"]).DealerEarnsWinnings();
                            ExecuteLoss();
                            RefreshBoard();
                        }
                        else
                        {
                            // Changes the state of the game.
                            gameOver = true;
                            ((BlackJack)ViewState["BlackJack"]).DealerEarnsWinnings();
                            ExecuteBust();
                            RefreshBoard();
                        }
                    }
                }
            }
            else
            {
                // Player cannot double down as balance would be negative.
                lblMessage.Text = "Balance below Zero: Player cannot double down.<br>Hit or Stand.";
            }
        }

        protected void btnNewGame_Click(object sender, EventArgs e)
        {
            gameOver = false;
            ((BlackJack)ViewState["BlackJack"]).NewGame();
            ClearBoard();
            lblMessage.Text = "Welcome to BlackJack!";
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            txtWager.Enabled = true;
            btnWager.Enabled = true;
            btnPlay.Visible = true;
        }

        public void RefreshBoard()
        {
            int playerCount = 0;
            int dealerCount = 0;
            playerCards = new Image[] { playerCard1, playerCard2, playerCard3, playerCard4, playerCard5, playerCard6, playerCard7 };
            //ViewState["PlayerCardImgs"] = playerCards;
            dealerCards = new Image[] { dealerCard1, dealerCard2, dealerCard3, dealerCard4, dealerCard5, dealerCard6, dealerCard7 };
            //ViewState["DealerCardImgs"] = dealerCards;

            // Players Info.
            foreach (Card card in ((BlackJack)ViewState["BlackJack"]).PlayersCards)
            {
                playerCards[playerCount].Visible = true;
                playerCards[playerCount].ImageUrl = card.ImgPath;
                playerCount++;
            }
            // Dealers Info.
            foreach (Card card in ((BlackJack)ViewState["BlackJack"]).DealersCards)
            {
                // Checks state of game to display dealers cards.
                if (gameOver == false)
                {
                    // Shows first card,
                    if (dealerCount == 0)
                    {
                        dealerCards[dealerCount].Visible = true;
                        dealerCards[dealerCount].ImageUrl = card.ImgPath;
                    }
                    // Hides second card.
                    else if (dealerCount == 1)
                    {
                        dealerCards[dealerCount].Visible = true;
                        dealerCards[dealerCount].ImageUrl = card.CardBackImgPath;
                    }
                    dealerCount++;
                }
                else if (gameOver == true)
                {
                    dealerCards[dealerCount].Visible = true;
                    dealerCards[dealerCount].ImageUrl = card.ImgPath;
                    dealerCount++;
                }
            }
            // Updates the information for game output.
            lblDealerBalance.Text = $"Dealer Balance: {((BlackJack)ViewState["BlackJack"]).Dealer.Balance}";
            lblDealerWager.Text = $"Dealer Wager: {((BlackJack)ViewState["BlackJack"]).DealerWager}";
            if (gameOver == false)
            {
                lblDealerScore.Text = $"Dealer Score: ?";
            }
            else if (gameOver == true)
            {
                lblDealerScore.Text = $"Dealer Score: {((BlackJack)ViewState["BlackJack"]).DealersPoints}";
            }
            lblWinnings.Text = $"Current Winnings: {((BlackJack)ViewState["BlackJack"]).GameBalance}";
            lblPlayerBalance.Text = $"Player Balance: {((BlackJack)ViewState["BlackJack"]).Player.Balance}";
            lblPlayerWager.Text = $"Player Wager: {((BlackJack)ViewState["BlackJack"]).GameMinWager}";
            lblPlayerScore.Text = $"Player Score: {((BlackJack)ViewState["BlackJack"]).PlayersPoints}";
        }

        public void ClearBoard()
        {
            int playerCount = 0;
            int dealerCount = 0;
            playerCards = new Image[] { playerCard1, playerCard2, playerCard3, playerCard4, playerCard5, playerCard6, playerCard7 };
            //ViewState["PlayerCardImgs"] = playerCards;
            dealerCards = new Image[] { dealerCard1, dealerCard2, dealerCard3, dealerCard4, dealerCard5, dealerCard6, dealerCard7 };
            //ViewState["DealerCardImgs"] = dealerCards;

            // Players Info.
            foreach (Image img in playerCards)
            {
                playerCards[playerCount].Visible = true;
                playerCards[playerCount].ImageUrl = "";
                playerCount++;
            }
            // Dealers Info.
            foreach (Image img in dealerCards)
            {
                dealerCards[dealerCount].Visible = false;
                dealerCards[dealerCount].ImageUrl = "";
                dealerCount++;
            }
            // Updates the information for game output.
            lblDealerBalance.Text = $"Dealer Balance: {((BlackJack)ViewState["BlackJack"]).Dealer.Balance}";
            lblDealerWager.Text = "Dealer Wager: ";
            lblDealerScore.Text = "Dealer Score: ";
            lblWinnings.Text = "Current Winnings: ";
            lblPlayerBalance.Text = $"Player Balance: {((BlackJack)ViewState["BlackJack"]).Player.Balance}";
            lblPlayerWager.Text = "Player Wager: ";
            lblPlayerScore.Text = "Player Score: ";
        }

        public void ExecuteWin()
        {
            CreateConnectionForDAO();
            ((BlackJack)ViewState["BlackJack"]).SaveWin(playerDB, recordDB, gameDB);
            lblMessage.Text = "You Win: You beat the dealers hand.";
            btnHit.Visible = false;
            btnHit.Enabled = false;
            btnStand.Visible = false;
            btnStand.Enabled = false;
            btnDoubleDown.Visible = false;
            btnDoubleDown.Enabled = false;
            btnPlay.Visible = false;
            btnNewGame.Visible = true;
            btnNewGame.Enabled = true;
        }

        public void ExecuteLoss()
        {
            CreateConnectionForDAO();
            ((BlackJack)ViewState["BlackJack"]).SaveLoss(playerDB, recordDB, gameDB);
            lblMessage.Text = "House Wins: Dealer beat your hand.";
            btnHit.Visible = false;
            btnHit.Enabled = false;
            btnStand.Visible = false;
            btnStand.Enabled = false;
            btnDoubleDown.Visible = false;
            btnDoubleDown.Enabled = false;
            btnPlay.Visible = false;
            btnNewGame.Visible = true;
            btnNewGame.Enabled = true;
        }

        public void ExecuteBust()
        {
            CreateConnectionForDAO();
            ((BlackJack)ViewState["BlackJack"]).SaveLoss(playerDB, recordDB, gameDB);
            lblMessage.Text = "Bust: Went over 21!";
            btnHit.Visible = false;
            btnHit.Enabled = false;
            btnStand.Visible = false;
            btnStand.Enabled = false;
            btnDoubleDown.Visible = false;
            btnDoubleDown.Enabled = false;
            btnPlay.Visible = false;
            btnNewGame.Visible = true;
            btnNewGame.Enabled = true;
        }

        public void ExecuteDraw()
        {
            CreateConnectionForDAO();
            ((BlackJack)ViewState["BlackJack"]).SaveDraw(playerDB, recordDB, gameDB);
            lblMessage.Text = "Push: Game ends in a draw.";
            btnHit.Visible = false;
            btnHit.Enabled = false;
            btnStand.Visible = false;
            btnStand.Enabled = false;
            btnDoubleDown.Visible = false;
            btnDoubleDown.Enabled = false;
            btnPlay.Visible = false;
            btnNewGame.Visible = true;
            btnNewGame.Enabled = true;
        }

        public void CreateConnectionForDAO()
        {
            playerDB = new PlayerDAO(connString);
            recordDB = new RecordDAO(connString);
            gameDB = new GameDAO(connString);
        }
    }
}