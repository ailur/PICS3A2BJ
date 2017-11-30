using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLib
{
    public class Card
    {
        private EnumValue cardValue;
        private EnumSuite suite;

        private readonly Dictionary<EnumSuite, string> suiteDict = new Dictionary<EnumSuite, string>
        {
            {EnumSuite.Clubs, "c"},
            {EnumSuite.Diamonds, "d"},
            {EnumSuite.Hearts, "h"},
            {EnumSuite.Spades, "s"}
        };

        private readonly Dictionary<EnumValue, string> valueDict = new Dictionary<EnumValue, string>
        {
            {EnumValue.Ace, "1"},
            {EnumValue.Two, "2"},
            {EnumValue.Three, "3"},
            {EnumValue.Four, "4"},
            {EnumValue.Five, "5"},
            {EnumValue.Six, "6"},
            {EnumValue.Seven, "7"},
            {EnumValue.Eight, "8"},
            {EnumValue.Nine, "9"},
            {EnumValue.Ten, "10"},
            {EnumValue.Jack, "j"},
            {EnumValue.Queen, "q"},
            {EnumValue.King, "k"}
        };

        [Key]
        public int CardId { get; set; }

        /// <summary>
        /// Value in int.
        /// </summary>
        public int CardScore
        {
            get
            {
                int cardValue = (int)Value;
                if (cardValue <= 8)
                    return cardValue + 1;
                return 10;
            }
            private set { }
        }

        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; private set; }

        /// <summary>
        /// Value in enum.
        /// </summary>
        public EnumValue Value
        {
            get => cardValue;
            private set => cardValue = value;
        }

        /// <summary>
        /// Suite of the card.
        /// </summary>
        public EnumSuite Suite
        {
            get => suite;
            private set => suite = value;
        }

        /// <summary>
        /// Short name.
        /// </summary>
        public string ToStringShort => suiteDict[Suite] + valueDict[Value];
    }

    public class Player
    {
        private string name;
        private List<Card> hand;

        public int PlayerId { get; set; }

        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name
        {
            get => name;
            private set => name = value;
        }

        /// <summary>
        /// Hand of the player
        /// </summary>
        public virtual List<Card> Hand
        {
            get => hand;
            private set => hand = value;
        }

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

        public bool IsCroupier { get; private set; }
    }

    public class Deck : Stack<Card>
    {
        private Stack<Card> cards;
        private int deckMultiplier;

        public int DeckId { get; set; }

        /// <summary>
        /// Stack of cards
        /// </summary>
        public Stack<Card> Cards
        {
            get => cards;
            set => cards = value;
        }

        /// <summary>
        /// How many decks compose the main deck
        /// </summary>
        private int DeckMultiplier
        {
            get => deckMultiplier;
            set => deckMultiplier = value;
        }

        /// <summary>
        /// Count of cards in the deck
        /// </summary>
        public new int Count
        {
            get => Cards.Count;
        }

    }
    public class Game
    {
        public DateTime DateStarted { get; set; }
        public int GameId { get; set; }
        public List<Player> players;

        private List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        private Deck myDeck;

        private Deck MyDeck
        {
            get { return myDeck; }
            set { myDeck = value; }
        }

        private Deck discarded;

        private Deck Discarded
        {
            get { return discarded; }
            set { discarded = value; }
        }

        private int currentPlayer;

        /// <summary>
        /// Current player
        /// </summary>
        private int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }


        /// <summary>
        /// String describing deck reimaining cards
        /// </summary>
        public string DeckString => MyDeck.ToString();

        /// <summary>
        /// Number of cards in discarded stack
        /// </summary>
        public int DiscardedCount => Discarded.Count;

        /// <summary>
        /// Get last card discarded
        /// </summary>
        public Card LastCardDiscarded => Discarded.Peek();

        /// <summary>
        /// String describing discarded cards
        /// </summary>
        public string DiscardedString => Discarded.ToString();
    }
}