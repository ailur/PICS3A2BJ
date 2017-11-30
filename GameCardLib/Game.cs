using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCardLib
{
    public class Game
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new BJDBContext());
        public DateTime DateStarted { get; set; }
        public int GameId { get; set; }
        public List<Player> players;
        private List<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
            }
        }
        private Deck myDeck;
        private Deck MyDeck
        {
            get
            {
                return myDeck;
            }
            set
            {
                myDeck = value;
            }
        }
        private Deck discarded;
        private Deck Discarded
        {
            get
            {
                return discarded;
            }
            set
            {
                discarded = value;
            }
        }
        private int currentPlayer;
        /// <summary>
        /// Current player
        /// </summary>
        private int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                currentPlayer = value;
            }
        }


        /// <summary>
        /// String describing deck reimaining cards
        /// </summary>
        public string DeckString => MyDeck.ToString();
        /// <summary>
        /// Number of cards in discarded stack
        /// </summary>
        public int DiscardedCount => Discarded.Count;
        /// <summary>
        /// Get last card discarded
        /// </summary>
        public Card LastCardDiscarded => Discarded.Peek();
        /// <summary>
        /// String describing discarded cards
        /// </summary>
        public string DiscardedString => Discarded.ToString();

        /// <summary>
        /// Constructor that takes a list of player names
        /// </summary>
        /// <param name="playerList">List of player names</param>
        public Game(List<string> playerList)
        {
            Players = new List<Player> {new Player()};
            foreach (string name in playerList)
            {
                Players.Add(new Player(name));
            }
            Initialize();
        }

        /// <summary>
        /// Constructor that takes number of players
        /// </summary>
        /// <param name="numberOfPlayers">The number of players that is going to play</param>
        public Game(int numberOfPlayers)
        {
            Players = new List<Player>(numberOfPlayers) {new Player()};
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player(i.ToString(),false));
            }
            Initialize();
        }

        private void Initialize()
        {
            DateStarted = DateTime.Now;
            unitOfWork.Players.AddRange(Players);
            CurrentPlayer = 0;
            unitOfWork.Games.Add(this);
            unitOfWork.Complete();
        }

        /// <summary>
        /// Start the game: instantiate deck and discarded deck and give cards to players and croupier.
        /// </summary>
        /// <param name="numberOfDecks">Number of decks that compose the deck</param>
        public void StartGame(int numberOfDecks = 1)
        {
            MyDeck = new Deck(numberOfDecks);
            unitOfWork.Decks.Add(MyDeck);
            Discarded = new Deck(0);
            unitOfWork.Decks.Add(Discarded);
            unitOfWork.Games.Update(this);
            unitOfWork.Complete();
            foreach (Card card in MyDeck.Cards)
            {
                card.GameId = this.GameId;
            }
            unitOfWork.Cards.AddRange(MyDeck.Cards);
            unitOfWork.Complete();
            foreach (Player player in Players)
            {
                GiveCard(Players.IndexOf(player), player.IsCroupier?1:2);
            }
        }

        /// <summary>
        /// Give a number of cards to a player
        /// </summary>
        /// <param name="playerId">Player that receives cards</param>
        /// <param name="count">Number of cards the player receives</param>
        private void GiveCard(int playerId, int count = 1)
        {
            //TODO: Comprobar si quedan suficientes cartas
            Player player = Players[playerId];
            for (int i = 0; i < count; i++)
            {
                if (player.Hand.Any(card => card.ToStringShort == MyDeck.Peek().ToStringShort) == false)
                {
                    Card card = myDeck.Pop();
                    player.AddCard(card);
                    unitOfWork.Cards.Update(card);
                }
                else
                {
                    Card card = myDeck.Pop();
                    Discarded.Push(card);
                    GiveCard(playerId);
                    unitOfWork.Cards.Update(card);
                }
            }
            unitOfWork.Complete();
        }

        /// <summary>
        /// Give 1 card to current player
        /// </summary>
        public void GiveCard()
        {
            GiveCard(CurrentPlayer);
        }

        /// <summary>
        /// Get the current player
        /// </summary>
        /// <returns>Current player</returns>
        public Player GetPlayer()
        {
            return GetPlayer(CurrentPlayer);
        }

        public Player GetCroupier()
        {
            return Players.First(p => p.IsCroupier);
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
            if (CurrentPlayer == Players.Count - 1)
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
            Player croupier = GetCroupier();
            while (croupier.Score < 17)
            {
                croupier.AddCard(MyDeck.Pop());
            }
            ScoreCheck();
        }


        /// <summary>
        /// Make a list of winners
        /// </summary>
        private void ScoreCheck()
        {
            List<Player> playersNotOver21 = (from player in Players where player.Score <= 21 select player).ToList();
            Player croupier = GetCroupier();
            if (croupier.Score <= 21) { playersNotOver21.Add(croupier); }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Reshuffle the deck
        /// </summary>
        public void Reshuffle()
        {
            MyDeck.Shuffle();
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
                if (player.IsCroupier)
                    continue;
                foreach (Card card in player.Hand)
                {
                    Discarded.Push(card);
                }
                player.Clear();
                GiveCard(Players.IndexOf(player), 2);
            }
            Player croupier = GetCroupier();
            foreach (Card card in croupier.Hand)
            {
                Discarded.Push(card);
            }
            croupier.Clear();
            croupier.AddCard(MyDeck.Pop());
        }
    }
}
