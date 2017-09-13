using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    class Hand
    {
        #region fields
        private List<Card> cards;
        #endregion
        #region Properties
        public int NumberOfCards {
            get
            {
                return cards.Count;
            }
        }
        public int Score
        {
            get
            {
                int sum = 0;
                foreach (Card card in cards)
                {
                    sum = sum + card.Value;
                }
                return sum;
            }
        }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Hand()
        {

        }
        #endregion
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        #endregion
    }
}