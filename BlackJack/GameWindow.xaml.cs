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
        private Croupier croupier;
        private bool gameStarted;
        private int numberOfPlayers;
        private int numberOfDecks;

        private Croupier Croupier { get => croupier; set => croupier = value; }
        private bool GameStarted { get => gameStarted; set => gameStarted = value; }
        private int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        private int NumberOfDecks { get => numberOfDecks; set => numberOfDecks = value; }

        public GameWindow() : this(1,1)
        {
        }

        public GameWindow(int numberOfPlayers, int numberOfDecks)
        {
            InitializeComponent();
            GameStarted = false;
            btnDrawCard.IsEnabled = false;
            imgDeck.IsEnabled = false;
            imgDeck.Opacity = 0.5;
            NumberOfPlayers = numberOfPlayers;
            NumberOfDecks = numberOfDecks;
        }

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

        private void UpdateDiscarded()
        {
            if(Croupier.Discarded.Count > 0)
            {
                imgDiscard.Source = new ImageSourceConverter().ConvertFromString("CardGUI/" + Croupier.Discarded.Peek().ToStringShort + ".png") as ImageSource;
            }
        }

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
                updatedTextBlock = txtPlayerScore;
                updatedTextBlock.Text = "Your score:\n" + player.Hand.Score.ToString();
            }
            updatedTextBlock.Foreground = Brushes.Black;
            if (player.Hand.Score > 21) { updatedTextBlock.Foreground = Brushes.Red; }
            else if (player.Hand.Score == 21) { updatedTextBlock.Foreground = Brushes.Green; }
        }

        private void CheckDeck()
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

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!GameStarted)
            {
                Croupier = new Croupier(NumberOfPlayers);
                Croupier.StartGame(NumberOfDecks);
                UpdateCards(Croupier);
                UpdateCards(Croupier.GetPlayer());
                UpdateDiscarded();
                CheckDeck();
                Debug();
                GameStarted = true;
            }
            else
            {
                Croupier.ContinueGame();
                UpdateCards(Croupier);
                UpdateCards(Croupier.GetPlayer());
                UpdateDiscarded();
                CheckDeck();
                Debug();
            }
        }

        [ConditionalAttribute("DEBUG")]
        private void Debug()
        {
            txtDebug.Text = Croupier.DeckString;
            if (Croupier.Discarded.Count > 0)
            {
                txtDebug.Text += "\n" + Croupier.DiscardedString;
            }
        }

        private void btnReshuffle_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                Croupier.Reshuffle();
                Debug();
            }
        }

        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                Croupier.GiveCard();
                UpdateCards(Croupier.GetPlayer());
                UpdateDiscarded();
                CheckDeck();
                Debug();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                UpdateCards(Croupier.NextPlayer());
                CheckDeck();
                UpdateCards(Croupier);
                UpdateDiscarded();
                btnDrawCard.IsEnabled = false;
                imgDeck.IsEnabled = false;
                imgDeck.Opacity = 0.5;
            }
        }

        private void imgDeck_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnDrawCard_Click(sender, e);
        }
    }
}