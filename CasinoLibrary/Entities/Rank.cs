using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Last modified 2019-03-07
namespace CasinoApp.Entities
{
   class Rank
    {
        string rankName;
        int rankValue;

        public string RankName {
            get { return rankName; }
            set {
                if(value != null)
                {
                    rankName = value;
                }
            }
        }

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

        public Rank(string rankName)
        {
            RankName = rankName;
        }
    }
}
