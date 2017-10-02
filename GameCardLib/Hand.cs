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
        public int NumberOfCards => cards.Count;
        public int Score => cards.Aggregate(0, (current, card) => current + card.Value);

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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Score {0}: ", Score);
            foreach (Card card in cards)
            {
                sb.AppendFormat("{0}, ", card);
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
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