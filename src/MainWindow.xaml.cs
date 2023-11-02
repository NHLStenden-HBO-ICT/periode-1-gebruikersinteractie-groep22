using MovingObstacles;
using periode_1_gebruikersinteractie_groep22;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Menus {

    public partial class MainWindow : Window {

        private DispatcherTimer timer;

        private string lockOut = "";

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(10);

            timer.Tick += Timer_Tick;

            timer.Start();


            if (!File.Exists("./Time.txt"))
                File.Create("./Time.txt");
            if (!File.Exists("./TimeOut.txt"))
                File.Create("./TimeOut.txt");

            if (File.ReadAllText("./Time.txt") == "Expired")
            {
                File.WriteAllText("TimeOut.txt", "true");
                File.WriteAllText("./Time.txt", "0");
            }
                

            lockOut = (File.ReadAllText("./TimeOut.txt"));

            if(lockOut == "true")
            {
                SinglePlayer.Background = Brushes.Red;
                MultiPlayer.Background = Brushes.Red;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(lockOut == "true")
                lockOut = (File.ReadAllText("./TimeOut.txt"));

            if (lockOut == "false")
            {
                var bc = new BrushConverter();
                SinglePlayer.Background = (Brush)bc.ConvertFrom("#00ff0f");
                MultiPlayer.Background = (Brush)bc.ConvertFrom("#00ff0f");
            }
        }


        private void Uitleg_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UitlegPage();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            if(lockOut != "true")
            {
                if (File.ReadAllText("./Time.txt") == "")
                    File.WriteAllText("./Time.txt", 0.ToString());

                MultiplayerWindow multiplayerWindow = new MultiplayerWindow();
                multiplayerWindow.Show();
                this.Close();
            }
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            if(lockOut != "true")
            {
                if (File.ReadAllText("./Time.txt") == "")
                    File.WriteAllText("./Time.txt", 0.ToString());

                GameWindow gameWindow = new GameWindow(false, 1, 1);
                gameWindow.Show();
                this.Close();
            }
        }

        private void Tijd_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimerPage();
        }
    }
}