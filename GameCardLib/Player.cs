using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player
    {
        #region fields
        private string name;
        private Hand hand;
        #endregion
        #region Properties
        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get => name; private set => name = value; }
        /// <summary>
        /// Hand of the player
        /// </summary>
        public Hand Hand { get => hand; private set => hand = value; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Player() => Hand = new Hand();

        /// <summary>
        /// Constructor that takes one argument, name.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name) : this() => Name = name;
        #endregion

        /// <summary>
        /// Player ToString method.
        /// </summary>
        /// <returns>Name, score and card collection.</returns>
        public override string ToString() => Name + Hand;
        #endregion
    }
}