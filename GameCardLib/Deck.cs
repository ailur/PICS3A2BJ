using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    class Deck
    {
        #region fields
        private Stack<Card> cards;
        private int deckMultiplier;
        #endregion
        #region Properties
        public int Count
        {
            get { return cards.Count; }
        }
        #endregion
        #region Methods()
        #region Constructors
        public Deck()
        {
        }
        public Deck(int deckMultiplier)
        {
            this.deckMultiplier = deckMultiplier;
            Stack<Card> cards = new Stack<Card>();
            FillDeckWithCards();
            Shuffle();
        }
        #endregion
        public bool DiscardCards(int quantity)
        {
            for (int i = quantity; i > 0; i--)
            {
                cards.Pop();
            }
            return true;
        }
        public Card DrawNextCard()
        {
            return cards.Pop();
        }

        private bool FillDeckWithCards()
        {
            foreach (int deck in Enumerable.Range(1,deckMultiplier))
            {
                foreach (EnumSuite suite in Enum.GetValues(typeof(EnumSuite)))
                {
                    foreach (int value in Enum.GetValues(typeof(EnumValue)))
                    {
                        cards.Push(new Card(value, suite));
                    }
                }
            }
            return true;
        }
        //utilities?
        public Stack<Card> Shuffle()
        {
            Random rnd = new Random();
            return new Stack<Card>(cards.OrderBy(x => rnd.Next()));
        }
        #endregion
    }
}
