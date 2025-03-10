using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Pexeso2D
{
    public partial class MainWindow : Window
    {
        private const int GridSize = 6; // hrací pole
        private List<string> imagePaths;
        private Button firstCard = null, secondCard = null;
        private int score = 0, matchedPairs = 0, seconds = 0;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            // Nastavení časovače
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Načtení obrázků
            imagePaths = new List<string>
            {
                "Images/belasek.jpg",
                "Images/ruka.jpg",
                "Images/motyl.jpg",
                "Images/kost.jpg",
                "Images/phodiny.jpg",
                "Images/kupa.jpg",
                "Images/lilie.jpg",
                "Images/mesic.jpg"
            };

            // Zdvojíme obrázky a zamícháme je
            imagePaths = imagePaths.Concat(imagePaths).OrderBy(x => Guid.NewGuid()).ToList();

            // Vytvoření hrací plochy
            GameGrid.Children.Clear();
            foreach (string imagePath in imagePaths)
            {
                Button btn = new Button
                {
                    Tag = imagePath,
                    Background = Brushes.Gray,
                    Content = CreateHiddenCard() // Skrytá karta (vizuálně otočená)
                };
                btn.Click += Card_Click;
                GameGrid.Children.Add(btn);
            }

            // Reset skóre a času
            score = 0;
            seconds = 0;
            matchedPairs = 0;
            UpdateScore();
            UpdateTime();
        }

        private UIElement CreateHiddenCard()
        {
            return new TextBlock
            {
                Text = "o", // Místo obrázku bude na začátku otazník
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            UpdateTime();
        }

        private void UpdateScore()
        {
            ScoreText.Text = $"Skóre: {score}";
        }

        private void UpdateTime()
        {
            TimeText.Text = $"Čas: {seconds}s";
        }

        private async void Card_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null || clickedButton == firstCard || secondCard != null)
                return;

            // Odkryjeme kartu s animací otočení
            string imagePath = clickedButton.Tag.ToString();
            FlipCard(clickedButton, imagePath);

            if (firstCard == null)
            {
                firstCard = clickedButton;
                return;
            }

            secondCard = clickedButton;

            // Kontrola shody
            if (firstCard.Tag.ToString() == secondCard.Tag.ToString())
            {
                score += 10;
                matchedPairs++;
                firstCard.IsEnabled = false;
                secondCard.IsEnabled = false;
                firstCard = null;
                secondCard = null;

                // Kontrola výhry
                if (matchedPairs == GridSize * GridSize / 2)
                {
                    // Zastavení časovače
                    timer.Stop();

                    // Zobrazení vítězství
                    MessageBox.Show($"Vyhrál(a) jsi! Čas: {seconds}s | Skóre: {score}");

                    // Restartování hry po krátkém zpoždění (aby hráč mohl vidět výsledek)
                    await Task.Delay(1000);
                    StartGame(); // Restartování hry
                }
            }
            else
            {
                score -= 5;
                await Task.Delay(1000);
                FlipCard(firstCard, null); // Otočí zpět
                FlipCard(secondCard, null); // Otočí zpět
                firstCard = null;
                secondCard = null;
            }

            UpdateScore();
        }


        private void FlipCard(Button button, string imagePath)
        {
            DoubleAnimation flipAnimation = new DoubleAnimation(0, 360, TimeSpan.FromMilliseconds(300));
            flipAnimation.AutoReverse = false;

            RotateTransform rotate = new RotateTransform();
            button.RenderTransform = rotate;
            button.RenderTransformOrigin = new Point(0.5, 0.5);

            flipAnimation.Completed += (s, e) =>
            {
                if (!string.IsNullOrEmpty(imagePath)) // Pokud máme platnou cestu k obrázku
                {
                    Uri imageUri = new Uri(imagePath, UriKind.Relative); // Používáme relativní cestu
                    BitmapImage bitmap = new BitmapImage(imageUri);
                    button.Content = new Image
                    {
                        Source = bitmap,
                        Stretch = Stretch.Fill
                    };
                }
                else
                {
                    // Pokud máme obrázek vrátit zpět
                    button.Content = CreateHiddenCard();
                }
            };

            rotate.BeginAnimation(RotateTransform.AngleProperty, flipAnimation);
        }


    }
}
