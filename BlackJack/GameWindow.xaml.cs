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

        private Croupier Croupier { get => croupier; set => croupier = value; }
        private bool GameStarted { get => gameStarted; set => gameStarted = value; }

        public GameWindow()
        {
            InitializeComponent();
            GameStarted = false;
            btnDrawCard.IsEnabled = false;
            imgDeck.IsEnabled = false;
            imgDeck.Opacity = 0.5;
        }

        private void updateCards(Player player)
        {
            if (player is Croupier) { CroupierDeck.Children.Clear(); }
            else { PlayerDeck.Children.Clear(); }
            foreach (Card card in player.Hand)
            {
                string src = "CardGUI/" + card.ToStringShort + ".png";
                Image img = new Image();
                img.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;
                img.Height = 96;
                img.Width = 75;
                img.ToolTip = card.ToString() + "\n" + card.Value;
                if (player is Croupier) { CroupierDeck.Children.Add(img); }
                else { PlayerDeck.Children.Add(img); }
            }
            updateScores(player);
        }

        private void updateDiscarded()
        {
            if(Croupier.Discarded.Count > 0)
            {
                imgDiscard.Source = new ImageSourceConverter().ConvertFromString("CardGUI/" + Croupier.Discarded.Peek().ToStringShort + ".png") as ImageSource;
            }
        }

        private void updateScores(Player player)
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
                int numberOfPlayers;
                if (!string.IsNullOrWhiteSpace(txtPlayers.Text) && int.TryParse(txtPlayers.Text, out numberOfPlayers) && numberOfPlayers > 0) { Croupier = new Croupier(numberOfPlayers); }
                else { Croupier = new Croupier(); }
                int numberOfDecks;
                if (!string.IsNullOrWhiteSpace(txtNumberOfDecks.Text) && int.TryParse(txtNumberOfDecks.Text, out numberOfDecks) && numberOfDecks > 0) { Croupier.StartGame(int.Parse(txtNumberOfDecks.Text)); }
                else { Croupier.StartGame(); }
                updateCards(Croupier);
                updateCards(Croupier.GetPlayer());
                updateDiscarded();
                CheckDeck();
                Debug();
                GameStarted = true;
            }
            else
            {
                Croupier.ContinueGame();
                updateCards(Croupier);
                updateCards(Croupier.GetPlayer());
                updateDiscarded();
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
                updateCards(Croupier.GetPlayer());
                updateDiscarded();
                CheckDeck();
                Debug();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                updateCards(Croupier.NextPlayer());
                CheckDeck();
                updateCards(Croupier);
                updateDiscarded();
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