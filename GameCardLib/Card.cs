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
        #endregion
        #region Properties
        public int Value { get; private set; }
        public EnumSuite Suite { get; private set; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Card() : this(1,EnumSuite.Diamonds)
        {
        }
        public Card(int value, EnumSuite suite)
        {
            if (Enum.IsDefined(typeof(EnumValue), (EnumValue)value) && Enum.IsDefined(typeof(EnumSuite), suite))
            {
                this.Value = value;
                this.Suite = suite;
            }
        }
        #endregion
        public override string ToString()
        {
            return Enum.Parse(typeof(EnumValue),Value.ToString()) + " Of " + Suite.ToString();
        }
        #endregion
    }
}
