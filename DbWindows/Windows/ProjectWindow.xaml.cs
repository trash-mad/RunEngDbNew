using DbElems;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        bool IsMenuOpened = false;

        public ProjectWindow(int id)
        {
            InitializeComponent();
            ProjectFrame.Content = new ItemsControl(id);
        }

        private void MenuItemClick()
        {
            if (IsMenuOpened)
            {
                Storyboard sb = Resources["sbHideLeftMenu"] as Storyboard;
                sb.Begin(SlideMenu);
            }
            else
            {
                Storyboard sb = Resources["sbShowLeftMenu"] as Storyboard;
                sb.Begin(SlideMenu);
            }

            IsMenuOpened = !IsMenuOpened;
        }

        private async void SaveAndExit()
        {
            await App.TrySaveChanges();
            App.Current.Shutdown();
        }

        private void StartSync()
        {
            SyncWindow sw = new SyncWindow();
            sw.ShowDialog();
        }
    }
}
