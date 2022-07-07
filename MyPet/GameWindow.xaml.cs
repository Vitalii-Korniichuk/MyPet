using MyPet.Controls;
using MyPet.Models;
using MyPet.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyPet
{
    public partial class GameWindow : Window
    {
        Pet mainPet;
        DispatcherTimer timer = new DispatcherTimer();
        PetDatabaseEntities entities = new PetDatabaseEntities();

        const int hungerMaxValue = 69120;
        const int thirstMaxValue = 25920;
        const int exhaustionMaxValue = 95040;
        const int boredomMaxValue = 25920;

        int happinessLevel;

        readonly ImageSource bigSmileSource = new BitmapImage(new Uri(@"/Source/Images/Pets/big_smile.png", UriKind.Relative));
        readonly ImageSource noSmileSource = new BitmapImage(new Uri(@"/Source/Images/Pets/no_smile.png", UriKind.Relative));
        readonly ImageSource sadSource = new BitmapImage(new Uri(@"/Source/Images/Pets/sad.png", UriKind.Relative));
        readonly ImageSource deadSource = new BitmapImage(new Uri(@"/Source/Images/Pets/dead.png", UriKind.Relative));
        readonly ImageSource sleepSource = new BitmapImage(new Uri(@"/Source/Images/Pets/sleep.png", UriKind.Relative));

        readonly ImageSource glassesSource = new BitmapImage(new Uri(@"/Source/Images/Glasses/glasses.png", UriKind.Relative));
        readonly ImageSource sunglassesSource = new BitmapImage(new Uri(@"/Source/Images/Glasses/sunglasses.png", UriKind.Relative));

        readonly SolidColorBrush greenColor = new SolidColorBrush(Colors.Green);
        readonly SolidColorBrush orangeColor = new SolidColorBrush(Colors.Orange);
        readonly SolidColorBrush redColor = new SolidColorBrush(Colors.Red);
        public GameWindow(int id)
        {
            InitializeComponent();
            InitializeSettings();
            mainPet = entities.Pets.Find(id);
            LoadNewValues();
            InitializePet();
            HallButton.IsChecked=true;
            if (mainPet.Type != "Dead") {
                FindAge();
                timer.Tick += TimerTick;
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
            }
        }

        private void InitializeSettings()
        {
            if (Properties.Settings.Default.Fullscreen)
            {
                Window.WindowState = WindowState.Maximized;
            }
            if (!Properties.Settings.Default.BorderVisibility)
            {
                Window.WindowStyle = WindowStyle.None;
            }
        }

        private void InitializePet()
        {
            NameLabel.Content = mainPet.Name;
            MoneyLabel.Content = String.Format("{0}$", mainPet.Money);
            SetPetAlive();
            if (mainPet.Type != "Dead")
            {
                if (mainPet.IsSleeping)
                {
                    Sleeping();
                }
                else
                {
                    SetPetMood();
                }
                SetGlasses();
                RefreshBars();
            }
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

        private void FindAge()
        {
            double daysDifference = Math.Floor(DateTime.Now.Subtract(mainPet.Created).TotalDays) / 36;
            int yearsDifference = (int)daysDifference; //in game years
            if (yearsDifference > 9)
            {
                AgeImage.Source = new BitmapImage(new Uri(@"/Source/Images/Bars/grand.png", UriKind.Relative));
            }
            else if (yearsDifference > 6)
            {
                AgeImage.Source = new BitmapImage(new Uri(@"/Source/Images/Bars/adult.png", UriKind.Relative));
            }
            else if (yearsDifference > 3)
            {
                AgeImage.Source = new BitmapImage(new Uri(@"/Source/Images/Bars/child.png", UriKind.Relative));
            }
            else
            {
                AgeImage.Source = new BitmapImage(new Uri(@"/Source/Images/Bars/baby.png", UriKind.Relative));
            }

        }

        private void LoadNewValues()
        {
            double datetimeDifference = Math.Floor(DateTime.Now.Subtract(mainPet.Visited).TotalSeconds);
            int timeDifference = (int)datetimeDifference / 10;
            mainPet.Hunger -= timeDifference;
            mainPet.Thirst -= timeDifference;
            if (mainPet.IsSleeping)
            {
                mainPet.Exhaustion += 3 * timeDifference;
            }
            else
            {
                mainPet.Exhaustion -= timeDifference;
            }
            mainPet.Boredom -= timeDifference;
        }
        private void RefreshBars()
        {
            HungerBar.Value = mainPet.Hunger;
            ThirstBar.Value = mainPet.Thirst;
            ExhaustionBar.Value = mainPet.Exhaustion;
            BoredomBar.Value = mainPet.Boredom;
            SetBarColor(HungerBar, hungerMaxValue);
            SetBarColor(ThirstBar, thirstMaxValue);
            SetBarColor(ExhaustionBar, exhaustionMaxValue);
            SetBarColor(BoredomBar, boredomMaxValue);
        }

        private void SetBarColor(GameProgressBar bar, int maxValue)
        {
            if (bar.Value > 2 / 3 * maxValue)
            {
                bar.BarColor = greenColor;
            }
            else if (bar.Value > 1 / 3 * maxValue)
            {
                bar.BarColor = orangeColor;
            }
            else
            {
                bar.BarColor = redColor;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (mainPet.Type != "Dead" && !mainPet.IsSleeping)
            {
                mainPet.Hunger-=3;
                mainPet.Thirst-=3;
                mainPet.Exhaustion-=3;
                mainPet.Boredom-=3;
                SetPetMood();
                SetPetAlive();
            }
            if (mainPet.IsSleeping)
            {
                mainPet.Exhaustion += 9;
                mainPet.Hunger --;
                mainPet.Thirst --;
            }
            RefreshBars();
        }

        private void SetPetMood()
        {
            SetHappinessLevel();

            if (happinessLevel >= 75)
            {
                PetImage.Source = bigSmileSource;
                GlassesImage.Margin = new Thickness(700, 710, 700, 690);
            }
            else if (happinessLevel >= 35)
            {
                PetImage.Source = noSmileSource;
                GlassesImage.Margin = new Thickness(700, 680, 700, 720);
            }
            else
            {
                PetImage.Source = sadSource;
                GlassesImage.Margin = new Thickness(700, 700, 700, 700);
            }
        }

        private void SetHappinessLevel()
        {
            double hungerPercentage =  (double)mainPet.Hunger / (double)hungerMaxValue * 100.0;
            double thirstPercentage = (double)mainPet.Thirst / (double)thirstMaxValue * 100.0;
            double exhaustionPercentage = (double)mainPet.Exhaustion / (double)exhaustionMaxValue * 100.0;
            double boredomPercentage = (double)mainPet.Boredom / (double)boredomMaxValue * 100.0;
            happinessLevel = (int)((hungerPercentage + thirstPercentage + exhaustionPercentage + boredomPercentage) / 4);
        }

        private void SetPetAlive()
        {
            if (mainPet.Hunger < 1 || mainPet.Thirst < 1 || mainPet.Exhaustion < 1 || mainPet.Boredom < 1 || mainPet.Type == "Dead")
            {
                mainPet.Type = "Dead";
                mainPet.TypeImageSource = "/MyPet;component/Source/Images/Types/skull.png";
                PetImage.Source = deadSource;
            }
        }
        private void HallButton_Checked(object sender, RoutedEventArgs e)
        {
            SavePet();

            KitchenPanel.Visibility = Visibility.Hidden;
            BedroomPanel.Visibility = Visibility.Hidden;
        }

        private void KitchenButton_Checked(object sender, RoutedEventArgs e)
        {
            SavePet();
            InitializeFridge();
            KitchenPanel.Visibility = Visibility.Visible;

            BedroomPanel.Visibility = Visibility.Hidden;
        }

        private void InitializeFridge()
        {
            if (mainPet.Apples > 1)
            {
                AppleLabel.Content = String.Format("{0} apples", mainPet.Apples);
            }
            else
            {
                AppleLabel.Content = String.Format("{0} apple", mainPet.Apples);
            }
            if (mainPet.Pizzas > 1)
            {
                PizzaLabel.Content = String.Format("{0} pizzas", mainPet.Pizzas);
            }
            else
            {
                PizzaLabel.Content = String.Format("{0} pizza", mainPet.Pizzas);
            }
            JuiceLabel.Content = String.Format("{0} juice", mainPet.Juice);
            CokeLabel.Content = String.Format("{0} coke", mainPet.Coke);
        }

        private void BedroomButton_Checked(object sender, RoutedEventArgs e)
        {
            SavePet();
            BedroomPanel.Visibility = Visibility.Visible;

            KitchenPanel.Visibility = Visibility.Hidden;

        }

        private void AppleImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mainPet.Apples > 0)
            {
                mainPet.Hunger += hungerMaxValue / 5;
                mainPet.Apples--;
                DinnerWakeUp();
            }
        }

        private void PizzaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mainPet.Pizzas > 0)
            {
                mainPet.Hunger += hungerMaxValue / 5;
                mainPet.Pizzas--;
                DinnerWakeUp();
            }
        }

        private void JuiceImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mainPet.Juice > 0)
            {
                mainPet.Thirst += thirstMaxValue / 5;
                mainPet.Juice--;
                DinnerWakeUp();
            }
        }

        private void CokeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mainPet.Coke > 0)
            {
                mainPet.Thirst += thirstMaxValue / 3;
                mainPet.Exhaustion += exhaustionMaxValue / 8;
                mainPet.Coke--;
                DinnerWakeUp();
            }
        }
        private void DinnerWakeUp()
        {
            mainPet.IsSleeping = false;
            InitializeFridge();
        }
        private void BedImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!mainPet.IsSleeping)
            {
                Sleeping();
            }
            else
            {
                mainPet.IsSleeping = false;
                if (mainPet.Type != "Dead")
                {
                    SetPetMood();
                }
            }
        }

        private void Sleeping()
        {
            mainPet.IsSleeping = true;
            PetImage.Source = sleepSource;
        }
        public void SavePet()
        {
            mainPet.Visited = DateTime.Now;
            entities.SaveChanges();
        }

        private void PlaygroundButton_Click(object sender, RoutedEventArgs e)
        {
            SavePet();
            PlaygroundDialog playground = new PlaygroundDialog(mainPet.Id);
            this.Hide();
            playground.ShowDialog();
            this.Show();
            entities = new PetDatabaseEntities();
            mainPet = entities.Pets.Find(mainPet.Id);
            InitializePet();
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            SavePet();
            ShopDialog shop = new ShopDialog(mainPet.Id);
            bool? dialogResult = shop.ShowDialog();
            this.Hide();
            if (dialogResult == true)
            {
                entities = new PetDatabaseEntities();
                mainPet = entities.Pets.Find(mainPet.Id);
                InitializePet();
                InitializeFridge();
            }
            this.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SavePet();
            LauncherWindow launcher = new LauncherWindow();
            launcher.PageFrame.Navigate(new GamesPage());
            launcher.Show();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
