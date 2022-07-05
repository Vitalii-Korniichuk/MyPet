using MyPet.Controls;
using MyPet.Models;
using MyPet.Pages;
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
    public partial class GameWindow : Window
    {
        Pet mainPet;
        DispatcherTimer timer = new DispatcherTimer();
        PetDatabaseEntities entities = new PetDatabaseEntities();

        const int hungerMaxValue = 69120;
        const int thirstMaxValue = 25920;
        const int exhaustionMaxValue = 95040;
        const int boredomMaxValue = 25920;

        int happinessLevel = 100;

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
            mainPet = entities.Pets.Find(id);
            InitializePet();
            HallButton.IsChecked=true;
            if (mainPet.Type != "Dead")
            {
                LoadNewValues();
                FindAge();
                timer.Tick += TimerTick;
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
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
                mainPet.Hunger--;
                mainPet.Thirst--;
                mainPet.Exhaustion--;
                mainPet.Boredom--;
                SetPetMood();
            }
            if (mainPet.IsSleeping)
            {
                mainPet.Exhaustion += 3;
            }
            RefreshBars();
        }

        private void SetPetMood()
        {
            SetHappinessLevel();

            if (happinessLevel >= 60) { PetImage.Source = bigSmileSource; }
            else if (happinessLevel >= 30) { PetImage.Source = noSmileSource; }
            else { PetImage.Source = sadSource; }
        }

        private void SetHappinessLevel()
        {
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
            HallPanel.Visibility = Visibility.Visible;

            KitchenPanel.Visibility = Visibility.Hidden;
            BedroomPanel.Visibility = Visibility.Hidden;
        }

        private void KitchenButton_Checked(object sender, RoutedEventArgs e)
        {
            SavePet();
            InitializeFridge();
            KitchenPanel.Visibility = Visibility.Visible;

            HallPanel.Visibility = Visibility.Hidden;
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

            HallPanel.Visibility = Visibility.Hidden;
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
                mainPet.IsSleeping = true;
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
            BedroomButton.IsChecked = true;
            PetImage.Source = sleepSource;
        }

        private void CameraImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        public void SavePet()
        {
            mainPet.Visited = DateTime.Now;
            entities.SaveChanges();
        }

        private void PlaygroundButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SavePet();
            LauncherWindow launcher = new LauncherWindow();
            launcher.PageFrame.Navigate(new GamesPage());
            launcher.Show();
        }
    }
}
