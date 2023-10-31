using Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using System.Windows.Threading;

namespace periode_1_gebruikersinteractie_groep22 {

    public partial class UitlegPage : Page{

        private bool toggle = false;
        private DispatcherTimer timer;
        public UitlegPage()
        {
            InitializeComponent();


            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(1000);

            timer.Tick += Timer_Tick;

            timer.Start();


            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            toggle = !toggle;

            if (toggle)
            {
                Wk.Content = "↑";
                Ak.Content = "←";
                Sk.Content = "↓";
                Dk.Content = "→";
            } else
            {
                Wk.Content = "W";
                Ak.Content = "A";
                Sk.Content = "S";
                Dk.Content = "D";
            }
        }



            private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
