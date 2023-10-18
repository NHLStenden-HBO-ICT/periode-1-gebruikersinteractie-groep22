using MovingObstacles;
using periode_1_gebruikersinteractie_groep22;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Menus {

    public partial class MainWindow : Window {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Uitleg_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UitlegPage();
        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerWindow multiplayerWindow = new MultiplayerWindow(TimerPage.timerTime);
            multiplayerWindow.Show();
            this.Close();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(false, TimerPage.timerTime);
            gameWindow.Show();
            this.Close();
        }

        private void Tijd_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimerPage();
        }
    }
}