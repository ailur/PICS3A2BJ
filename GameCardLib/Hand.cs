using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Hand : IEnumerable<Card>
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
            cards = new List<Card>();
        }
        #endregion
        //Cambiar a bool
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void Clear()
        {
            cards.Clear();
        }
        public override string ToString()
        {
            return base.ToString();
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return ((IEnumerable<Card>)cards).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Card>)cards).GetEnumerator();
        }


        #endregion
    }
}