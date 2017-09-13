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
        }
        #endregion
        #endregion
    }
}
