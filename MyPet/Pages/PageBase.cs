using MyPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyPet.Pages
{
    public class PageBase : Page
    {
        public void ChangeLauncherFrame(Page page)
        {
            LauncherWindow launcher = (LauncherWindow)Window.GetWindow(this);
            launcher.PageFrame.Navigate(page);
        }
        public void LoadGameWindow(int id)
        {
            GameWindow game = new GameWindow(id);
            game.Show();
            CloseLauncherWindow();
        }
        public void CloseLauncherWindow()
        {
            LauncherWindow launcher = (LauncherWindow)Window.GetWindow(this);
            launcher.Close();
        }
    }
}
