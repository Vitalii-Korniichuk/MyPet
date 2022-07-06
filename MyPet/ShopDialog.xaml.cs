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

namespace MyPet
{
    /// <summary>
    /// Interaction logic for ShopDialog.xaml
    /// </summary>
    public partial class ShopDialog : Window
    {
        Pet mainPet;
        PetDatabaseEntities entities = new PetDatabaseEntities();
        public ShopDialog(int id)
        {
            InitializeComponent();
            mainPet = entities.Pets.Find(id);
            InitializeStorage();
        }
        private void InitializeStorage()
        {
            MoneyLabel.Content = String.Format("{0}$", mainPet.Money);
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

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            BuyProducts();
            entities.SaveChangesAsync();

            DialogResult = true;
            this.Close();
        }

        private void BuyProducts()
        {
            if (AddWholeExpenses() <= mainPet.Money)
            {
                mainPet.Money -= AddWholeExpenses();
                FillStorage();
            }
        }

        private void FillStorage()
        {
            mainPet.Apples += ApplePanel.ProductNumber;
            mainPet.Pizzas += PizzaPanel.ProductNumber;
            mainPet.Juice += JuicePanel.ProductNumber;
            mainPet.Coke += CokePanel.ProductNumber;
        }

        private int AddWholeExpenses()
        {
            int sum = 0;
            sum += ApplePanel.ProductNumber * 3;
            sum += PizzaPanel.ProductNumber * 5;
            sum += JuicePanel.ProductNumber * 2;
            sum += CokePanel.ProductNumber * 3;
            return sum;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            entities.SaveChangesAsync();

            DialogResult = false;
            this.Close();
        }

        private void ApplePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSumPrice();
        }

        private void PizzaPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSumPrice();
        }

        private void JuicePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSumPrice();
        }

        private void CokePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSumPrice();
        }
        private void SetSumPrice()
        {
            int sum = AddWholeExpenses();
            SumLabel.Content = String.Format("{0}$", sum);
        }

        private void ApplePanel_MouseMove(object sender, MouseEventArgs e)
        {
            SetSumPrice();
        }

        private void PizzaPanel_MouseMove(object sender, MouseEventArgs e)
        {
            SetSumPrice();
        }

        private void JuicePanel_MouseMove(object sender, MouseEventArgs e)
        {
            SetSumPrice();
        }

        private void CokePanel_MouseMove(object sender, MouseEventArgs e)
        {
            SetSumPrice();
        }
    }
}
