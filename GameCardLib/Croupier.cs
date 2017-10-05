using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//pedir con 16, plantarse con 17

namespace GameCardLib
{
    /// <summary>
    /// Croupier class
    /// </summary>
    public class Croupier : Player
    {
        #region fields
        private Deck myDeck;
        private Deck discarded;
        private List<Player> players;
        private int currentPlayer;
        #endregion
        #region Properties
        private Deck MyDeck { get => myDeck; set => myDeck = value; }
        public string DeckString => MyDeck.ToString();
        public Deck Discarded { get => discarded; private set => discarded = value; }
        public string DiscardedString => Discarded.ToString();
        private List<Player> Players { get => players; set => players = value; }
        private int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Croupier() : this(1)
        {
        }

        /// <summary>
        /// Constructor that takes a list of player names
        /// </summary>
        /// <param name="playerList">List of player names</param>
        public Croupier(List<string> playerList)
        {
            Players = new List<Player>();
            foreach (string name in playerList)
            {
                Players.Add(new Player(name));
            }
            currentPlayer = 0;
        }

        /// <summary>
        /// Constructor that takes number of players
        /// </summary>
        /// <param name="numberOfPlayers">The number of players that is going to play</param>
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
        /// <summary>
        /// Start the game: instantiate deck and discarded deck and give cards to players and croupier.
        /// </summary>
        /// <param name="numberOfDecks">Number of decks that compose the deck</param>
        public void StartGame(int numberOfDecks = 1)
        {
            MyDeck = new Deck(numberOfDecks);
            Discarded = new Deck(0);
            foreach (Player player in Players)
            {
                GiveCard(Players.IndexOf(player), 2);
            }
            Hand.AddCard(MyDeck.Pop());
        }

        /// <summary>
        /// Start a new round
        /// </summary>
        public void ContinueGame()
        {
            //TODO: Check if there are enough cards left in deck
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

        /// <summary>
        /// Get the current player
        /// </summary>
        /// <returns>Current player</returns>
        public Player GetPlayer()
        {
            return GetPlayer(CurrentPlayer);
        }
        /// <summary>
        /// Get a player
        /// </summary>
        /// <param name="player">Player to get</param>
        /// <returns>Player</returns>
        private Player GetPlayer(int player)
        {
            return Players[player];
        }

        /// <summary>
        /// Change current player to next player.
        /// If player is last player, finish game.
        /// </summary>
        /// <returns>Next player</returns>
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

        /// <summary>
        /// Croupier picks cards acording to rule
        /// </summary>
        private void CroupierPicks()
        {
            while (Hand.Score < 17)
            {
                Hand.AddCard(MyDeck.Pop());
            }
            ScoreCheck();
        }

        /// <summary>
        /// Make a list of winners
        /// </summary>
        private void ScoreCheck()
        {
            List<Player> playersNotOver21 = (from player in Players where player.Hand.Score <=21 select player).ToList();
            if(Hand.Score <= 21) { playersNotOver21.Add(this); }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Give a number of cards to a player
        /// </summary>
        /// <param name="playerId">Player that receives cards</param>
        /// <param name="count">Number of cards the player receives</param>
        private void GiveCard(int playerId, int count = 1)
        {
            //TODO: Comprobar si quedan suficientes cartas
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

        /// <summary>
        /// Give 1 card to current player
        /// </summary>
        public void GiveCard()
        {
            GiveCard(CurrentPlayer);
        }

        /// <summary>
        /// Reshuffle the deck
        /// </summary>
        public void Reshuffle()
        {
            MyDeck.Shuffle();
        }
        #endregion
    }
}