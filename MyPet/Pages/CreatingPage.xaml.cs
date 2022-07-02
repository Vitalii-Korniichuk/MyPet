using MyPet.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for CreatingPage.xaml
    /// </summary>
    public partial class CreatingPage : PageBase
    {
        PetDatabaseEntities entities = new PetDatabaseEntities();
        public CreatingPage()
        {
            InitializeComponent();
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text != null)
            {
                Pet pet = new Pet();
                pet.Name = NameBox.Text;
                pet.Type = GetPetType();
                pet.TypeImageSource = GetTypeImageSource();
                pet.Created = DateTime.Now;
                pet.Visited = DateTime.Now;
                pet.Money = 100;
                pet.Hunger = 69120 / 2;
                pet.Thirst = 25920 / 2;
                pet.Exhaustion = 95040 / 2;
                pet.Boredom = 25920 / 2;
                pet.Apples = 1;
                pet.Pizzas = 0;
                pet.Juice = 1;
                pet.Coke = 0;
                entities.Pets.Add(pet);
                entities.SaveChanges();
                LoadGameWindow(pet.Id);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeLauncherFrame(new GamesPage());
        }
        private string GetPetType()
        {
            if (NerdType.IsSelected) { return "Nerd"; }
            else if (RapperType.IsSelected) { return "Rapper"; }
            else { return "Basic"; }
        }
        private string GetTypeImageSource()
        {
            if (NerdType.IsSelected) { return "/MyPet;component/Source/Images/Types/nerd.png"; }
            else if (RapperType.IsSelected) { return "/MyPet;component/Source/Images/Types/rapper.png"; }
            else { return "/MyPet;component/Source/Images/Types/basic.png"; }
        }
    }
}
