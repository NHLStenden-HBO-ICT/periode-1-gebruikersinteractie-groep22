using System;

using System.Collections.Generic;

using System.Windows;

using System.Windows.Controls;

using System.Windows.Input;

using System.Windows.Media;

using System.Windows.Shapes;

using System.Windows.Threading;



namespace MovingObstacles

{

    public partial class MainWindow : Window

    {

        private double playerLeft;

        private double playerTop;

        private List<Rectangle> obstacles;

        private List<bool> obstacleDirections;

        private DispatcherTimer timer;

        private int obstacleSpeed = 12;

        private int obstacleCount = 8; // Aantal obstakels



        private double finishLineY; // Y-coördinaat van de finishlijn

        private double finishLineHeight = 5; // Hoogte van de finishlijn



        public MainWindow()

        {

            InitializeComponent();

            playerLeft = Canvas.GetLeft(player);

            playerTop = Canvas.GetTop(player);

            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(10);

            timer.Tick += Timer_Tick;

            timer.Start();



            

            InitializeObstacles();



            // Bepaal de Y-coördinaat van de finishlijn 

            finishLineY = 50;



            // Finishlijn toegevoegd

            AddFinishLine();

        }



        private void InitializeObstacles()

        {

            obstacles = new List<Rectangle>();

            obstacleDirections = new List<bool>();



            for (int i = 0; i < obstacleCount; i++)

            {

                Rectangle obstacle = new Rectangle

                {

                    Width = 30,

                    Height = 30,

                    Fill = Brushes.Red

                };



                // Set initial position

                Canvas.SetLeft(obstacle, i * 150); // Spread obstacles horizontally

                Canvas.SetTop(obstacle, 100 + i * 50); // Spread obstacles vertically

                gameCanvas.Children.Add(obstacle);

                obstacles.Add(obstacle);



                // Set initial direction (true for right, false for left)

                obstacleDirections.Add(i % 2 == 0); 

            }

        }



        private void AddFinishLine()

        {

            Line finishLine = new Line

            {

                X1 = 0,

                Y1 = finishLineY,

                X2 = gameCanvas.ActualWidth,

                Y2 = finishLineY,

                Stroke = Brushes.Green,

                StrokeThickness = finishLineHeight

            };



            gameCanvas.Children.Add(finishLine);

        }



        private void Timer_Tick(object sender, EventArgs e)

        {

            // Speler bewegen met pijltjes

            if (Keyboard.IsKeyDown(Key.Up) && playerTop > 0)

                playerTop -= 5;

            if (Keyboard.IsKeyDown(Key.Down) && playerTop < gameCanvas.ActualHeight - player.Height)

                playerTop += 5;

            if (Keyboard.IsKeyDown(Key.Left) && playerLeft > 0)

                playerLeft -= 5;

            if (Keyboard.IsKeyDown(Key.Right) && playerLeft < gameCanvas.ActualWidth - player.Width)

                playerLeft += 5;



            // Move obstacles

            for (int i = 0; i < obstacles.Count; i++)

            {

                double obstacleLeft = Canvas.GetLeft(obstacles[i]);

                if (obstacleDirections[i])

                    obstacleLeft += obstacleSpeed;  // Naar rechts

                else

                    obstacleLeft -= obstacleSpeed;  // Naar links



                

                if (obstacleLeft > gameCanvas.ActualWidth - obstacles[i].Width || obstacleLeft < 0)

                    obstacleDirections[i] = !obstacleDirections[i];



                Canvas.SetLeft(obstacles[i], obstacleLeft);



                // Check for collision with the player

                if (IsCollision(player, obstacles[i]))

                {

                    timer.Stop();

                    MessageBox.Show("Game Over! Je hebt een obstakel geraakt.");

                    ResetGame();

                }

            }



            // Check if the player has reached the finish line

            if (playerTop <= finishLineY + finishLineHeight)

            {

                timer.Stop();

                MessageBox.Show("Gefeliciteerd!, je hebt de finish bereikt.");

                ResetGame();

            }



            // Update positie

            Canvas.SetTop(player, playerTop);

            Canvas.SetLeft(player, playerLeft);

        }



        private bool IsCollision(UIElement element1, UIElement element2)

        {

            Rect rect1 = new Rect(Canvas.GetLeft(element1), Canvas.GetTop(element1), element1.RenderSize.Width, element1.RenderSize.Height);

            Rect rect2 = new Rect(Canvas.GetLeft(element2), Canvas.GetTop(element2), element2.RenderSize.Width, element2.RenderSize.Height);

            return rect1.IntersectsWith(rect2);

        }



        private void ResetGame()

        {

            Canvas.SetLeft(player, 375);

            Canvas.SetTop(player, 500);

            playerLeft = 375;

            playerTop = 500;

            timer.Start();

        }

    }

}