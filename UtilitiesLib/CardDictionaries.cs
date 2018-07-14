using System.Collections.Generic;

namespace UtilitiesLib
{
    public static class CardDictionaries
    {
        /// <summary>
        /// Dictionary for short names of suites
        /// </summary>
        public static readonly Dictionary<Names.EnumSuite, string> suiteDict = new Dictionary<Names.EnumSuite, string>
        {
            { Names.EnumSuite.Clubs, "c" },
            { Names.EnumSuite.Diamonds, "d" },
            { Names.EnumSuite.Hearts, "h" },
            { Names.EnumSuite.Spades, "s" }
        };

        /// <summary>
        /// Dictionary for short names of values
        /// </summary>
        public static readonly Dictionary<Names.EnumValue, string> valueDict = new Dictionary<Names.EnumValue, string>
        {
            {Names.EnumValue.Ace, "1"},
            {Names.EnumValue.Two, "2"},
            {Names.EnumValue.Three, "3"},
            {Names.EnumValue.Four, "4"},
            {Names.EnumValue.Five, "5"},
            {Names.EnumValue.Six, "6"},
            {Names.EnumValue.Seven, "7"},
            {Names.EnumValue.Eight, "8"},
            {Names.EnumValue.Nine, "9"},
            {Names.EnumValue.Ten, "10"},
            {Names.EnumValue.Jack, "j"},
            {Names.EnumValue.Queen, "q"},
            {Names.EnumValue.King, "k"}
        };
    }
}