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

        readonly HomePage home = new HomePage();
        readonly KitchenPage kitchen = new KitchenPage();
        readonly BedroomPage bedroom = new BedroomPage();
        readonly GameroomPage gameroom = new GameroomPage();
        readonly ShopPage shop = new ShopPage();

        const int hungerMaxValue = 69120;
        const int thirstMaxValue = 25920;
        const int exhaustionMaxValue = 95040;
        const int boredomMaxValue = 25920;

        readonly ImageSource bigSmileSource = new BitmapImage(new Uri(@"/Source/Images/Pets/big_smile.png", UriKind.Relative));
        readonly ImageSource noSmileSource = new BitmapImage(new Uri(@"/Source/Images/Pets/no_smile.png", UriKind.Relative));
        readonly ImageSource openMouthSource = new BitmapImage(new Uri(@"/Source/Images/Pets/open_mouth.png", UriKind.Relative));
        readonly ImageSource sadSource = new BitmapImage(new Uri(@"/Source/Images/Pets/sad.png", UriKind.Relative));

        readonly ImageSource glassesSource = new BitmapImage(new Uri(@"/Source/Images/Glasses/glasses.png", UriKind.Relative));
        readonly ImageSource sunglassesSource = new BitmapImage(new Uri(@"/Source/Images/Glasses/sunglasses.png", UriKind.Relative));
        public GameWindow(int id)
        {
            InitializeComponent();
            InitializingSettings();
            mainPet = entities.Pets.Find(id);
            InitializePet();
            timer.Tick += TimerTick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void InitializePet()
        {
            NameLabel.Content = mainPet.Name;
            MoneyLabel.Content = mainPet.Money;
            FindAge();
            LoadNewValues();
            SetPetMood();
            SetGlasses();
            RefreshBars();
            mainPet.Visited = DateTime.Now;
        }

        private void SetGlasses()
        {
            switch (mainPet.Type)
            {
                case "Nerd": GlassesImage.Source = glassesSource;
                             break;
                case "Rapper": GlassesImage.Source = sunglassesSource;
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
            mainPet.Hunger = mainPet.Hunger - timeDifference;
            mainPet.Thirst = mainPet.Thirst - timeDifference;
            mainPet.Exhaustion = mainPet.Exhaustion - timeDifference;
            mainPet.Boredom = mainPet.Boredom - timeDifference;
        }
        private void RefreshBars()
        {
            HungerBar.Value = mainPet.Hunger;
            ThirstBar.Value = mainPet.Thirst;
            ExhaustionBar.Value = mainPet.Exhaustion;
            BoredomBar.Value = mainPet.Boredom;
        }
        private void InitializingSettings()
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
        private void TimerTick(object sender, EventArgs e)
        {
            mainPet.Hunger--;
            mainPet.Thirst--;
            mainPet.Exhaustion--;
            mainPet.Boredom--;
            SetPetMood();
            RefreshBars();
        }

        private void SetPetMood()
        {
            if (mainPet.Hunger >= hungerMaxValue / 2) { PetImage.Source = bigSmileSource; }
            else if (mainPet.Hunger >= hungerMaxValue * 1 / 3) { PetImage.Source = noSmileSource; }
            else { PetImage.Source = sadSource; }

            if (mainPet.Thirst >= thirstMaxValue / 2) { PetImage.Source = bigSmileSource; }
            else if (mainPet.Thirst >= thirstMaxValue * 1 / 3) { PetImage.Source = noSmileSource; }
            else { PetImage.Source = sadSource; }

            if (mainPet.Exhaustion >= exhaustionMaxValue / 2) { PetImage.Source = bigSmileSource; }
            else if (mainPet.Exhaustion >= exhaustionMaxValue * 1 / 3) { PetImage.Source = noSmileSource; }
            else { PetImage.Source = sadSource; }

            if (mainPet.Boredom >= boredomMaxValue / 2) { PetImage.Source = bigSmileSource; }
            else if (mainPet.Boredom >= boredomMaxValue * 1 / 3) { PetImage.Source = noSmileSource; }
            else { PetImage.Source = sadSource; }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainPet.Visited = DateTime.Now;
            entities.SaveChanges();
            LauncherWindow launcher = new LauncherWindow();
            launcher.PageFrame.Navigate(new GamesPage());
            launcher.Show();
        }
        private void HomeButton_Checked(object sender, RoutedEventArgs e)
        {
            EnvironmentFrame.Navigate(home);
        }

        private void KitchenButton_Checked(object sender, RoutedEventArgs e)
        {
            EnvironmentFrame.Navigate(kitchen);
        }

        private void BedroomButton_Checked(object sender, RoutedEventArgs e)
        {
            EnvironmentFrame.Navigate(bedroom);
        }

        private void GameroomButton_Checked(object sender, RoutedEventArgs e)
        {
            EnvironmentFrame.Navigate(gameroom);
        }

        private void ShopButton_Checked(object sender, RoutedEventArgs e)
        {
            EnvironmentFrame.Navigate(shop);
        }
    }
}
