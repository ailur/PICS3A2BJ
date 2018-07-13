using System.Collections.Generic;

namespace UtilitiesLib
{
    /// <summary>
    /// Db sets to show on combobox
    /// </summary>
    public enum cmbDbSets
    {
        Games,
        Cards,
        Decks,
        Players
    }
    /// <summary>
    /// Card suites
    /// </summary>
    public enum EnumSuite
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    /// <summary>
    /// Card values (figures)
    /// </summary>
    public enum EnumValue
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public class CardDictionaries
    {
        /// <summary>
        /// Dictionary for short names of suites
        /// </summary>
        public static readonly Dictionary<EnumSuite, string> suiteDict = new Dictionary<EnumSuite, string>
        {
            { EnumSuite.Clubs, "c" },
            { EnumSuite.Diamonds, "d" },
            { EnumSuite.Hearts, "h" },
            { EnumSuite.Spades, "s" }
        };

        /// <summary>
        /// Dictionary for short names of values
        /// </summary>
        public static readonly Dictionary<EnumValue, string> valueDict = new Dictionary<EnumValue, string>
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
    }
}