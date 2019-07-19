using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Last modified 2019-03-15
namespace CasinoLibrary.CardProperties
{
    /// <summary>
    /// This class assigns Ranks and the value of the Rank for each Card in the Deck.
    /// </summary>
   public class Rank
    {
        //Fields
        string rankName;
        int rankValue;

        /// <summary>
        /// Gets and Sets the RankName.
        /// </summary>
        public string RankName {
            get { return rankName; }
            set {
                if(value != null || value != "")
                {
                    rankName = value;
                }
            }
        }

        /// <summary>
        /// Gets the RankValue.
        /// </summary>
        public int RankValue
        {
            get
            {
                switch (RankName)
                {
                    case "A":
                        rankValue = 1;
                        break;
                    case "2":
                        rankValue = 2;
                        break;
                    case "3":
                        rankValue = 3;
                        break;
                    case "4":
                        rankValue = 4;
                        break;
                    case "5":
                        rankValue = 5;
                        break;
                    case "6":
                        rankValue = 6;
                        break;
                    case "7":
                        rankValue = 7;
                        break;
                    case "8":
                        rankValue = 8;
                        break;
                    case "9":
                        rankValue = 9;
                        break;
                    case "10":
                    case "J":
                    case "Q":
                    case "K":
                        rankValue = 10;
                        break;
                }
                return rankValue;
            }
        }

        /// <summary>
        /// Constructor for Class Rank.
        /// </summary>
        /// <param name="rankName"></param>
        public Rank(string rankName)
        {
            RankName = rankName;
        }
    }
}
