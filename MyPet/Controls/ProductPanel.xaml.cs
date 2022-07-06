using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// Interaction logic for ProductPanel.xaml
    /// </summary>
    public partial class ProductPanel : UserControl
    {
        public ProductPanel()
        {
            InitializeComponent();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            int value = GetBoxContent();
            value++;
            SetBoxContent(value);
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            int value = GetBoxContent();
            if (value > 0)
            {
                value--;
                SetBoxContent(value);
            }
        }
       
        private int GetBoxContent()
        {
            int content;
            if(int.TryParse(NumberBox.Text, out content))
            {
                return content;
            }
            else
            {
                return 0;
            }
        }
        private void SetBoxContent(int content)
        {
            NumberBox.Text = String.Format(" {0} ", content);
        }

        private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }

        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductName", typeof(string), typeof(ProductPanel), new PropertyMetadata("Product", OnProductNameChanged));

        private static void OnProductNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProductPanel panel = d as ProductPanel;
            panel.OnProductNameChanged(e);
        }
        private void OnProductNameChanged(DependencyPropertyChangedEventArgs e)
        {
            ProductLabel.Content = (string)e.NewValue;
        }

        public int ProductNumber
        {
            get { int value = GetBoxContent();
                  return value; }
        }
    }
}
