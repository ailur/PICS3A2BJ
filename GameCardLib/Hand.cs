using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Hand class
    /// </summary>
    public class Hand : IEnumerable<Card>
    {
        #region fields
        private List<Card> cards;
        #endregion
        #region Properties
        /// <summary>
        /// Cards of the Hand
        /// </summary>
        private List<Card> Cards { get => cards; set => cards = value; }
        /// <summary>
        /// Number of cards the hand have.
        /// </summary>
        public int NumberOfCards => Cards.Count;
        /// <summary>
        /// Score of the hand.
        /// </summary>
        public int Score => Cards.Sum(card => card.Value);
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Hand() => Cards = new List<Card>();
        #endregion
        /// <summary>
        /// Add a card to the hand.
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(Card card) => Cards.Add(card);

        /// <summary>
        /// Clear the hand.
        /// </summary>
        public void Clear() => Cards.Clear();

        /// <summary>
        /// Hand ToString method.
        /// </summary>
        /// <returns>Total Score and card collection.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Score {0}: ", Score);
            foreach (Card card in Cards)
            {
                sb.AppendFormat("{0}, ", card);
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        #region Interface implementation
        /// <summary>
        /// Interface implementation for IEnumerable
        /// </summary>
        public IEnumerator<Card> GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
        }

        /// <summary>
        /// Interface implementation for IEnumerable
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Card>)Cards).GetEnumerator();
        }
        #endregion
        #endregion
    }
}