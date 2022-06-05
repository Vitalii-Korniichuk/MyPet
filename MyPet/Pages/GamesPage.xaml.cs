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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyPet.Pages
{
    public partial class GamesPage : Page
    {
        readonly PetDatabaseEntities entities = new PetDatabaseEntities();
        public GamesPage()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void PetGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Pet pet = PetGrid.SelectedItem as Pet;
            pet.Name = (PetGrid.Columns[0].GetCellContent(e.Row.Item) as TextBox).Text;
            PetGrid.IsReadOnly = true;
            RefreshGrid();
        }
        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            PetGrid.IsReadOnly = false;
            PetGrid.CurrentCell = new DataGridCellInfo(PetGrid.SelectedItem, PetGrid.Columns[0]);
            PetGrid.BeginEdit();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            entities.Pets.Remove(PetGrid.SelectedItem as Pet);
            RefreshGrid();
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (PetGrid.SelectedItem != null)
            {

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Pet pet = new Pet();
            pet.Name = "The new pet";
            pet.Created = DateTime.Now.Date;
            entities.Pets.Add(pet);
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            entities.SaveChanges();
            PetGrid.ItemsSource = entities.Pets.ToList();
        }
    }
}
