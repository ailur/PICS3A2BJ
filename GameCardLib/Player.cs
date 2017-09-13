using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Player
    {
        #region fields
        private Hand hand;
        private string name;
        #endregion
        #region Properties
        public Hand Hand { get; set; }
        #endregion
        #region Methods()
        #region Constructors
        public Player()
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion
        #endregion
    }
}
