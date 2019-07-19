using CasinoLibrary.Entities;
using CasinoLibrary.GameFiles;
using System;

// Last modified 2019-04-07
namespace CasinoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Self class declaration for non-static function calls.
            Program self = new Program();

            // Creates a deck of cards for a game of BlackJack.
            Deck deck = new Deck();

            // Creates Players for a game of BlackJack
            Dealer dealer = new Dealer("Dealer", 1000000);
            Player player1 = new Player("Player1", 10000);

            // Creates new game instance.
            BlackJack blackjack = new BlackJack(deck, dealer, player1);

            String userInput;
            do
            {
                // Clears the screen for round.
                Console.Clear();

                // Asks Player for wager amount
                self.ShowWagerUI(blackjack);

                // Sets the Dealers wager amount
                blackjack.PlaceDealerWager();

                // Sets up the game of BlackJack
                blackjack.SetUpGame();

                // A quick check to see if distributed cards hold a BlackJack set.
                blackjack.CheckBlackJackSet();

                // Displays UI at start of game.
                self.ShowUIOnStart(blackjack);

                // Displays UI for BlackJack game.
                self.StartRound(blackjack);

                // Determines the start of a new round.
                Console.Write("Play another round? (Y/N): ");
                userInput = Console.ReadLine();

                if (userInput.Equals("Y"))
                {
                    if (blackjack.Player.Balance != 0)
                    {
                        blackjack.NewGame();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Sorry your balance is ${blackjack.Player.Balance}, you can't play anymore.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write($"Thanks for playing {player1.UserName}.");
                }
            } while (userInput.Equals("Y") && blackjack.Player.Balance > 0);

            Console.Read();
        }

        /// <summary>
        /// Runs through a round of BlackJack
        /// </summary>
        /// <param name="blackjack"></param>
        public void StartRound(BlackJack blackjack)
        {
            String userInput;
            do
            {
                // Ask the user if they choose to hit a new card, stay or double-down.
                Console.Write("Do you want to Hit, Stand or Double-Down (H/S/D): ");
                userInput = Console.ReadLine();
                if (userInput.Equals("H"))
                {
                    if (blackjack.IsPlayersHitValid())
                    {
                        Console.Clear();
                        // Displays UI at start of game.
                        ShowUIOnStart(blackjack);
                    }
                    else
                    {
                        Console.Clear();
                        // Dealers takes the winnings.
                        blackjack.Dealer.Balance += blackjack.GameBalance;
                        // Displays UI at end of game.
                        ShowUIOnEnd(blackjack);
                        Console.WriteLine("Dealer wins, You went over 21!");
                    }
                }
                else if (userInput.Equals("S"))
                {
                    // Dealer will simulate their turn for the BlackJack round.
                    blackjack.DealerAI();
                    if (blackjack.IsPlayersStandValid())
                    {
                        Console.Clear();
                        // Player takes the winnings.
                        blackjack.Player.Balance += blackjack.GameBalance;
                        // Display UI at end of game.
                        ShowUIOnEnd(blackjack);
                        Console.WriteLine("Player wins!");
                    }
                    else
                    {
                        if (blackjack.IsGamePointsComparable())
                        {
                            Console.Clear();
                            // Player and Dealer split winnings.
                            blackjack.Dealer.Balance += (blackjack.GameBalance / 2);
                            blackjack.Player.Balance += (blackjack.GameBalance / 2);
                            // Displays UI at end of game.
                            ShowUIOnEnd(blackjack);
                            Console.WriteLine("It's a tie!");
                        }
                        else
                        {
                            if (blackjack.IsDealersMoveValid())
                            {
                                Console.Clear();
                                // Dealer takes the winnings.
                                blackjack.Dealer.Balance += blackjack.GameBalance;
                                // Displays UI at end of game.
                                ShowUIOnEnd(blackjack);
                                Console.WriteLine("Dealer wins!");
                            }
                            else
                            {
                                Console.Clear();
                                // Player takes the winnings.
                                blackjack.Player.Balance += blackjack.GameBalance;
                                // Displays UI at end of game.
                                ShowUIOnEnd(blackjack);
                                Console.WriteLine("Player wins!");

                            }
                        }
                    }
                }
                else if (userInput.Equals("D"))
                {
                    // Dealer will simulate their turn for the BlackJack round.
                    blackjack.DealerAI();
                    if (blackjack.IsPlayersDoubleDownValid())
                    {
                        Console.Clear();
                        // Player takes the winnings.
                        blackjack.Player.Balance += blackjack.GameBalance;
                        // Displays UI at end of game.
                        ShowUIOnEnd(blackjack);
                        Console.WriteLine("Player wins!");
                    }
                    else
                    {
                        if (blackjack.IsGamePointsComparable())
                        {
                            Console.Clear();
                            // Player and Dealer split winnings.
                            blackjack.Dealer.Balance += (blackjack.GameBalance / 2);
                            blackjack.Player.Balance += (blackjack.GameBalance / 2);
                            // Displays UI at end of game.
                            ShowUIOnEnd(blackjack);
                            Console.WriteLine("It's a tie!");
                        }
                        else
                        {
                            if (blackjack.IsDealersMoveValid())
                            {
                                Console.Clear();
                                // Dealer takes the winnings.
                                blackjack.Dealer.Balance += blackjack.GameBalance;
                                // Displays UI at end of game.
                                ShowUIOnEnd(blackjack);
                                Console.WriteLine("Dealer wins!");
                            }
                        }
                    }
                }
                else
                {
                    // Report an error message.
                    Console.WriteLine("Invalid entry");
                }
            } while ((userInput != "S" && userInput != "D") && blackjack.PlayersPoints <= 21);
        }

        /// <summary>
        /// Shows UI at start of game, sets the Players wager amount.
        /// </summary>
        /// <param name="blackjack"></param>
        public void ShowWagerUI(BlackJack blackjack)
        {
            int playerWager;
            do
            {
                Console.Write("How much do you want to wager?: ");
                playerWager = int.Parse(Console.ReadLine());
                if (playerWager < blackjack.Player.Balance)
                {
                    if (playerWager <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Wager amount ${playerWager} is below or equal to 0, try again.");
                        Console.ResetColor();
                    }
                    else
                    {
                        blackjack.PlacePlayerWager(playerWager);
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wager amount ${playerWager} is above/equal to your balance of ${blackjack.Player.Balance}, try again.");
                    Console.ResetColor();
                }
            } while (playerWager >= blackjack.Player.Balance || playerWager <= 0);
            Console.Clear();

        }

        /// <summary>
        /// Shows UI at start of and during the game of BlackJack.
        /// </summary>
        /// <param name="game"></param>
        public void ShowUIOnStart(BlackJack blackjack)
        {
            // Shows the game information.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Current Winnings: ${blackjack.GameBalance}");
            Console.ResetColor();
            // Shows the Dealers information.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{blackjack.Dealer.UserName}");
            Console.ResetColor();
            Console.WriteLine($"\tBalance: ${blackjack.Dealer.Balance}");
            Console.WriteLine("\tCards:");
            Console.WriteLine($"\t - {blackjack.DealersCards[0].ToString()}");
            // Shows the Players information.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{blackjack.Player.UserName}");
            Console.ResetColor();
            Console.WriteLine($"\tBalance: ${blackjack.Player.Balance}");
            Console.WriteLine("\tCards:");
            foreach (Card card in blackjack.PlayersCards)
            {
                Console.WriteLine($"\t - {card.ToString()}");
            }
            Console.WriteLine($"\tCard Score: {blackjack.PlayersPoints}");
        }

        /// <summary>
        /// Shows UI at end of the game of BlackJack.
        /// </summary>
        /// <param name="game"></param>
        public void ShowUIOnEnd(BlackJack blackjack)
        {
            // Shows the Dealers information.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{blackjack.Dealer.UserName}");
            Console.ResetColor();
            Console.WriteLine($"\tBalance: ${blackjack.Dealer.Balance}");
            Console.WriteLine("\tCards:");
            foreach (Card card in blackjack.DealersCards)
            {
                Console.WriteLine($"\t - {card.ToString()}");
            }
            Console.WriteLine($"\tCard Score: {blackjack.DealersPoints}");
            // Shows the Players information.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{blackjack.Player.UserName}");
            Console.ResetColor();
            Console.WriteLine($"\tBalance: ${blackjack.Player.Balance}");
            Console.WriteLine("\tCards:");
            foreach (Card card in blackjack.PlayersCards)
            {
                Console.WriteLine($"\t - {card.ToString()}");
            }
            Console.WriteLine($"\tCard Score: {blackjack.PlayersPoints}");
        }
    }
}