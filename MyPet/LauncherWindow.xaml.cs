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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyPet
{
    public partial class LauncherWindow : Window
    {
        readonly GamesPage games = new GamesPage();
        readonly GalleryPage gallery = new GalleryPage();
        readonly SettingsPage settings = new SettingsPage();
        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void GamesButton_Checked(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(games);
        }

        private void GalleryButton_Checked(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(gallery);
        }

        private void SettingsButton_Checked(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(settings);
            settings.GetSettings();
        }
    }
}
