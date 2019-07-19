using CasinoLibrary.Entities;
using System;
using System.Collections.Generic;

// Last modified 2019-04-14
namespace CasinoLibrary.GameFiles
{
    /// <summary>
    /// The Game class is responsible for tracking a game of BlackJack.
    /// </summary>
    public class Game
    {
        string userName;
        int winnings;

        /// <summary>
        /// Gets or sets a players userName for that game.
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                if(value != null || value != "")
                {
                    userName = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.UserName is invalid.");
                }
                
            }
        }

        /// <summary>
        /// Gets or sets a games result.
        /// </summary>
        public Result Result{ get; set; }

        /// <summary>
        /// Gets or sets a players winnings for that game.
        /// </summary>
        public int Winnings
        {
            get
            {
                return winnings;
            }
            set
            {
                if(value >= 0)
                {
                    winnings = value;
                }
                else
                {
                    throw new ArgumentException("Value for Game.Winnings cannot be below zero.");
                }
                
            }
        }

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="result"></param>
        /// <param name="winnings"></param>
        public Game(string userName, Result result, int winnings)
        {
            UserName = userName;
            Result = result;
            Winnings = winnings;
        }
    }
}