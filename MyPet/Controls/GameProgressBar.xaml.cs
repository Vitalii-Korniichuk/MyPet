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

namespace MyPet.Controls
{
    public partial class GameProgressBar : UserControl
    {
        public GameProgressBar()
        {
            InitializeComponent();
        }
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(GameProgressBar), new PropertyMetadata(100, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GameProgressBar bar = d as GameProgressBar;
            bar.OnValueChanged(e);
        }
        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            BarProgress.Value = (int)e.NewValue;
        }
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(GameProgressBar), new PropertyMetadata(100, OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GameProgressBar bar = d as GameProgressBar;
            bar.OnMaxValueChanged(e);
        }
        private void OnMaxValueChanged(DependencyPropertyChangedEventArgs e)
        {
            BarProgress.Maximum = (int)e.NewValue;
        }
        public SolidColorBrush BarColor
        {
            get { return (SolidColorBrush)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }
        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register("BarColor", typeof(SolidColorBrush), typeof(GameProgressBar), new PropertyMetadata(null, OnBarColorChanged));

        private static void OnBarColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GameProgressBar bar = d as GameProgressBar;
            bar.OnBarColorChanged(e);
        }
        private void OnBarColorChanged(DependencyPropertyChangedEventArgs e)
        {
            BarProgress.Foreground = (SolidColorBrush)e.NewValue;
        }
        public ImageSource BarImageSource
        {
            get { return (ImageSource)GetValue(BarImageSourceProperty); }
            set { SetValue(BarImageSourceProperty, value); }
        }
        public static readonly DependencyProperty BarImageSourceProperty =
            DependencyProperty.Register("BarImageSource", typeof(ImageSource), typeof(GameProgressBar), new PropertyMetadata(null, OnBarImageSourceChanged));

        private static void OnBarImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GameProgressBar bar = d as GameProgressBar;
            bar.OnBarImageSourceChanged(e);
        }
        private void OnBarImageSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            BarImage.Source = (ImageSource)e.NewValue;
        }
    }
}
