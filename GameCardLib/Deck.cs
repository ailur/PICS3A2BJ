using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Deck
    {
        #region fields
        private Stack<Card> cards;
        private int deckMultiplier;
        #endregion
        #region Properties
        public int Count
        {
            get { return Cards.Count; }
        }

        private Stack<Card> Cards { get => cards; set => cards = value; }
        private int DeckMultiplier { get => deckMultiplier; set => deckMultiplier = value; }
        #endregion
        #region Methods()
        #region Constructors
        public Deck() :this(1)
        {
        }
        public Deck(int deckMultiplier)
        {
            DeckMultiplier = deckMultiplier;
            Cards = new Stack<Card>();
            FillDeckWithCards();
            Shuffle();
        }
        #endregion
        public bool DiscardCards(int quantity)
        {
            for (int i = quantity; i > 0; i--)
            {
                Cards.Pop();
            }
            return true;
        }

        private void FillDeckWithCards()
        {
            foreach (int deck in Enumerable.Range(1,DeckMultiplier))
            {
                foreach (EnumSuite suite in Enum.GetValues(typeof(EnumSuite)))
                {
                    foreach (int value in Enum.GetValues(typeof(EnumValue)))
                    {
                        Card card = new Card(value, suite);
                        Cards.Push(card);
                    }
                }
            }
        }
        //utilities?
        public void Shuffle()
        {
            Random rnd = new Random();
            Cards = new Stack<Card>(Cards.OrderBy(x => rnd.Next()));
        }
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

        public Card Peek() => Cards.Peek();
        public Card Pop() => Cards.Pop();
        #endregion
    }
}
