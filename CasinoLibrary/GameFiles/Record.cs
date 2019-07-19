using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Last modified 2019-04-18
namespace CasinoLibrary.GameFiles
{
    /// <summary>
    /// The Record class keeps track of every instance on a leaderboard.
    /// </summary>
    public class Record
    {
        string userName;
        int gamesPlayed, wins, losses, draws;
        double winLossRatio;

        /// <summary>
        /// Gets or sets the players username.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set
            {
                if (value != null || value != "")
                {
                    userName = value;
                }
                else
                {
                    throw new ArgumentException("Value for Record.UserName is invalid.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of games played by player.
        /// </summary>
        public int GamesPlayed
        {
            get { return gamesPlayed; }
            set
            {
                if (value >= 0)
                {
                    gamesPlayed = value;
                }
                else
                {
                    throw new ArgumentException("Value for Record.GamesPlayed cannot be below zero.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of games won by player.
        /// </summary>
        public int Wins
        {
            get { return wins; }
            set
            {
                if (value >= 0)
                {
                    wins = value;
                }
                else
                {
                    throw new ArgumentException("Value for Record.Wins cannot be below zero.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of games lost by player.
        /// </summary>
        public int Losses
        {
            get { return losses; }
            set
            {
                if (value >= 0)
                {
                    losses = value;
                }
                else
                {
                    throw new ArgumentException("Value for Record.Losses cannot be below zero.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of games ending as a draw for player.
        /// </summary>
        public int Draws
        {
            get { return draws; }
            set
            {
                if (value >= 0)
                {
                    draws = value;
                }
                else
                {
                    throw new ArgumentException("Value for Record.Draws cannot be below zero.");
                }
            }
        }

        /// <summary>
        /// Gets the performance ratio of a player in BlackJack.
        /// </summary>
        public float WinLossRatio
        {
            get
            {
                if (losses == 0)
                {
                    winLossRatio = (double) wins / 1;
                    winLossRatio = Math.Round(winLossRatio, 2);
                }
                else
                {
                    winLossRatio = (float) wins / losses;
                    winLossRatio = Math.Round(winLossRatio, 2);
                }
                return (float)winLossRatio;
            }
            set
            {
                if(value >= 0)
                {
                    winLossRatio = value;
                }
                else
                {
                    throw new ArgumentException("Value for Record.WinLossRatio cannot be below zero.");
                }
            }
        }

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="gamesplayed"></param>
        /// <param name="wins"></param>
        /// <param name="losses"></param>
        /// <param name="draws"></param>
        public Record(string userName, int gamesPlayed, int wins, int losses, int draws)
        {
            UserName = userName;
            GamesPlayed = gamesPlayed;
            Wins = wins;
            Losses = losses;
            Draws = draws;
        }
    }
}