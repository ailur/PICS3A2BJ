using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Croupier
    {
        private Deck deck;
        private List<Player> players;

        public void GiveCard(int playerId)
        {
            players[playerId].Hand.AddCard(deck.DrawNextCard());
        }
    }
}