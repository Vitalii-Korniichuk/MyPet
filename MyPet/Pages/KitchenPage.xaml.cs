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
    /// Interaction logic for KitchenPage.xaml
    /// </summary>
    public partial class KitchenPage : Page
    {
        public KitchenPage(int apples, int pizzas, int juice, int coke)
        {
            InitializeComponent();
            InitializeFood(apples, pizzas, juice, coke);
        }

        private void InitializeFood(int apples, int pizzas, int juice, int coke)
        {
            if (apples > 1)
            {
                AppleLabel.Content = String.Format("{0} apples", apples);
            }
            else
            {
                AppleLabel.Content = String.Format("{0} apple", apples);
            }
            if (pizzas > 1)
            {
                PizzaLabel.Content = String.Format("{0} pizzas", pizzas);
            }
            else
            {
                PizzaLabel.Content = String.Format("{0} pizza", pizzas);
            }
            JuiceLabel.Content = String.Format("{0} juice", juice);
            CokeLabel.Content = String.Format("{0} coke", coke);
        }

        private void AppleImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PizzaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void JuiceImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CokeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
