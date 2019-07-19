using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasinoLibrary.DAL;
using CasinoLibrary.Entities;

// Last modified 2019-04-18
namespace CasinoLibrary.GameFiles
{
    /// <summary>
    /// This class will handle all the game logic for BlackJack.
    /// </summary>
    [Serializable]
    public class BlackJack
    {
        //fields
        List<Card> dealersCards;
        int dealersPoints;
        List<Card> playersCards;
        int playersPoints;
        int gameBalance = 0;
        int dealerWager = 0;
        int gameMinWager = 0;

        /// <summary>
        /// Gets or sets the Cards for a Dealer.
        /// </summary>
        public List<Card> DealersCards
        {
            get
            {
                return dealersCards;
            }
            set
            {
                if (value != null)
                {
                    dealersCards = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.DealersCards is invoking a null/invalid reference.");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the points for a dealer.
        /// </summary>
        public int DealersPoints
        {
            get
            {
                if (dealersPoints != 21)
                {
                    dealersPoints = 0;
                    foreach (Card c in DealersCards)
                    {
                        dealersPoints += c.CardValue();
                    }
                    return dealersPoints;
                }
                return dealersPoints;

            }
            set
            {
                if (value >= 0)
                {
                    dealersPoints = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.DealersPoints cannot be set below zero.");
                }
            }
        }

        /// <summary>
        /// Returns the Cards for a Player.
        /// </summary>
        public List<Card> PlayersCards
        {
            get
            {
                return playersCards;
            }
            set
            {
                if (value != null)
                {
                    playersCards = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.PlayersCards is invoking a null/invalid reference.");
                }
            }
        }

        /// <summary>
        /// Gets the points for a player.
        /// </summary>
        public int PlayersPoints
        {
            get
            {
                if (playersPoints != 21)
                {
                    playersPoints = 0;
                    foreach (Card c in PlayersCards)
                    {
                        playersPoints += c.CardValue();
                    }
                    return playersPoints;
                }
                return playersPoints;
            }
            set
            {
                if (value >= 0)
                {
                    playersPoints = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.PlayersPoints cannot be set below zero.");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the overall balance being played for 
        /// within that game.
        /// </summary>
        public int GameBalance
        {
            get
            {
                return gameBalance;
            }
            set
            {
                if (value >= 0)
                {
                    gameBalance = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.GameBalance cannot be set below zero.");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the wager placed by the Dealer during the game.
        /// </summary>
        public int DealerWager
        {
            get
            {
                return dealerWager;
            }
            set
            {
                if (value >= 0)
                {
                    dealerWager = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.dealerWager cannot be set below 0.");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the minimum wager placed by the Player during the game.
        /// </summary>
        public int GameMinWager
        {
            get
            {
                return gameMinWager;
            }
            set
            {
                if (value >= 0)
                {
                    gameMinWager = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.GameMinWager cannot be set below 0.");
                }
            }
        }

        /// <summary>
        /// Gets and Sets the Deck.
        /// </summary>
        public Deck Deck { get; set; }

        /// <summary>
        /// Gets and Sets the game Dealer.
        /// </summary>
        public Dealer Dealer { get; set; }

        /// <summary>
        /// Gets and Sets the game Player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets and Sets the list of players. 
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Constructor for Game class.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="dealer"></param>
        /// <param name="player"></param>
        public BlackJack(Deck deck, Dealer dealer, Player player)
        {
            Deck = deck;
            Dealer = dealer;
            Player = player;
            // Players = players;
            DealersCards = new List<Card>();
            PlayersCards = new List<Card>();
        }

        /// <summary>
        /// Sets up the AI for the Dealer during the game of BlackJack.
        /// </summary>
        /// <param name="bJ"></param>
        /// <param name="dealer"></param>
        /// <param name="deck"></param>
        public void DealerAI()
        {
            while (PlayersPoints > DealersPoints && DealersPoints <= 21)
            {
                DealersCards.Add(Dealer.Hit(Deck));
            }
        }

        /// <summary>
        /// Starts a new game of BlackJack by reseting all previous values.
        /// </summary>
        public void NewGame()
        {
            Deck = new Deck();
            DealersCards.Clear();
            DealersPoints = 0;
            PlayersCards.Clear();
            PlayersPoints = 0;
            GameBalance = 0;
            GameMinWager = 0;

        }

        /// <summary>
        /// Conditional function checks the legality of a players set wager.
        /// </summary>
        /// <param name="playerWager"></param>
        /// <returns></returns>
        public Boolean IsPlayersWagerValid(int playerWager)
        {
            if (playerWager > 0 && playerWager <= Player.Balance)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if wager can be doubled down from the player balance.
        /// </summary>
        /// <returns></returns>
        public Boolean IsDoubleDownWagerValid()
        {
            // Checks if double down can be initiated based on current player balance.
            if (Player.Balance - gameMinWager >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Player places a wager on how much they're willing to gamble for that 
        /// specific game of BlackJack.
        /// </summary>
        /// <param name="wager"></param>
        public void PlacePlayerWager(int playerWager)
        {
            if (playerWager > GameMinWager && playerWager <= Player.Balance)
            {
                GameMinWager = playerWager;
                Player.Balance = Player.Balance - playerWager;
                GameBalance += playerWager;
            }
            else if (playerWager == GameMinWager && playerWager <= Player.Balance)
            {
                Player.Balance = Player.Balance - playerWager;
                GameBalance += playerWager;
            }
            else
            {
                // Wager shouldn't be able to be placed.
                throw new ArgumentException("The wager placed is invalid, wager should be above or equal to last placed wager.");
            }
        }

        /// <summary>
        /// Dealer places a wager between the minimum wager set by player and Dealers balance.
        /// </summary>
        public void PlaceDealerWager()
        {
            Random random = new Random();
            DealerWager = random.Next(GameMinWager, GameMinWager + (GameMinWager / 2));

            Dealer.Balance = Dealer.Balance - DealerWager;
            GameBalance += DealerWager;
        }

        /// <summary>
        /// Sets up the game of BlackJack with a card shuffle and distribution to
        /// active players
        /// </summary>
        public void SetUpGame()
        {
            // Dealer shuffles the deck of cards.
            Dealer.Shuffle(Deck);

            // Dealer hits 4 cards from the deck.
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                {
                    PlayersCards.Add(Dealer.Hit(Deck));

                }
                else
                {
                    DealersCards.Add(Dealer.Hit(Deck));
                }
            }
        }

        /// <summary>
        /// Checks to see if Player or Dealer has a starting BlackJack set.
        /// </summary>
        public void CheckBlackJackSet()
        {
            // Checks to see if Dealers Card set is BlackJack.
            if ((DealersCards[0].CardValue() == 1 && DealersCards[1].CardValue() == 10) ||
                (DealersCards[0].CardValue() == 10 && DealersCards[1].CardValue() == 1))
            {
                DealersPoints = 21;
            }
            // Checks to see if Players Card set is BlackJack.
            if ((PlayersCards[0].CardValue() == 1 && PlayersCards[1].CardValue() == 10) ||
                (PlayersCards[0].CardValue() == 10 && PlayersCards[1].CardValue() == 1))
            {
                PlayersPoints = 21;
            }
        }

        /// <summary>
        /// Checks to see if the Players decision to hit is legal.
        /// </summary>
        /// <returns></returns>
        public Boolean IsPlayersHitValid()
        {
            PlayersCards.Add(Dealer.Hit(Deck));
            // Check if players points are equal to/below 21.
            if (PlayersPoints <= 21)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the Players decision to stand is legal.
        /// </summary>
        /// <returns></returns>
        public Boolean IsPlayersStandValid()
        {
            // Check values of dealer and player and compare.
            if (PlayersPoints > DealersPoints)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the Players decision to double-down is legal.
        /// </summary>
        /// <returns></returns>
        public Boolean IsPlayersDoubleDownValid()
        {
            // Doubles the initial wage.
            PlacePlayerWager(gameMinWager);
            // Checks to see if players hit and stand are successful.
            if (IsPlayersHitValid() && IsPlayersStandValid() && Player.Balance >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if Players points and Dealers points compare.
        /// </summary>
        /// <returns></returns>
        public Boolean IsGamePointsComparable()
        {
            if (PlayersPoints == DealersPoints)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the Dealers decisions to hit/stand legal.
        /// </summary>
        /// <returns></returns>
        public Boolean IsDealersMoveValid()
        {
            // Checks value of dealers points
            if (DealersPoints <= 21)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// On players win, game earnings are added to their balance.
        /// </summary>
        public void PlayerEarnsWinnings()
        {
            // Player takes the winnings.
            Player.Balance += GameBalance;
        }

        /// <summary>
        /// On dealers win, game earnings are added to their balance.
        /// </summary>
        public void DealerEarnsWinnings()
        {
            // Dealer takes the winnings.
            Dealer.Balance += GameBalance;
        }

        /// <summary>
        /// On draw, game earnings are shared between the players.
        /// </summary>
        public void ShareWinnings()
        {
            // Player and Dealer split winnings.
            Dealer.Balance += (GameBalance / 2);
            Player.Balance += (GameBalance / 2);
        }

        /// <summary>
        /// On game win, all information is saved into their respective databases.
        /// </summary>
        /// <param name="playerDB"></param>
        /// <param name="recordDB"></param>
        /// <param name="gameDB"></param>
        public void SaveWin(PlayerDAO playerDB, RecordDAO recordDB, GameDAO gameDB)
        {
            // Updates player table information.
            playerDB.UpdateBalance(Player);
            // Updates record table information.
            Record record = recordDB.ReadRecordByUserName(Player.UserName);
            record.GamesPlayed++;
            record.Wins++;
            recordDB.UpdateRecord(record);
            // Updates game table information.
            gameDB.AddNewGame(new Game(Player.UserName, Result.WON, GameBalance));
        }

        /// <summary>
        /// On game loss, all information is saved into their respective databases.
        /// </summary>
        /// <param name="playerDB"></param>
        /// <param name="recordDB"></param>
        /// <param name="gameDB"></param>
        public void SaveLoss(PlayerDAO playerDB, RecordDAO recordDB, GameDAO gameDB)
        {
            // Updates player table information.
            playerDB.UpdateBalance(Player);
            // Updates record table information.
            Record record = recordDB.ReadRecordByUserName(Player.UserName);
            record.GamesPlayed++;
            record.Losses++;
            recordDB.UpdateRecord(record);
            // Updates game table information.
            gameDB.AddNewGame(new Game(Player.UserName, Result.LOST, 0));
        }

        /// <summary>
        /// On game draw, all information is saved into their respective databases.
        /// </summary>
        /// <param name="playerDB"></param>
        /// <param name="recordDB"></param>
        /// <param name="gameDB"></param>
        public void SaveDraw(PlayerDAO playerDB, RecordDAO recordDB, GameDAO gameDB)
        {
            // Updates player table information.
            playerDB.UpdateBalance(Player);
            // Updates record table information.
            Record record = recordDB.ReadRecordByUserName(Player.UserName);
            record.GamesPlayed++;
            record.Draws++;
            recordDB.UpdateRecord(record);
            // Updates game table information.
            gameDB.AddNewGame(new Game(Player.UserName, Result.DRAW, GameBalance / 2));
        }
    }
}
