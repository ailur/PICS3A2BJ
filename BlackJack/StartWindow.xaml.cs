using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BlackJack
{
    /// <summary>
    /// Lógica de interacción para StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        #region fields
        private GameWindow mainWindow;
        #endregion
        #region Properties
        private int NumberOfDecks { get; set; }
        private int NumberOfPlayers { get; set; }
        #endregion
        #region Methods()
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public StartWindow()
        {
            InitializeComponent();
        }
        #endregion
        /// <summary>
        /// Checks input data
        /// </summary>
        /// <returns>True if successful</returns>
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
            NumberOfPlayers = numberOfPlayers;
            if (string.IsNullOrWhiteSpace(txtNumberOfDecks.Text) ||
                !int.TryParse(txtNumberOfDecks.Text, out int numberOfDecks) ||
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
        #region Events()
        /// <summary>
        /// When check button is clicked, if Data is ok, show textboxes for name inputs and a button to start the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                panelPlayers.Children.Clear();
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    TextBox textBox = new TextBox {Name = "txtPlayerName" + i, Text = "Player " + i + " Name"};
                    textBox.GotFocus += TextBoxOnGotFocus;
                    panelPlayers.Children.Add(textBox);
                }
                Button btnStart = new Button { Name = "btnStart", Content = "Start Game" };
                btnStart.Click += btnStart_Click;
                panelPlayers.Children.Add(btnStart);
            }
        }

        /// <summary>
        /// When a textbox got focus, empty default name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxOnGotFocus;
        }

        /// <summary>
        /// Start a new game with the names entered on the textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            List<string> playerList = new List<string>();
            foreach (TextBox textBox in panelPlayers.Children.OfType<TextBox>())
            {
                playerList.Add(textBox.Text);
            }
            mainWindow = new GameWindow(playerList.Count, NumberOfDecks, playerList);
            mainWindow.Show();
            Close();
        }
        #endregion
        #endregion
    }
}
