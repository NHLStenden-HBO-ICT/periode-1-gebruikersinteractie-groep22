using System;

using System.Collections.Generic;

using System.Windows;

using System.Windows.Controls;

using System.Windows.Input;

using System.Windows.Media;

using System.Windows.Shapes;

using System.Windows.Threading;
using System.Xml.Schema;

namespace MovingObstacles

{

    public partial class GameWindow : Window

    {

        private double playerLeft;

        private double playerTop;

        private List<Rectangle> obstacles;

        private List<bool> obstacleDirections;
        private List<double> obstacleSpeeds;
        private List<double> obstacleWave;

        private DispatcherTimer timer;

        private double obstacleSpeed = 5;

        private int obstacleCount = 4; // Aantal obstakels
        private int level = 1;

        private double wave = 0;

        private double finishLineY; // Y-coördinaat van de finishlijn

        private double finishLineHeight = 5; // Hoogte van de finishlijn



        public GameWindow(bool multiPlayer)

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
            wave = 0;
            int back = 0;
            obstacles = new List<Rectangle>();

            obstacleDirections = new List<bool>();
            obstacleSpeeds = new List<double>();
            obstacleWave = new List<double>();

            int random = level * 387;
            int r = 30;
            
            


            for (int i = 0; i < obstacleCount; i++)

            {

                double w = 0;
                r = Math.Abs((random % 3)) * 30 + 30;
                if (Math.Abs(random % 15) == 0) r = 25;

                byte colorR = 0;
                byte colorG = 0;
                byte colorB = 0;

                if (r == 30) {
                    colorR = 255;
                } else if (r == 60)
                {
                    colorR = 255;
                    colorG = 255;
                } else if (r == 90)
                {
                    colorB = 255;
                } else
                {
                    colorG = 255;
                    w = 4 + Math.Abs((random % 4));
                }
                Color c = Color.FromRgb(colorR, colorG, colorB);

                Rectangle obstacle = new Rectangle

                {

                    Width = r, // random width
                    Height = 30,
                    Fill = new SolidColorBrush(c)

                };

                


                // Set initial position

                Canvas.SetLeft(obstacle, (1000 / obstacleCount) * Math.Abs((random % (obstacleCount)))); // Spread obstacles horizontally

                Canvas.SetTop(obstacle, (400 / (obstacleCount)) * (i - back) + 100); // Spread obstacles vertically

                gameCanvas.Children.Add(obstacle);

                obstacles.Add(obstacle);



                // Set initial direction (true for right, false for left)

                random = (random * 318211 + 1471343) % 167449;

                if (random % 3 == 0) back = random % 2 + 1; else back = 0;

                obstacleDirections.Add(( random + i )% 2 == 0);
                obstacleSpeeds.Add((random) % 6 / 3 + obstacleSpeed);
                obstacleWave.Add(w);
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

            if ((Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.W)) && playerTop > 0)

                playerTop -= 5;

            if ((Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.S)) && playerTop < gameCanvas.ActualHeight - player.Height)

                playerTop += 5;

            if ((Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A)) && playerLeft > 0)

                playerLeft -= 5;

            if ((Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D)) && playerLeft < gameCanvas.ActualWidth - player.Width)

                playerLeft += 5;



            // Move obstacles
            wave += .25;
            

            for (int i = 0; i < obstacles.Count; i++)

            {
                
                double obstacleLeft = Canvas.GetLeft(obstacles[i]);
                double obstacleUp = Canvas.GetTop(obstacles[i]);

                if (obstacleDirections[i])

                    obstacleLeft += obstacleSpeeds[i];  // Naar rechts

                else

                    obstacleLeft -= obstacleSpeeds[i];  // Naar links

                obstacleUp += Math.Sin(wave) * obstacleWave[i];



                if (obstacleLeft > gameCanvas.ActualWidth - obstacles[i].Width || obstacleLeft < 0)

                    if (obstacleDirections[i]) { 
                        obstacleLeft = 0;
                    } else
                    {
                        obstacleLeft = gameCanvas.ActualWidth - obstacles[i].Width;
                    }



                Canvas.SetLeft(obstacles[i], obstacleLeft);
                Canvas.SetTop(obstacles[i], obstacleUp);



                // Check for collision with the player

                if (IsCollision(player, obstacles[i]))

                {

                    timer.Stop();

                    

                    ResetGame();

                }

            }



            // Check if the player has reached the finish line

            if (playerTop <= finishLineY + finishLineHeight)

            {

                timer.Stop();
                
                level++;

                if (level % 2 == 0)
                {
                    obstacleSpeed+=.5;
                    obstacleCount++;
                }

                Level.Content = "Level: " + level;
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



            playerLeft = 640;

            playerTop = 585;

            timer.Start();

            // clear canvas but keep player object
            List<UIElement> objectsToKeep = new List<UIElement>();
            objectsToKeep.Add(player);
            objectsToKeep.Add(Level);
            gameCanvas.Children.Clear();
            foreach (UIElement obj in objectsToKeep)
            {
                gameCanvas.Children.Add(obj);
            }


            Canvas.SetLeft(player, 640);

            Canvas.SetTop(player, 585);

            InitializeObstacles();





        }

    }

}
