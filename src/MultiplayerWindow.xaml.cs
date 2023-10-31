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

        private int Timertime;
        private int player1id = 1;
        private int player2id = 2;
        public MultiplayerWindow(int timerTime)
        {
            Timertime = timerTime;
            InitializeComponent();

            ImageUpdate();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if(player1id != player2id)
            {
                GameWindow gameWindow = new GameWindow(true, Timertime, player1id, player2id);
                gameWindow.Show();
                this.Close();
            }
        }

        private void Player1Up_Click(object sender, RoutedEventArgs e)
        {
            player1id++;
            if (player1id == 21)
                player1id = 1;
            ImageUpdate();

        }

        private void Player1Down_Click(object sender, RoutedEventArgs e)
        {
            player1id--;
            if (player1id == 0)
                player1id = 20;
            ImageUpdate();
        }

        private void Player2Up_Click(object sender, RoutedEventArgs e)
        {
            player2id++;
            if (player2id == 21)
                player2id = 1;
            ImageUpdate();
        }

        private void Player2Down_Click(object sender, RoutedEventArgs e)
        {
            player2id--;
            if (player2id == 0)
                player2id = 20;
            ImageUpdate();
        }

        private void ImageUpdate()
        {
            Player1Image.Source = new BitmapImage(new Uri(@"\src\resources\heads\Lego_hoofd" + player1id + ".png", UriKind.Relative));
            Player2Image.Source = new BitmapImage(new Uri(@"\src\resources\heads\Lego_hoofd" + player2id + ".png", UriKind.Relative));
        }
        
    }
}
