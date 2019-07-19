using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Last modified 2019-04-12
namespace CasinoLibrary.Entities
{
    /// <summary>
    /// Dealer class inherits Players class and has specific functions for participating in BlackJack.
    /// </summary>
    [Serializable]
    public class Dealer : Player
    {
        /// <summary>
        /// Constructor to the dealer, inherits from parent class Player
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="balance"></param>
        public Dealer(string userName, int balance)
            :base(userName, balance)
        {

        }

        /// <summary>
        /// Returns a Card from a Deck.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public Card Hit(Deck deck)
        {
            return deck.PickACard();
        }

        /// <summary>
        /// Shuffles the cards in a Deck.
        /// </summary>
        /// <param name="deck"></param>
        public void Shuffle(Deck deck)
        {
            deck.Shuffle();            
        }
    }
}
