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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace periode_1_gebruikersinteractie_groep22.src {
    /// <summary>
    /// Interaction logic for WinPage.xaml
    /// </summary>
    public partial class WinPage : Page {
        public WinPage(int playerWin)
        {
            InitializeComponent();

            PlayerWin.Content = "Player" + playerWin;
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerWindow multiplayerWindow = new MultiplayerWindow();
            multiplayerWindow.Show();
            GameWindow.timer.Start();
            GameWindow.closeWindow = true;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            GameWindow.timer.Start();
            GameWindow.closeWindow = true;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.timer.Start();
            GameWindow.closeWindow = true;
        }
    }
}
