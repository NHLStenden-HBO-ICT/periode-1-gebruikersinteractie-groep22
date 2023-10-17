using Menus;
using MovingObstacles;
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

namespace periode_1_gebruikersinteractie_groep22 {
    /// <summary>
    /// Interaction logic for MultiplayerWindow.xaml
    /// </summary>
    public partial class MultiplayerWindow : Window {
        public MultiplayerWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(true);
            gameWindow.Show();
            this.Close();
        }

        private void Player1Up_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Player1Down_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Player2Up_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Player2Down_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
