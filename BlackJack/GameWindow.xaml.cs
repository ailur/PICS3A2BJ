using GameCardLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Lógica de interacción para GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        #region fields
        private Croupier croupier;
        private bool gameStarted;
        private int numberOfPlayers;
        private int numberOfDecks;
        private List<string> playerList;
        #endregion
        #region Properties
        private Croupier Croupier { get => croupier; set => croupier = value; }
        private bool GameStarted { get => gameStarted; set => gameStarted = value; }
        private int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        private int NumberOfDecks { get => numberOfDecks; set => numberOfDecks = value; }
        private List<string> PlayerList { get => playerList; set => playerList = value; }
        #endregion
        #region Methods()
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public GameWindow() : this(1,1)
        {
        }

        /// <summary>
        /// Constructor with 2 parameters and 1 optional parameter
        /// </summary>
        /// <param name="numberOfPlayers">Number of players</param>
        /// <param name="numberOfDecks">Number of decks</param>
        /// <param name="playerList">(Optional)List with players' names</param>
        public GameWindow(int numberOfPlayers, int numberOfDecks, List<string> playerList = null)
        {
            PlayerList = playerList;
            InitializeComponent();
            GameStarted = false;
            btnDrawCard.IsEnabled = false;
            imgDeck.IsEnabled = false;
            imgDeck.Opacity = 0.5;
            NumberOfPlayers = numberOfPlayers;
            NumberOfDecks = numberOfDecks;

            if (PlayerList != null) { Croupier = new Croupier(PlayerList); }
            else { Croupier = new Croupier(NumberOfPlayers); }
            Croupier.StartGame(NumberOfDecks);
            UpdateCards(Croupier);
            UpdateCards(Croupier.GetPlayer());
            UpdateDiscarded();
            CheckHand();
            Debug();
            #if DEBUG
            txtDebug.Visibility = Visibility.Visible;
            #else
            txtDebug.Visibility = Visibility.Collapsed;
            #endif
            GameStarted = true;
        }
        #endregion
        /// <summary>
        /// Update the images of player hand
        /// </summary>
        /// <param name="player">Player which cards will be drawn</param>
        private void UpdateCards(Player player)
        {
            if (player is Croupier) { CroupierDeck.Children.Clear(); }
            else { PlayerDeck.Children.Clear(); }
            foreach (Card card in player.Hand)
            {
                string src = "CardGUI/" + card.ToStringShort + ".png";
                Image img = new Image
                {
                    Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource,
                    Height = 96,
                    Width = 75,
                    ToolTip = card + "\n" + card.Value
                };
                if (player is Croupier) { CroupierDeck.Children.Add(img); }
                else { PlayerDeck.Children.Add(img); }
            }
            UpdateScores(player);
        }

        /// <summary>
        /// Update images of discarded deck
        /// </summary>
        private void UpdateDiscarded()
        {
            if (Croupier.DiscardedCount > 0)
            {
                imgDiscard.Source =
                    new ImageSourceConverter().ConvertFromString(
                        "CardGUI/" + Croupier.LastCardDiscarded.ToStringShort + ".png") as ImageSource;
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
            if (player is Croupier)
            {
                updatedTextBlock = txtCroupierScore;
                updatedTextBlock.Text = "Croupier\nscore:\n" + player.Hand.Score.ToString();
            }
            else
            {
                txtPlayerName.Text = player.Name;
                updatedTextBlock = txtPlayerScore;
                updatedTextBlock.Text = "Your score:\n" + player.Hand.Score.ToString();
            }
            updatedTextBlock.Foreground = Brushes.Black;
            if (player.Hand.Score > 21) { updatedTextBlock.Foreground = Brushes.Red; }
            else if (player.Hand.Score == 21) { updatedTextBlock.Foreground = Brushes.Green; }
        }

        /// <summary>
        /// Check if hand scores if greater than 21, if that is the case player can't draw cards
        /// </summary>
        private void CheckHand()
        {
            if (Croupier.GetPlayer().Hand.Score >= 21)
            {
                btnDrawCard.IsEnabled = false;
                imgDeck.IsEnabled = false;
                imgDeck.Opacity = 0.5;
            }
            else
            {
                btnDrawCard.IsEnabled = true;
                imgDeck.IsEnabled = true;
                imgDeck.Opacity = 1;
            }
        }

        /// <summary>
        /// DEBUG method. Shows cards in deck and dicarded deck.
        /// </summary>
        [ConditionalAttribute("DEBUG")]
        private void Debug()
        {
            txtDebug.Text = Croupier.DeckString;
            if (Croupier.DiscardedCount > 0)
            {
                txtDebug.Text += "\n" + Croupier.DiscardedString;
            }
            txtDebug.Text = txtDebug.Text.Replace("c", "♣").Replace("d", "♦").Replace("h", "♥").Replace("s", "♠").ToUpper();
        }

        #region events
        /// <summary>
        /// Start next round, update all cards, discarded deck.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            Croupier.ContinueGame();
            UpdateCards(Croupier);
            UpdateCards(Croupier.GetPlayer());
            UpdateDiscarded();
            CheckHand();
            Debug();
        }

        /// <summary>
        /// Reshuffle the deck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReshuffle_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                Croupier.Reshuffle();
                Debug();
            }
        }

        /// <summary>
        /// Give card to current player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                Croupier.GiveCard();
                UpdateCards(Croupier.GetPlayer());
                UpdateDiscarded();
                CheckHand();
                Debug();
            }
        }

        /// <summary>
        /// Go to next player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                UpdateCards(Croupier.NextPlayer());
                CheckHand();
                UpdateCards(Croupier);
                UpdateDiscarded();
                btnDrawCard.IsEnabled = false;
                imgDeck.IsEnabled = false;
                imgDeck.Opacity = 0.5;
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
        #endregion
        #endregion
    }
}