using Menus;
using MovingObstacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace periode_1_gebruikersinteractie_groep22 {
    /// <summary>
    /// Interaction logic for PausePage.xaml
    /// </summary>
    public partial class PausePage : Page {

        private DispatcherTimer timer;

        public PausePage()
        {
            InitializeComponent();
            this.Visibility = Visibility.Visible;

            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(10);

            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

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
