using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GameCardLib
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player
    {
        #region fields
        private string name;
        private List<Card> hand;
        #endregion
        #region Properties

        /// <summary>
        /// The ID of this player object
        /// </summary>
        public int PlayerId { get; set; }
        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get => name; private set => name = value; }
        /// <summary>
        /// Hand of the player
        /// </summary>
        public virtual List<Card> Hand { get => hand; private set => hand = value; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; private set; }
        /// <summary>
        /// Number of cards the hand have.
        /// </summary>
        public int NumberOfCards => Hand.Count;
        /// <summary>
        /// Score of the hand.
        /// </summary>
        public int Score => Hand.Sum(card => card.CardScore);
        /// <summary>
        /// Wether this player is croupier or not
        /// </summary>
        public bool IsCroupier { get; private set; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        private Player() => Hand = new List<Card>();

        /// <summary>
        /// Constructor that takes one argument, name.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name ="", bool isCroupier = true) : this()
        {
            if (!string.IsNullOrWhiteSpace(name))
                isCroupier = false;
            Name = isCroupier ? "Croupier" : name;
            IsCroupier = isCroupier;
        }

        #endregion
        /// <summary>
        /// Add a card to the hand.
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(Card card)
        {
            Hand.Add(card);
        }
        /// <summary>
        /// Clear the hand.
        /// </summary>
        public void Clear() => Hand.Clear();
        /// <summary>
        /// Hand ToString method.
        /// </summary>
        /// <returns>Total Score and card collection.</returns>
        public string HandToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Score {0}: ", Score);
            foreach (Card card in Hand)
            {
                sb.AppendFormat("{0}, ", card);
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
        /// <summary>
        /// Player ToString method.
        /// </summary>
        /// <returns>Name, score and card collection.</returns>
        public override string ToString() => Name + ": " + HandToString();
        #endregion
    }
}