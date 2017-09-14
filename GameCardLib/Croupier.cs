using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//pedir con 16, plantarse con 17

namespace GameCardLib
{
    public class Croupier:Player
    {
        private Deck deck;
        private List<Player> players;

        public void GiveCard(int playerId)
        {
            players[playerId].Hand.AddCard(deck.DrawNextCard());
        }
    }
}