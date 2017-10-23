using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GameCardLib
{
    /// <summary>
    /// Hand class
    /// </summary>
    public class Hand : IEnumerable<Card>
    {
        #region fields
        [Key]
        public int IdHand { get; set; }
        private List<Card> cards;

        public int IdPlayer { get; set; }
        [Required, ForeignKey("IdPlayer")]
        public virtual Player Player { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// Cards of the Hand
        /// </summary>
        public List<Card> Cards { get => cards; set => cards = value; }
        /// <summary>
        /// Number of cards the hand have.
        /// </summary>
        public int NumberOfCards => Cards.Count;
        /// <summary>
        /// Score of the hand.
        /// </summary>
        public int Score => Cards.Sum(card => card.CardValue);
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