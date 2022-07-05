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
    }
}
