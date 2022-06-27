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

namespace MyPet.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : PageBase
    {
        public SettingsPage()
        {
            InitializeComponent();
            GetSettings();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Fullscreen = Convert.ToBoolean(FullscreenBox.IsChecked);
            Properties.Settings.Default.BorderVisibility = Convert.ToBoolean(BorderVisibilityBox.IsChecked);
            Properties.Settings.Default.Save();
            LauncherWindow launcher = (LauncherWindow)Window.GetWindow(this);
            launcher.GamesButton.IsChecked = true;
            ChangeLauncherFrame(new GamesPage());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GetSettings();
        }
        public void GetSettings()
        {
            FullscreenBox.IsChecked = Convert.ToBoolean(Properties.Settings.Default.Fullscreen);
            BorderVisibilityBox.IsChecked = Convert.ToBoolean(Properties.Settings.Default.BorderVisibility);
        }
    }
}
