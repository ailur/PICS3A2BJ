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
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            croupier = new Croupier();
            croupier.StartGame();
            updateCards();
        }

        private void updateCards()
        {
            //ESTO VA A CLASE CARTA
            Dictionary<EnumSuite,string> suiteDict = new Dictionary<EnumSuite,string>();
            suiteDict.Add(EnumSuite.Clubs, "c");
            suiteDict.Add(EnumSuite.Diamonds, "d");
            suiteDict.Add(EnumSuite.Hearts, "h");
            suiteDict.Add(EnumSuite.Spades, "s");
            Dictionary<EnumValue, string> valueDict = new Dictionary<EnumValue, string>();
            valueDict.Add(EnumValue.Ace, "1");
            valueDict.Add(EnumValue.Two, "2");
            valueDict.Add(EnumValue.Three, "3");
            valueDict.Add(EnumValue.Four, "4");
            valueDict.Add(EnumValue.Five, "5");
            valueDict.Add(EnumValue.Six, "6");
            valueDict.Add(EnumValue.Seven, "7");
            valueDict.Add(EnumValue.Eight, "8");
            valueDict.Add(EnumValue.Nine, "9");
            valueDict.Add(EnumValue.Ten, "10");
            valueDict.Add(EnumValue.Jack, "j");
            valueDict.Add(EnumValue.Queen, "q");
            valueDict.Add(EnumValue.King, "k");
            Croupier.Children.Clear();
            foreach (Card card in croupier.Hand)
            {
                string src = "CardGUI/"+suiteDict[card.Suite]+valueDict[card.ValueEnum]+".png";

                Image img = new Image();

                img.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;

                Croupier.Children.Add(img);
            }
            Players.Children.Clear();
            foreach (Card card in croupier.Players[0].Hand)
            {
                string src = "CardGUI/" + suiteDict[card.Suite] + valueDict[card.ValueEnum] + ".png";

                Image img = new Image();

                img.Source = new ImageSourceConverter().ConvertFromString(src) as ImageSource;

                Players.Children.Add(img);
            }
        }
    }
}
