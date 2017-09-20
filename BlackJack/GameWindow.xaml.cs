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
        private Player currentPlayer;
        public GameWindow()
        {
            InitializeComponent();
        }

        private void btnReshuffle_Click(object sender, RoutedEventArgs e)
        {
            croupier.Reshuffle();
            txtDebug.Text = croupier.Deck.ToString();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPlayers.Text))
            {
                croupier = new Croupier(int.Parse(txtPlayers.Text));
            }
            else
            {
                croupier = new Croupier();
            }
            if (!string.IsNullOrWhiteSpace(txtNumberOfDecks.Text))
            {
                croupier.StartGame(int.Parse(txtNumberOfDecks.Text));
            }
            else
            {
                croupier.StartGame();
            }
            updateCards(croupier);
            updateCards(croupier.Players[0]);
            txtDebug.Text = croupier.Deck.ToString();
        }

        private void updateCards(Player player)
        {
            if(player is Croupier)
            {
                Croupier.Children.Clear();
            }
            else
            {
                PlayerDeck.Children.Clear();
            }
            foreach (Card card in player.Hand)
            {
                string src = "CardGUI/" + card.ToStringShort + ".png";
                Image img = new Image();
                img.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;
                img.Height = 96;
                img.ToolTip = card.ToString() + "\n" + card.Value;
                if (player is Croupier)
                {
                    Croupier.Children.Add(img);
                }
                else
                {
                    PlayerDeck.Children.Add(img);
                }
            }
        }

        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            croupier.GiveCard(0);
            updateCards(croupier);
            updateCards(croupier.Players[0]);
            txtDebug.Text = croupier.Deck.ToString();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            updateCards(croupier.Players[1]);
        }
    }
}
