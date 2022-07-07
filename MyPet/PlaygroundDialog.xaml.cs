using MyPet.Models;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyPet
{
    /// <summary>
    /// Interaction logic for PlaygroundDialog.xaml
    /// </summary>
    public partial class PlaygroundDialog : Window
    {
        int score = 0;
        int currentBags = 0;
        double bagSpeed = 5;
        Pet mainPet;
        PetDatabaseEntities entities = new PetDatabaseEntities();
        Random random = new Random();
        DispatcherTimer timer = new DispatcherTimer();

        Rect petHitbox;
        List<Rectangle> bagsToRemove = new List<Rectangle>();

        readonly ImageSource glassesSource = new BitmapImage(new Uri(@"/Source/Images/Glasses/glasses.png", UriKind.Relative));
        readonly ImageSource sunglassesSource = new BitmapImage(new Uri(@"/Source/Images/Glasses/sunglasses.png", UriKind.Relative));
        public PlaygroundDialog(int id)
        {
            InitializeComponent();
            mainPet = entities.Pets.Find(id);
            RecordLabel.Content = String.Format("Record: {0}", mainPet.Record);
            SetGlasses();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(20);
        }
        private void SetGlasses()
        {
            switch (mainPet.Type)
            {
                case "Nerd":
                    GlassesImage.Source = glassesSource;
                    break;
                case "Rapper":
                    GlassesImage.Source = sunglassesSource;
                    break;
                default: break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RecordLabel.Content = String.Format("Score: {0}", score);
            bagSpeed += 0.01;
            if (currentBags < 3)
            {
                CreateBag();
                currentBags++;
            }
            foreach(Rectangle x in MainCanvas.Children.OfType<Rectangle>())
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + bagSpeed);

                if (Canvas.GetTop(x) > 425)
                {
                    timer.Stop();
                    mainPet.Money += score * 10;
                    mainPet.Boredom += score * 50;
                    if (score > mainPet.Record)
                    {
                        mainPet.Record = score;
                    }
                    entities.SaveChanges();
                    ResetGame();
                    break;
                }

                petHitbox = new Rect(Canvas.GetLeft(PetImage), Canvas.GetTop(PetImage), PetImage.Width, PetImage.Height);
                Rect bagHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if (petHitbox.IntersectsWith(bagHitbox))
                {
                    bagsToRemove.Add(x);
                    currentBags--;
                    score++;
                }
            }
            foreach(Rectangle i in bagsToRemove)
            {
                MainCanvas.Children.Remove(i);
            }
        }

        private void ResetGame()
        {
            score = 0;
            currentBags = 0;
            bagSpeed = 5;
            MainCanvas.Visibility = Visibility.Hidden;
            MenuBox.Visibility = Visibility.Visible;
            bagsToRemove = new List<Rectangle>();
            RecordLabel.Content = String.Format("Record: {0}", mainPet.Record);
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);

            double xPosition = position.X;

            Canvas.SetLeft(PetImage, xPosition - 35);
            Canvas.SetLeft(GlassesImage, xPosition - 35);
        }
        private void CreateBag()
        {
            ImageBrush image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(@"B:\Personal\Programming\MyPet\MyPet\Source\Images\Bars\money.png", UriKind.Absolute));
            Rectangle bagImage = new Rectangle
            {
                Tag = "Drop",
                Height = 50,
                Width = 50,
                Fill = image
            };
            Canvas.SetLeft(bagImage, random.Next(50, 400));
            Canvas.SetTop(bagImage, random.Next(-400, -50));

            MainCanvas.Children.Add(bagImage);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = MainCanvas.Children.Count - 1; i >= 0; i += -1)
            {
                UIElement x = MainCanvas.Children[i];
                if (x is Rectangle)
                    MainCanvas.Children.Remove(x);
            }
            MenuBox.Visibility = Visibility.Hidden;
            MainCanvas.Visibility = Visibility.Visible;
            MainCanvas.Focus();
            timer.Start();
        }
    }
}
