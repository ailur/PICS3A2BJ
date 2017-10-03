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
        private string name;
        private Hand hand;
        #endregion
        #region Properties
        public string Name { get; set; }
        public Hand Hand { get; set; }
        #endregion
        #region Methods()
        #region Constructors
        public Player()
        {
            Hand = new Hand();
        }

        public Player(string name) : this()
        {
            Name = name;
        }
        #endregion
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
