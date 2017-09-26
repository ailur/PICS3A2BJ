using GameCardLib;
using System;
using System.Collections.Generic;
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
                img.ToolTip = card.ToString() + "\n" + card.Value;
                if (player is Croupier) { CroupierDeck.Children.Add(img); }
                else { PlayerDeck.Children.Add(img); }
            }
            updateScores(player);
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
            if (!string.IsNullOrWhiteSpace(txtPlayers.Text))
            {
                Croupier = new Croupier(int.Parse(txtPlayers.Text));
            }
            else
            {
                Croupier = new Croupier();
            }
            if (!string.IsNullOrWhiteSpace(txtNumberOfDecks.Text))
            {
                Croupier.StartGame(int.Parse(txtNumberOfDecks.Text));
            }
            else
            {
                Croupier.StartGame();
            }
            updateCards(Croupier);
            updateCards(Croupier.GetPlayer());
            if (Croupier.GetPlayer().Hand.Score >= 21) { btnDrawCard.IsEnabled = false; }
            else { btnDrawCard.IsEnabled = true; }
            Debug();
            GameStarted = true;
        }

        private void Debug()
        {
            txtDebug.Text = Croupier.DeckString;
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
            }
        }

        private void imgDeck_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnDrawCard_Click(sender, e);
        }
    }
}