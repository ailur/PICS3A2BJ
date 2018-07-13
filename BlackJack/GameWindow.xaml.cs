using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DAL;
using GameCardLib;
using cmbDbSets = UtilitiesLib.cmbDbSets;

namespace BlackJack
{
    /// <summary>
    /// Lógica de interacción para GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        #region fields
        /// <summary>
        /// Wether the game is started or not
        /// </summary>
        private bool gameStarted;
        #endregion
        #region Properties
        /// <summary>
        /// DB Context
        /// </summary>
        private BJDBContext Context { get; set; }
        /// <summary>
        /// Unit of work
        /// </summary>
        private UnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Game instance
        /// </summary>
        private Game Game { get; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Constructor with 2 parameters and 1 optional parameter.
        /// </summary>
        /// <param name="numberOfPlayers">Number of players.</param>
        /// <param name="numberOfDecks">Number of decks.</param>
        /// <param name="playerList">(Optional)List with players' names.</param>
        public GameWindow(int numberOfPlayers, int numberOfDecks, List<string> playerList = null)
        {
            InitializeComponent();
            Initialize();
            gameStarted = false;
            CanDraw(false);
            Game = playerList == null ? new Game(numberOfPlayers, numberOfDecks) : new Game(playerList, numberOfDecks);
            UnitOfWork.Players.AddRange(Game.Players);
            UnitOfWork.Decks.Add(Game.MyDeck);
            UnitOfWork.Decks.Add(Game.Discarded);
            UnitOfWork.Games.Add(Game);
            Game.StartGame();
            UnitOfWork.Cards.AddRange(Game.MyDeck.Cards);
            UnitOfWork.Complete();
            UpdateCards(Game.GetCroupier());
            UpdateCards(Game.GetPlayer());
            UpdateDiscarded();
            CheckHand();
            Debug();
#if DEBUG
            txtDebug.Visibility = Visibility.Visible;
#else
            txtDebug.Visibility = Visibility.Collapsed;
#endif
            gameStarted = true;
            cmbDbSet.ItemsSource = Enum.GetValues(typeof(cmbDbSets));
            cmbDbSet.SelectedItem = cmbDbSets.Cards;
            DataBaseShow.ItemsSource = Context.Cards.Local.ToList();
        }
        #endregion
        /// <summary>
        /// Initializes context and unit of work
        /// </summary>
        private void Initialize()
        {
            Context = new BJDBContext();
            UnitOfWork = new UnitOfWork(Context);
        }

        /// <summary>
        /// If current player cannot draw anymore cards, disable drawing options and change deck opacity.
        /// </summary>
        /// <param name="can">Wether current player can draw more cards or not.</param>
        private void CanDraw(bool can)
        {
            btnDrawCard.IsEnabled = can;
            imgDeck.IsEnabled = can;
            imgDeck.Opacity = can ? 1 : 0.5;
        }

        /// <summary>
        /// Update the images of player hand.
        /// </summary>
        /// <param name="player">Player which cards will be drawn.</param>
        private void UpdateCards(Player player)
        {
            UIElementCollection elementCollection = player.IsCroupier ? CroupierDeck.Children : PlayerDeck.Children;
            elementCollection.Clear();
            foreach (Card card in player.Hand)
            {
                string src = "CardGUI/" + card.ToStringShort + ".png";
                Image img = new Image
                {
                    Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource,
                    Height = 96,
                    Width = 75,
                    ToolTip = card + "\n" + card.CardScore
                };
                elementCollection.Add(img);
            }
            UpdateScores(player);
        }

        /// <summary>
        /// Update images of discarded deck.
        /// </summary>
        private void UpdateDiscarded()
        {
            if (Game.DiscardedCount > 0)
            {
                imgDiscard.Source =
                    new ImageSourceConverter().ConvertFromString(
                        "CardGUI/" + Game.LastCardDiscarded.ToStringShort + ".png") as ImageSource;
            }
            else
            {
                imgDiscard.Source =
                    new ImageSourceConverter().ConvertFromString("CardGUI/jb.png") as ImageSource;
            }
        }

        /// <summary>
        /// Update scores to show.
        /// </summary>
        /// <param name="player">Player whose score will be read</param>
        private void UpdateScores(Player player)
        {
            TextBlock updatedTextBlock;
            if (player.IsCroupier)
            {
                updatedTextBlock = txtCroupierScore;
                updatedTextBlock.Text = "Croupier\nscore:\n" + player.Score;
            }
            else
            {
                txtPlayerName.Text = player.Name;
                updatedTextBlock = txtPlayerScore;
                updatedTextBlock.Text = "Your score:\n" + player.Score;
            }
            updatedTextBlock.Foreground = Brushes.Black;
            if (player.Score > 21) { updatedTextBlock.Foreground = Brushes.Red; }
            else if (player.Score == 21) { updatedTextBlock.Foreground = Brushes.Green; }
        }

        /// <summary>
        /// Check if hand scores if greater than 21, if that is the case player can't draw cards.
        /// </summary>
        private void CheckHand()
        {
            CanDraw(Game.GetPlayer().Score < 21);
        }

        /// <summary>
        /// DEBUG method. Shows cards in deck and dicarded deck.
        /// </summary>
        [Conditional("DEBUG")]
        private void Debug()
        {
            string txtDeck = "Deck: " + Game.DeckString.Replace("c", "♣").Replace("d", "♦").Replace("h", "♥").Replace("s", "♠").ToUpper();
            txtDebug.Text = txtDeck;
            if (Game.DiscardedCount == 0) return;
            string txtDiscarded = "Discarded: " + Game.DiscardedString.Replace("c", "♣").Replace("d", "♦").Replace("h", "♥").Replace("s", "♠").ToUpper();
            txtDebug.Text += "\n" + txtDiscarded;
        }

        #region events handlers
        /// <summary>
        /// Start next round, update all cards, discarded deck.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            Game.ContinueGame();
            foreach (var gamePlayer in Game.Players)
            {
                UnitOfWork.Players.Update(gamePlayer);
            }
            foreach (var discardedCard in Game.Discarded)
            {
                UnitOfWork.Cards.Update(discardedCard);
            }

            UnitOfWork.Complete();

            UpdateCards(Game.GetCroupier());
            UpdateCards(Game.GetPlayer());
            UpdateDiscarded();
            CheckHand();
            Debug();
            DataBaseShow.Items.Refresh();
        }

        /// <summary>
        /// Reshuffle the deck.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReshuffle_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
            {
                Game.Reshuffle();
                Debug();
                DataBaseShow.Items.Refresh();
            }
        }

        /// <summary>
        /// Give card to current player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
            {
                Game.GiveCard();
                foreach (var card in Game.GetPlayer().Hand)
                {
                    UnitOfWork.Cards.Update(card);
                }
                UnitOfWork.Complete();
                UpdateCards(Game.GetPlayer());
                UpdateDiscarded();
                CheckHand();
                Debug();
                DataBaseShow.Items.Refresh();
            }
        }

        /// <summary>
        /// Go to next player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
            {
                UpdateCards(Game.NextPlayer());
                foreach (var card in Game.GetPlayer().Hand)
                {
                    UnitOfWork.Cards.Update(card);
                }
                UnitOfWork.Complete();
                CheckHand();
                UpdateCards(Game.GetCroupier());
                UpdateDiscarded();
                DataBaseShow.Items.Refresh();
            }
        }

        /// <summary>
        /// Draw a card.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgDeck_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnDrawCard_Click(sender, e);
        }

        /// <summary>
        /// On combobox selection changed, change set of the database shower.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDbSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((cmbDbSets)cmbDbSet.SelectedItem)
            {
                case cmbDbSets.Cards:
                    DataBaseShow.ItemsSource = Context.Cards.Local.ToList();
                    break;
                case cmbDbSets.Decks:
                    DataBaseShow.ItemsSource = Context.Decks.Local.ToList();
                    break;
                case cmbDbSets.Games:
                    DataBaseShow.ItemsSource = Context.Games.Local.ToList();
                    break;
                case cmbDbSets.Players:
                    DataBaseShow.ItemsSource = Context.Players.Local.ToList();
                    break;
            }
        }
        #endregion
        #endregion
    }
}