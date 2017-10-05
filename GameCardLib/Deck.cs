using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Deck class
    /// </summary>
    public class Deck
    {
        #region fields
        private Stack<Card> cards;
        private int deckMultiplier;
        #endregion
        #region Properties
        /// <summary>
        /// Stack of cards
        /// </summary>
        private Stack<Card> Cards { get => cards; set => cards = value; }
        /// <summary>
        /// How many decks compose the main deck
        /// </summary>
        private int DeckMultiplier { get => deckMultiplier; set => deckMultiplier = value; }
        /// <summary>
        /// Count of cards in the deck
        /// </summary>
        public int Count { get => Cards.Count; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Deck() :this(1)
        {
        }
        /// <summary>
        /// Constructor that takes one argument: deckMultiplier
        /// </summary>
        /// <param name="deckMultiplier">Number of decks to form the deck</param>
        public Deck(int deckMultiplier)
        {
            DeckMultiplier = deckMultiplier;
            Cards = new Stack<Card>();
            FillDeckWithCards();
            Shuffle();
        }
        #endregion
        /// <summary>
        /// Discard the quantity of cards
        /// </summary>
        /// <param name="quantity">Quantity of cards to discard</param>
        /// <returns></returns>
        public bool DiscardCards(int quantity = 1)
        {
            for (int i = quantity; i > 0; i--)
            {
                Cards.Pop();
            }
            return true;
        }

        /// <summary>
        /// Make the deck
        /// </summary>
        private void FillDeckWithCards()
        {
            foreach (int deck in Enumerable.Range(1,DeckMultiplier))
            {
                foreach (EnumSuite suite in Enum.GetValues(typeof(EnumSuite)))
                {
                    foreach (EnumValue value in Enum.GetValues(typeof(EnumValue)))
                    {
                        Card card = new Card(value, suite);
                        Cards.Push(card);
                    }
                }
            }
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random();
            Cards = new Stack<Card>(Cards.OrderBy(x => rnd.Next()));
        }

        /// <summary>
        /// Deck ToString method
        /// </summary>
        /// <returns>Deck cards</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Card card in Cards)
            {
                stringBuilder.AppendFormat("{0}, ", card.ToStringShort);
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Peek method for deck
        /// </summary>
        /// <returns>Show next card</returns>
        public Card Peek() => Cards.Peek();
        /// <summary>
        /// Pop method for deck
        /// </summary>
        /// <returns>Pop next card</returns>
        public Card Pop() => Cards.Pop();
        /// <summary>
        /// Push method for deck
        /// </summary>
        /// <param name="card">Card to push</param>
        public void Push(Card card) => Cards.Push(card);
        #endregion
    }
}
