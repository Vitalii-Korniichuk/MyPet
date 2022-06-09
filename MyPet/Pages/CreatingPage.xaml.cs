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
    public partial class CreatingPage : Page
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
                pet.Created = DateTime.Now.Date;
                pet.Visited = DateTime.Now.Date;
                entities.Pets.Add(pet);
                entities.SaveChanges();
                GameWindow game = new GameWindow();
                game.Show();
                LauncherWindow launcher = (LauncherWindow)Window.GetWindow(this);
                launcher.Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LauncherWindow launcher = (LauncherWindow)Window.GetWindow(this);
            launcher.PageFrame.Navigate(new GamesPage());
        }
        private string GetPetType()
        {
            if (CowboyType.IsSelected) { return "Cowboy"; }
            else if (NerdType.IsSelected) { return "Nerd"; }
            else if (RapperType.IsSelected) { return "Rapper"; }
            else { return "Basic"; }
        }
        private string GetTypeImageSource()
        {
            if (CowboyType.IsSelected) { return "/MyPet;component/Source/Images/Types/cowboy.png"; }
            else if (NerdType.IsSelected) { return "/MyPet;component/Source/Images/Types/nerd.png"; }
            else if (RapperType.IsSelected) { return "/MyPet;component/Source/Images/Types/rapper.png"; }
            else { return "/MyPet;component/Source/Images/Types/basic.png"; }
        }
    }
}
