using System;
using System.Collections.Generic;
using System.Linq;
using CasinoLibrary.Entities.CardProperties;

// Last modified 2019-04-12
namespace CasinoLibrary.Entities
{
    /// <summary>
    /// Deck consists 52 Cards, has functions for Card/Deck based actions in BlackJack.
    /// </summary>
    [Serializable]
    public class Deck
    {
        // Fields
        List<Card> cards;
        // Constant value specifies the maximum number of Cards in a Deck.
        const int MAX_NUM_OF_CARDS = 52;

        /// <summary>
        /// Gets and Sets a list of Cards.
        /// </summary>
        public List<Card> Cards
        {
            get { return cards; }
            set
            {
                if (value != null)
                {
                    cards = value;
                }
                else
                {
                    throw new ArgumentException("Value of Deck.Cards is invoking a null/invalid reference.");
                }
            }
        }

        /// <summary>
        /// Default Constructor for Deck.
        /// </summary>
        public Deck()
        {
            Cards = new List<Card>();
            Init();
        }

        /// <summary>
        /// Initializes the Cards in a Deck, 4 Suits 13 Ranks each.
        /// </summary>
        public void Init()
        {
            for (int suit = 0; suit < 4; suit++)
            {
                for (int rank = 1; rank <= 13; rank++)
                {
                    if (rank == 1)
                    {
                        cards.Add(new Card(new Rank("A"), (Suit)suit, $"~/images/Cards/A{Card.SuitDisplayFormat((Suit)suit)}.png"));
                    }
                    else if (rank == 11)
                    {
                        cards.Add(new Card(new Rank("J"), (Suit)suit, $"~/images/Cards/J{Card.SuitDisplayFormat((Suit)suit)}.png"));
                    }
                    else if (rank == 12)
                    {
                        cards.Add(new Card(new Rank("Q"), (Suit)suit, $"~/images/Cards/Q{Card.SuitDisplayFormat((Suit)suit)}.png"));
                    }
                    else if (rank == 13)
                    {
                        cards.Add(new Card(new Rank("K"), (Suit)suit, $"~/images/Cards/K{Card.SuitDisplayFormat((Suit)suit)}.png"));
                    }
                    else
                    {
                        cards.Add(new Card(new Rank(rank.ToString()), (Suit)suit, $"~/images/Cards/{rank}{Card.SuitDisplayFormat((Suit)suit)}.png"));
                    }
                }
            }

        }

        /// <summary>
        /// Returns the first Card in the Deck.
        /// </summary>
        /// <returns></returns>
        public Card PickACard()
        {
            Card card = Cards.ElementAt(0);
            Cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Shuffles the Cards in the Deck.
        /// </summary>
        public void Shuffle()
        {
            int j;
            int n = MAX_NUM_OF_CARDS;
            Random rand = new Random();

            while (--n >= 0)
            {
                for (int i = 0; i < n; i++)
                {
                    j = rand.Next(0, n);
                    Card temp = Cards[j];
                    Cards[j] = Cards[i];
                    Cards[i] = temp;
                }
            }
        }

        /// <summary>
        /// Returns the amount of Cards in the Deck as a int.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return Cards.Count;
        }

    }
}
