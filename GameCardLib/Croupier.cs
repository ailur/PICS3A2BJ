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
        public Deck Deck
        {
            get { return deck; }
        }
        public List<Player> Players
        {
            //Esto esta muy feo
            get { return players; }
        }
        public Croupier()
        {
            players = new List<Player>();
            players.Add(new Player());
        }
        public Croupier(int numberOfPlayers)
        {
            players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player());
            }
        }
        public void GiveCard(int playerId)
        {
            players[playerId].Hand.AddCard(deck.DrawNextCard());
        }
        public void Reshuffle()
        {
            deck.Shuffle();
        }
        public void StartGame()
        {
            deck = new Deck();
            for (int playerId=0; playerId < players.Count; playerId++)
            {
                GiveCard(playerId);
                GiveCard(playerId);
            }
            this.Hand.AddCard(deck.DrawNextCard());
        }
    }
}