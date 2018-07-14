using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UtilitiesLib;
using EnumValue = UtilitiesLib.Names.EnumValue;
using EnumSuite = UtilitiesLib.Names.EnumSuite;

namespace GameCardLib
{
    /// <summary>
    /// Card class
    /// </summary>
    public class Card
    {
        #region fields
        /// <summary>
        /// Card value
        /// </summary>
        private EnumValue cardValue;
        /// <summary>
        /// Suit value
        /// </summary>
        private EnumSuite suite;
        #endregion
        #region Properties
        /// <summary>
        /// Card Database Id
        /// </summary>
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
        /// <summary>
        /// ID of the deck the card is in
        /// </summary>
        [ForeignKey("Deck")]
        public int DeckId { get; set; }
        /// <summary>
        /// Dech the card is in
        /// </summary>
        public virtual Deck Deck { get; set; }
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
        public string ToStringShort => CardDictionaries.suiteDict[Suite] + CardDictionaries.valueDict[Value];
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