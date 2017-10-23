using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Card class
    /// </summary>
    public class Card
    {
        #region fields
        private EnumValue cardValue;
        private EnumSuite suite;
        private readonly Dictionary<EnumSuite, string> suiteDict = new Dictionary<EnumSuite, string>
        {
            { EnumSuite.Clubs, "c" },
            { EnumSuite.Diamonds, "d" },
            { EnumSuite.Hearts, "h" },
            { EnumSuite.Spades, "s" }
        };
        private readonly Dictionary<EnumValue, string> valueDict = new Dictionary<EnumValue, string>
        {
            { EnumValue.Ace, "1"},
            { EnumValue.Two, "2"},
            { EnumValue.Three, "3"},
            { EnumValue.Four, "4"},
            { EnumValue.Five, "5"},
            { EnumValue.Six, "6"},
            { EnumValue.Seven, "7"},
            { EnumValue.Eight, "8"},
            { EnumValue.Nine, "9"},
            { EnumValue.Ten, "10"},
            { EnumValue.Jack, "j"},
            { EnumValue.Queen, "q"},
            { EnumValue.King, "k"}
        };
        #endregion
        #region Properties

        public int Id { get; set; }
        /// <summary>
        /// Value in int.
        /// </summary>
        public int CardValue
        {
            get
            {
                int cardValue = (int) ValueEnum;
                if (cardValue <= 8)
                    return cardValue + 1;
                else
                    return 10;
            }
        }
        /// <summary>
        /// Value in enum.
        /// </summary>
        private EnumValue ValueEnum { get => cardValue; set => cardValue = value; }
        /// <summary>
        /// Suite of the card.
        /// </summary>
        private EnumSuite Suite { get => suite; set => suite = value; }
        /// <summary>
        /// Short name.
        /// </summary>
        public string ToStringShort => suiteDict[Suite] + valueDict[ValueEnum];
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Card() : this(EnumValue.Ace, EnumSuite.Spades)
        {
        }

        /// <summary>
        /// Constructor that takes 2 arguments, Value and Suite.
        /// </summary>
        /// <param name="value">Value of the card.</param>
        /// <param name="suite">Suite of the card.</param>
        public Card(EnumValue value, EnumSuite suite)
        {
            if (Enum.IsDefined(typeof(EnumValue), ValueEnum) && Enum.IsDefined(typeof(EnumSuite), suite))
            {
                ValueEnum = value;
                Suite = suite;
            }
        }
        #endregion
        /// <summary>
        /// Card ToString method.
        /// </summary>
        /// <returns>Description of the card.</returns>
        public override string ToString() => ValueEnum + " Of " + Suite;
        #endregion
    }
}