using periode_1_gebruikersinteractie_groep22;
using System;

using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

using System.Windows.Controls;

using System.Windows.Input;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace MovingObstacles

{

    public partial class GameWindow : Window

    {

        private double playerLeft;
        private double playerTop;

        private double player2Left = 0;
        private double player2Top = 0; 

        private List<Rectangle> obstacles;

        private List<bool> obstacleDirections;
        private List<double> obstacleSpeeds;
        private List<double> obstacleWave;

        public static DispatcherTimer timer;

        private double obstacleSpeed = 5;

        private int obstacleCount = 4; // Aantal obstakels
        private int level = 1;

        private double wave = 0;

        private double finishLineY; // Y-coördinaat van de finishlijn

        private double finishLineHeight = 5; // Hoogte van de finishlijn

        private bool Multiplayer = false;


        private int player1Score = 0;
        private int player2Score = 0;

        public static bool closeWindow = false;

        private int player1Id;
        private int player2Id;
        private int TimerTime;
        private bool directionImage;

        ImageBrush p1Image = new ImageBrush();
        ImageBrush p2Image = new ImageBrush();
        


        public GameWindow(bool multiPlayer, int timerTime, int player1id, int player2id)

        {
            player1Id = player1id;
            player2Id = player2id;
            TimerTime = timerTime;

            Multiplayer = multiPlayer;

            InitializeComponent();

            playerLeft = Canvas.GetLeft(player);

            playerTop = Canvas.GetTop(player);

            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(10);

            timer.Tick += Timer_Tick;

            timer.Start();

            p1Image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/src/resources/heads/Lego_hoofd" + player1id + ".png"));
            p2Image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/src/resources/heads/Lego_hoofd" + player2id + ".png"));

            player.Fill = p1Image;
            player2.Fill = p2Image;

            if (multiPlayer)
            {
                Level.Content = "Player 1: 0\nPlayer 2: 0";

                player2Left = Canvas.GetLeft(player2);
                player2Top = Canvas.GetTop(player2);

                Random rnd = new Random();
                level = rnd.Next(580);

                obstacleCount = 12;
                obstacleSpeed = 7;
            }
            else player2.Visibility = Visibility.Hidden;


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


            
            ScaleTransform flipTransform = new ScaleTransform(-1, 1);

            for (int i = 0; i < obstacleCount; i++)

            {
                string carColor = "";
                double w = 0;
                r = Math.Abs((random % 3)) * 30 + 30;
                if (Math.Abs(random % 15) == 0) r = 25;


                if (r == 30) {
                    carColor = "red";
                } else if (r == 60)
                {
                    carColor = "yellow";
                } else if (r == 90)
                {
                    carColor = "blue";
                } else
                {
                    carColor = "green";
                    w = 4 + Math.Abs((random % 4));
                }

                ImageBrush autoObstakel = new ImageBrush();

                autoObstakel.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/src/resources/autos/Auto_" + carColor + ".png"));
                

                Rectangle obstacle = new Rectangle

                {
                    
                    Width = r, // random width
                    Height = 30,
                    Fill = autoObstakel

                };




                // Set initial direction (true for right, false for left)

                random = (random * 318211 + 1471343) % 167449;

                if (random % 3 == 0) back = random % 2 + 1; else back = 0;

                obstacleDirections.Add(( random + i )% 2 == 0);
                obstacleSpeeds.Add((random) % 6 / 3 + obstacleSpeed);
                obstacleWave.Add(w);

                directionImage = (random + i) % 2 == 0;

                if (directionImage) obstacle.RenderTransform = flipTransform;

                // Set initial position

                Canvas.SetLeft(obstacle, (1000 / obstacleCount) * Math.Abs((random % (obstacleCount)))); // Spread obstacles horizontally

                Canvas.SetTop(obstacle, (400 / (obstacleCount)) * (i - back) + 100); // Spread obstacles vertically

                gameCanvas.Children.Add(obstacle);

                obstacles.Add(obstacle);


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


            if (!Multiplayer)
            {
                if ((Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.W)) && playerTop > 0)

                    playerTop -= 5;

                if ((Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.S)) && playerTop < gameCanvas.ActualHeight - player.Height)

                    playerTop += 5;

                if ((Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A)) && playerLeft > 0)

                    playerLeft -= 5;

                if ((Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D)) && playerLeft < gameCanvas.ActualWidth - player.Width)

                    playerLeft += 5;
            } else
            {
                // player 1 movement

                if (Keyboard.IsKeyDown(Key.W) && playerTop > 0)

                    playerTop -= 5;

                if (Keyboard.IsKeyDown(Key.S) && playerTop < gameCanvas.ActualHeight - player.Height)

                    playerTop += 5;

                if (Keyboard.IsKeyDown(Key.A) && playerLeft > 0)

                    playerLeft -= 5;

                if ( Keyboard.IsKeyDown(Key.D) && playerLeft < gameCanvas.ActualWidth - player.Width)

                    playerLeft += 5;


                // player 2 movement

                if (Keyboard.IsKeyDown(Key.Up) && player2Top > 0) player2Top -= 5;
                if (Keyboard.IsKeyDown(Key.Down) && player2Top < gameCanvas.ActualHeight - player.Height) player2Top += 5;
                if (Keyboard.IsKeyDown(Key.Left) && player2Left > 0) player2Left -= 5; 
                if (Keyboard.IsKeyDown(Key.Right) &&  player2Left < gameCanvas.ActualWidth - player.Width) player2Left += 5;
            }


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
                    if (!Multiplayer)
                    {
                        timer.Stop();
                        ResetGame();
                    } else
                    {
                        playerLeft = 640;
                        playerTop = 585;
                        Canvas.SetLeft(player, 640);
                        Canvas.SetTop(player, 585);
                    }

                }

                if (IsCollision(player2, obstacles[i]) && Multiplayer)
                {
                    player2Left = 640;
                    player2Top = 585;
                    Canvas.SetLeft(player2, 640);
                    Canvas.SetTop(player2, 585);
                }

            }



            // Check if the player has reached the finish line

            if (playerTop <= finishLineY + finishLineHeight)

            {

                timer.Stop();
                
                level++;
                if (!Multiplayer && player1Id < 20)
                {
                    player1Id++;
                    p1Image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/src/resources/heads/Lego_hoofd" + player1Id + ".png"));
                }

                    if (level % 2 == 0 && !Multiplayer)
                {
                    obstacleSpeed+=.5;
                    obstacleCount++;
                }

                if (!Multiplayer) Level.Content = "Level: " + level;
                else { player1Score++;
                    Level.Content = "Player 1: " + player1Score + "\nPlayer 2: " + player2Score;
                }
                    
                
                ResetGame();
            }

            // player 2 checkfor finish

            if ((player2Top <= finishLineY + finishLineHeight) && Multiplayer)

            {

                timer.Stop();

                level++;

                player2Score++;
                Level.Content = "Player 1: " + player1Score + "\nPlayer 2: " + player2Score;



                ResetGame();
            }


            // pause screen

            if (Keyboard.IsKeyDown(Key.Escape))
            {
                Main.Content = new PausePage();
                timer.Stop();
            }

            if (closeWindow)
            {
                timer.Stop();
                this.Close();
                closeWindow = false;
            }


                // Update positie

                Canvas.SetTop(player, playerTop);

            Canvas.SetLeft(player, playerLeft);

            Canvas.SetTop(player2, player2Top);

            Canvas.SetLeft(player2, player2Left);

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
            objectsToKeep.Add(finishlijn1);
            objectsToKeep.Add(finishlijn2);
            objectsToKeep.Add(finishlijn3);
            objectsToKeep.Add(AchtergrondWeg);
            objectsToKeep.Add(mooigrasveldje);
            objectsToKeep.Add(player);
            objectsToKeep.Add(Level);
            objectsToKeep.Add(Main);
            if (Multiplayer) objectsToKeep.Add(player2);
            gameCanvas.Children.Clear();
            foreach (UIElement obj in objectsToKeep)
            {
                gameCanvas.Children.Add(obj);
            }


            Canvas.SetLeft(player, 640);

            Canvas.SetTop(player, 585);

            if (Multiplayer)
            {
                player2Left = 640;
                player2Top = 585;
                Canvas.SetLeft(player2, 640);
                Canvas.SetTop(player2, 585);
            }

            InitializeObstacles();





        }

    }

}
