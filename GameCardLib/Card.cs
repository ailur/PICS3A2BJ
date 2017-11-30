using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
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
        [Key]
        public int CardId { get; set; }
        /// <summary>
        /// Value in int.
        /// </summary>
        public int CardScore
        {
            get
            {
                int cardValue = (int) Value;
                if (cardValue <= 8)
                    return cardValue + 1;
                return 10;
            }
            private set
            {  
            }
        }
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; private set; }
        /// <summary>
        /// Value in enum.
        /// </summary>
        public EnumValue Value { get => cardValue; private set => cardValue = value; }
        /// <summary>
        /// Suite of the card.
        /// </summary>
        public EnumSuite Suite { get => suite; private set => suite = value; }
        /// <summary>
        /// Short name.
        /// </summary>
        public string ToStringShort => suiteDict[Suite] + valueDict[Value];
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Constructor that takes 2 arguments, Value and Suite.
        /// </summary>
        /// <param name="value">Value of the card.</param>
        /// <param name="suite">Suite of the card.</param>
        public Card(EnumValue value, EnumSuite suite)
        {
            if (Enum.IsDefined(typeof(EnumValue), Value) && Enum.IsDefined(typeof(EnumSuite), suite))
            {
                Value = value;
                Suite = suite;
            }
        }
        #endregion
        /// <summary>
        /// Card ToString method.
        /// </summary>
        /// <returns>Description of the card.</returns>
        public override string ToString() => Value + " Of " + Suite;
        #endregion
    }
}