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
        private Stack<Card> Discarded { get => discarded; set => discarded = value; }
        public string DeckString => MyDeck.ToString();
        private List<Player> Players { get => players; set => players = value; }
        private int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        #endregion
        #region Methods()
        #region Constructors
        public Croupier() : this(1)
        {
        }
        public Croupier(int numberOfPlayers)
        {
            Players = new List<Player>();
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
            for (int playerId = 0; playerId < Players.Count; playerId++)
            {
                GiveCard(playerId);
                GiveCard(playerId);
            }
            Hand.AddCard(MyDeck.DrawNextCard());
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
            }
            else { CurrentPlayer++; }
            return GetPlayer();
        }

        private void CroupierPicks()
        {
            //Pedir con 16, plantarse con 17
            while (true)
            {
                if (Hand.Score < 17) { Hand.AddCard(MyDeck.DrawNextCard()); }
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

        private void GiveCard(int playerId)
        {
            if(Players[playerId].Hand.Where(Card => Card.ToStringShort == MyDeck.Peek().ToStringShort).Any() == false)
            {
                Players[playerId].Hand.AddCard(MyDeck.DrawNextCard());
            }
            else
            {
                Discarded.Push(MyDeck.Pop());
                GiveCard(playerId);
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