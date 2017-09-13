using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Game
    {
        private Deck deck;
        private List<Player> players;

        public void GiveCard(Player player)
        {
            player.Hand.AddCard(deck.DrawNextCard());
        }
    }
}
