using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//pedir con 16, plantarse con 17

namespace GameCardLib
{
    public class Croupier : Player
    {
        #region fields
        private Deck myDeck;
        private Stack<Card> discarded;
        private List<Player> players;
        private int currentPlayer;
        #endregion
        #region Properties
        private Deck MyDeck { get => myDeck; set => myDeck = value; }
        public string DeckString => MyDeck.ToString();
        public Stack<Card> Discarded { get => discarded; private set => discarded = value; }
        public string DiscardedString
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Card card in Discarded)
                {
                    stringBuilder.AppendFormat("{0}, ", card.ToStringShort);
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                return stringBuilder.ToString();
            }
        }
        private List<Player> Players { get => players; set => players = value; }
        private int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        #endregion
        #region Methods()
        #region Constructors
        public Croupier() : this(1)
        {
        }

        public Croupier(List<string> playerList)
        {
            Players = new List<Player>();
            foreach (string name in playerList)
            {
                Players.Add(new Player(name));
            }
            currentPlayer = 0;
        }
        public Croupier(int numberOfPlayers)
        {
            Players = new List<Player>(numberOfPlayers);
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player());
            }
            CurrentPlayer = 0;
        }
        #endregion
        public void StartGame(int numberOfDecks = 1)
        {
            MyDeck = new Deck(numberOfDecks);
            Discarded = new Stack<Card>();
            foreach (Player player in Players)
            {
                GiveCard(Players.IndexOf(player), 2);
            }
            Hand.AddCard(MyDeck.Pop());
        }

        public void ContinueGame()
        {
            CurrentPlayer = 0;
            foreach (Player player in Players)
            {
                foreach (Card card in player.Hand)
                {
                    Discarded.Push(card);
                }
                player.Hand.Clear();
                GiveCard(Players.IndexOf(player));
            }
            foreach (Card card in Hand)
            {
                Discarded.Push(card);
            }
            Hand.Clear();
            Hand.AddCard(MyDeck.Pop());
        }

        public Player GetPlayer()
        {
            return GetPlayer(CurrentPlayer);
        }
        private Player GetPlayer(int player)
        {
            return Players[player];
        }
        public Player NextPlayer()
        {
            if(CurrentPlayer == Players.Count - 1)
            {
                CroupierPicks();
                //endgame
            }
            else { CurrentPlayer++; }
            return GetPlayer();
        }

        private void CroupierPicks()
        {
            //Pedir con 16, plantarse con 17
            while (true)
            {
                if (Hand.Score < 17) { Hand.AddCard(MyDeck.Pop()); }
                else { break; }
            }
            ScoreCheck();
        }

        private void ScoreCheck()
        {
            List<Player> winners = (from player in Players where player.Hand.Score <=21 select player).ToList();
            if(Hand.Score <= 21) { winners.Add(this); }
            //throw new NotImplementedException();
        }

        private void GiveCard(int playerId, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (Players[playerId].Hand.Any(Card => Card.ToStringShort == MyDeck.Peek().ToStringShort) == false)
                {
                    Players[playerId].Hand.AddCard(MyDeck.Pop());
                }
                else
                {
                    Discarded.Push(MyDeck.Pop());
                    GiveCard(playerId);
                }
            }
        }

        public void GiveCard()
        {
            GiveCard(CurrentPlayer);
        }
        public void Reshuffle()
        {
            MyDeck.Shuffle();
        }
        #endregion
    }
}