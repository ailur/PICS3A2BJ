using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Card
    {
        #region fields
        private int value;
        private EnumSuite suite;
        private Dictionary<EnumSuite, string> suiteDict = new Dictionary<EnumSuite, string>
        {
            { EnumSuite.Clubs, "c" },
            { EnumSuite.Diamonds, "d" },
            { EnumSuite.Hearts, "h" },
            { EnumSuite.Spades, "s" }
        };
        private Dictionary<EnumValue, string> valueDict = new Dictionary<EnumValue, string>
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
        public int Value {
            get { return value + 1; }
            private set { this.value = value; }
        }
        public EnumValue ValueEnum
        {
            get { return (EnumValue)value; }
        }
        public EnumSuite Suite { get; private set; }
        public string ToStringShort
        {
            get
            {
                return suiteDict[this.Suite] + valueDict[this.ValueEnum];
            }
        }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Card() : this(1, EnumSuite.Spades)
        {
        }
        public Card(int value, EnumSuite suite)
        {
            if (Enum.IsDefined(typeof(EnumValue), (EnumValue)value) && Enum.IsDefined(typeof(EnumSuite), suite))
            {
                this.value = value;
                this.Suite = suite;
            }
        }
        #endregion
        public override string ToString()
        {
            return ValueEnum.ToString() + " Of " + Suite.ToString();
        }
        #endregion
    }
}
