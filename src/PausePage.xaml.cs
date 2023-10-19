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

namespace periode_1_gebruikersinteractie_groep22 {
    /// <summary>
    /// Interaction logic for PausePage.xaml
    /// </summary>
    public partial class PausePage : Page {

        public PausePage()
        {
            InitializeComponent();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.timer.Start();
            this.Visibility = Visibility.Hidden;
        }

        private void Mainmenu_Click(object sender, RoutedEventArgs e)
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
