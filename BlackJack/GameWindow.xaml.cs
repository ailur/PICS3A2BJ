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
            croupier = new Croupier(int.Parse(txtPlayers.Text));
            croupier.StartGame();
            updateCards();
        }

        private void updateCards()
        {
            Croupier.Children.Clear();
            foreach (Card card in croupier.Hand)
            {
                string src = "CardGUI/" + card.ToStringShort + ".png";
                Image img = new Image();
                img.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;
                img.Height = 96;
                Croupier.Children.Add(img);
            }
            PlayerDeck.Children.Clear();
            foreach (Card card in croupier.Players[0].Hand)
            {
                string src = "CardGUI/" + card.ToStringShort + ".png";
                Image img = new Image();
                img.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;
                img.Height = 96;
                PlayerDeck.Children.Add(img);
            }
        }

        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            croupier.GiveCard(0);
            updateCards();
            txtDebug.Text = croupier.Deck.ToString();
        }
    }
}
