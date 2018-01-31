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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbServerManagementStudio
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsMenuOpened = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        //Метод, открывающий боковое меню
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

            byte[] arr = new byte[] { 244, 233 };
            arr = new byte[] { 244, 233,111 };

        }


    }
}
