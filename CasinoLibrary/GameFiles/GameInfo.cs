using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasinoLibrary.Entities;

namespace CasinoLibrary.GameFiles
{
    /// <summary>
    /// Responsible for keeping in track of a result of the game
    /// </summary>
   class GameInfo
    {
        //fields
        string userName;
        string result;
        int score;

        /// <summary>
        /// Getter and Setter for Result
        /// </summary>
        public string Result
        {
            get
            {
                return result;
            }

            set
            {
                value = result;
            }
        }

        /// <summary>
        /// Getter and Setter for Score
        /// </summary>
        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                value = score;
            }
        }

        /// <summary>
        /// Getter and Setter for Username
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                value = userName;
            }
        }

        /// <summary>
        /// Constructor for the class
        /// </summary>
        /// <param name="username"></param>
        /// <param name="result"></param>
        /// <param name="score"></param>
        public GameInfo(string username, string result, int score)
        {
            UserName = userName;
            Result = result;
            Score = score;
        }


    }
}
