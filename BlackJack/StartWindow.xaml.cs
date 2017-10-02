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
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btnCheck_OnClick(object sender, RoutedEventArgs e)
        {
            CheckData();
        }

        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(txtPlayers.Text) ||
                !int.TryParse(txtPlayers.Text, out int numberOfPlayers) &&
                numberOfPlayers < 0)
            {
                MessageBox.Show("Could not parse Number of players.", "Please, Check data", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNumberOfDecks.Text) &&
                !int.TryParse(txtNumberOfDecks.Text, out int numberOfDecks) &&
                numberOfDecks < 0)
            {
                MessageBox.Show("Could not parse Number of decks.", "Please, Check data", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            if (numberOfDecks * 13 * 4 < numberOfPlayers * 2 + 1)
            {
                MessageBox.Show("Not enough decks for that number of players.", "Need more decks", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }
    }
}
