using System;
using GameCardLib;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BlackJack
{
    /// <summary>
    /// Lógica de interacción para GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        #region fields
        private bool gameStarted;

        private enum cmbDbSets
        {
            Games,
            Cards,
            Decks,
            Players
        }
        #endregion
        #region Properties
        private Game Game { get; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Constructor with 2 parameters and 1 optional parameter
        /// </summary>
        /// <param name="numberOfPlayers">Number of players</param>
        /// <param name="numberOfDecks">Number of decks</param>
        /// <param name="playerList">(Optional)List with players' names</param>
        public GameWindow(int numberOfPlayers, int numberOfDecks, List<string> playerList = null)
        {
            InitializeComponent();
            gameStarted = false;
            CanDraw(false);
            Game = playerList == null ? new Game(numberOfPlayers) : new Game(playerList);
            Game.StartGame(numberOfDecks);
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
            DataBaseShow.ItemsSource = Game.GetContext().Cards.Local.ToList();
        }
        #endregion
        private void CanDraw(bool can)
        {
            btnDrawCard.IsEnabled = can;
            imgDeck.IsEnabled = can;
            imgDeck.Opacity = can ? 1 : 0.5;
        }
        /// <summary>
        /// Update the images of player hand
        /// </summary>
        /// <param name="player">Player which cards will be drawn</param>
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
        /// Update images of discarded deck
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
        /// Update scores to show
        /// </summary>
        /// <param name="player">Player whose score will be read</param>
        private void UpdateScores(Player player)
        {
            TextBlock updatedTextBlock;
            if (player.IsCroupier)
            {
                updatedTextBlock = txtCroupierScore;
                updatedTextBlock.Text = "Croupier\nscore:\n" + player.Score.ToString();
            }
            else
            {
                txtPlayerName.Text = player.Name;
                updatedTextBlock = txtPlayerScore;
                updatedTextBlock.Text = "Your score:\n" + player.Score.ToString();
            }
            updatedTextBlock.Foreground = Brushes.Black;
            if (player.Score > 21) { updatedTextBlock.Foreground = Brushes.Red; }
            else if (player.Score == 21) { updatedTextBlock.Foreground = Brushes.Green; }
        }

        /// <summary>
        /// Check if hand scores if greater than 21, if that is the case player can't draw cards
        /// </summary>
        private void CheckHand()
        {
            CanDraw(Game.GetPlayer().Score < 21);
        }

        /// <summary>
        /// DEBUG method. Shows cards in deck and dicarded deck.
        /// </summary>
        [ConditionalAttribute("DEBUG")]
        private void Debug()
        {
            string txtDeck = "Deck: " + Game.DeckString.Replace("c", "♣").Replace("d", "♦").Replace("h", "♥")
                .Replace("s", "♠").ToUpper();
            txtDebug.Text = txtDeck;
            if (Game.DiscardedCount > 0)
            {
                string txtDiscarded = "";
                txtDiscarded = "Discarded: " + Game.DiscardedString.Replace("c", "♣").Replace("d", "♦").Replace("h", "♥")
                                   .Replace("s", "♠").ToUpper();
                txtDebug.Text += "\n" + txtDiscarded;
            }
        }

        #region events
        /// <summary>
        /// Start next round, update all cards, discarded deck.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            Game.ContinueGame();
            UpdateCards(Game.GetCroupier());
            UpdateCards(Game.GetPlayer());
            UpdateDiscarded();
            CheckHand();
            Debug();
            DataBaseShow.Items.Refresh();
        }

        /// <summary>
        /// Reshuffle the deck
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
        /// Give card to current player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
            {
                Game.GiveCard();
                UpdateCards(Game.GetPlayer());
                UpdateDiscarded();
                CheckHand();
                Debug();
                DataBaseShow.Items.Refresh();
            }
        }

        /// <summary>
        /// Go to next player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
            {
                UpdateCards(Game.NextPlayer());
                CheckHand();
                UpdateCards(Game.GetCroupier());
                UpdateDiscarded();
                DataBaseShow.Items.Refresh();
            }
        }

        /// <summary>
        /// Draw a card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgDeck_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnDrawCard_Click(sender, e);
        }

        private void cmbDbSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((cmbDbSets)cmbDbSet.SelectedItem)
            {
                case cmbDbSets.Cards:
                    DataBaseShow.ItemsSource = Game.GetContext().Cards.Local.ToList();
                    break;
                case cmbDbSets.Decks:
                    DataBaseShow.ItemsSource = Game.GetContext().Decks.Local.ToList();
                    break;
                case cmbDbSets.Games:
                    DataBaseShow.ItemsSource = Game.GetContext().Games.Local.ToList();
                    break;
                case cmbDbSets.Players:
                    DataBaseShow.ItemsSource = Game.GetContext().Players.Local.ToList();
                    break;
            }
        }
        #endregion

        #endregion
    }
}