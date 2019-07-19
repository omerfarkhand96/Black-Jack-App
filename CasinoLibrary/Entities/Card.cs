using CasinoLibrary.Entities.CardProperties;
using System;

// Last modified 2019-04-12
namespace CasinoLibrary.Entities
{
    /// <summary>
    /// Card class creates a card with a Rank and Suit, to be hold in a Deck.
    /// </summary>
    [Serializable]
    public class Card
    {
        string imgPath;
        static readonly string cardBackImgPath = "~/images/Cards/red_back.png";

        /// <summary>
        /// Gets and Sets the Rank.
        /// </summary>
        public Rank Rank { get; set; }

        /// <summary>
        /// Gets and Sets the Suit.
        /// </summary>
        public Suit Suit { get; set; }

        /// <summary>
        /// Gets or sets the imgPath.
        /// </summary>
        public string ImgPath
        {
            get { return imgPath; }
            set
            {
                if (value != null || value != "")
                {
                    imgPath = value;
                }
                else
                {
                    throw new ArgumentException("Value for Card.ImgPath is invalid.");
                }
            }
        }

        /// <summary>
        /// Gets the cardBackImgPath.
        /// </summary>
        public string CardBackImgPath
        {
            get { return cardBackImgPath; }
        }

        /// <summary>
        /// Constructor for the Card class.
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        public Card(Rank rank, Suit suit, string imgPath)
        {
            Suit = suit;
            Rank = rank;
            ImgPath = imgPath;
        }

        /// <summary>
        /// Displays the Suit of a Card in a string format.
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
        public static string SuitDisplayFormat(Suit suit)
        {
            //char suitAsChar = '\u0000';
            string suitAsString = null;
            switch (suit)
            {
                case Suit.CLUBS:
                    //suitAsChar = '\u2663';
                    suitAsString = "C";
                    break;
                case Suit.SPADES:
                    //suitAsChar = '\u2660';
                    suitAsString = "S";
                    break;
                case Suit.HEARTS:
                    //suitAsChar = '\u2665';
                    suitAsString = "H";
                    break;
                case Suit.DIAMONDS:
                    //suitAsChar = '\u2666';
                    suitAsString = "D";
                    break;
            }
            return suitAsString;
        }

        /// <summary>
        /// Returns the card value through RankValue as a int.
        /// </summary>
        /// <returns></returns>
        public int CardValue()
        {
            return Rank.RankValue;
        }

        /// <summary>
        /// Overridden ToString function outputs the string value for a Card instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Rank.RankName} of {SuitDisplayFormat(Suit)}";
        }
    }
}