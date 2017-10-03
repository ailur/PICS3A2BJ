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
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Lógica de interacción para StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private GameWindow mainWindow;

        public int NumberOfDecks { get; set; }
        public int NumberOfPlayers { get; set; }
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnCheck_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    TextBox textBox = new TextBox {Name = "txtPlayerName" + i, Text = "Player" + i + "Name"};
                    textBox.GotFocus += TextBoxOnGotFocus;
                    panelPlayers.Children.Add(textBox);
                }
                Button btnStart = new Button { Name = "btnStart", Content = "Start Game" };
                btnStart.Click += btnStart_Click;
                panelMain.Children.Add(btnStart);
            }
        }

        private void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxOnGotFocus;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            List<string> playerList = new List<string>();
            foreach (TextBox textBox in panelPlayers.Children)
            {
                playerList.Add(textBox.Text);
            }
            mainWindow = new GameWindow(playerList.Count, NumberOfDecks, playerList); //TODO: Lista jugadores
            mainWindow.Show();
            this.Close();
        }

        private bool CheckData()
        {
            int numberOfPlayers = 0;
            int numberOfDecks = 0;
            if (string.IsNullOrWhiteSpace(txtPlayers.Text) ||
                !int.TryParse(txtPlayers.Text, out numberOfPlayers) &&
                numberOfPlayers < 0)
            {
                MessageBox.Show("Could not parse Number of players.", "Please, Check data", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }
            NumberOfPlayers = numberOfPlayers;
            if (string.IsNullOrWhiteSpace(txtNumberOfDecks.Text) ||
                !int.TryParse(txtNumberOfDecks.Text, out numberOfDecks) ||
                numberOfDecks < 0)
            {
                MessageBox.Show("Could not parse Number of decks.", "Please, Check data", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }
            NumberOfDecks = numberOfDecks;
            if (numberOfDecks * 13 * 4 >= numberOfPlayers * 2 + 1)
            {
                return true;
            }
            MessageBox.Show("Not enough decks for that number of players.", "Need more decks", MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
            return false;
        }
    }
}
