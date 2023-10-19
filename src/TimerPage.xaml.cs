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


    public partial class TimerPage : Page {

        private DispatcherTimer timer;

        private string correctPincode = "1234";
        private string input = "";
        private string pincode = "";
        private string minutes = "";
        private bool pincodeCorrect = false;

        public static int timerTime;


        public TimerPage()
        {
            InitializeComponent();

            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(10);

            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(InvoerPincode.Content.ToString() == "")
                InvoerPincode.Content = "Pincode Invoeren";

            if(InvoerSpeeltijd.Content.ToString() == "")
                InvoerSpeeltijd.Content = "Speeltijd invoeren";

            if(pincode.Length == correctPincode.Length)
            {
                if(pincode == correctPincode)
                {
                    var bc = new BrushConverter();
                    PincodeBorder.BorderBrush = Brushes.Green;
                    PincodeBorder.Background = (Brush)bc.ConvertFrom("#FFFF7200");
                    SpeelTijdBorder.Background = (Brush)bc.ConvertFrom("#00ff0f");
                    pincodeCorrect = true;
                }
                else
                {
                    PincodeBorder.BorderBrush = Brushes.Red;
                }
            }

            if(pincode.Length != 0)
            {
                InvoerPincode.Content = pincode;
            }

            if(minutes.Length != 0)
            {
                InvoerSpeeltijd.Content = minutes;
            }

            if (pincode.Length == correctPincode.Length)
            {
                pincode = "";
                input = "";
            }

            if (!pincodeCorrect)
            {
                pincode = input;
            }
            else
            {
                minutes = input;
            }
        }

        private void Knop1_Click(object sender, RoutedEventArgs e)
        {
            input += "1";
        }

        private void Knop2_Click(object sender, RoutedEventArgs e)
        {
            input += "2";
        }

        private void Knop3_Click(object sender, RoutedEventArgs e)
        {
            input += "3";
        }

        private void Knop4_Click(object sender, RoutedEventArgs e)
        {
            input += "4";
        }

        private void Knop5_Click(object sender, RoutedEventArgs e)
        {
            input += "5";
        }

        private void Knop6_Click(object sender, RoutedEventArgs e)
        {
            input += "6";
        }

        private void Knop7_Click(object sender, RoutedEventArgs e)
        {
            input += "7";
        }

        private void Knop8_Click(object sender, RoutedEventArgs e)
        {
            input += "8";
        }

        private void Knop9_Click(object sender, RoutedEventArgs e)
        {
            input += "9";
        }

        private void Knop0_Click(object sender, RoutedEventArgs e)
        {
            input += "0";
        }

        private void KnopKlaar_Click(object sender, RoutedEventArgs e)
        {
            if(minutes != "")
                timerTime = Convert.ToInt32(minutes);

            this.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
