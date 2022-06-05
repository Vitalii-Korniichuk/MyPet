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
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            GetSettings();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MusicVolume = Convert.ToInt32(MusicVolumeSlider.Value);
            Properties.Settings.Default.EffectVolume = Convert.ToInt32(EffectVolumeSlider.Value);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GetSettings();
        }
        public void GetSettings()
        {
            MusicVolumeSlider.Value = Convert.ToDouble(Properties.Settings.Default.MusicVolume);
            EffectVolumeSlider.Value = Convert.ToDouble(Properties.Settings.Default.EffectVolume);
        }
    }
}
