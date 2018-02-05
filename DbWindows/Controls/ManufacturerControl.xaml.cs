using DbElems;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для ManufacturerControl.xaml
    /// </summary>
    public partial class ManufacturerControl : UserControl
    {
        public Manufacturer Current
        {
            get { return (Manufacturer)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        public static DependencyProperty CurrentProperty =
           DependencyProperty.Register("Current", typeof(Manufacturer), typeof(ManufacturerControl), new FrameworkPropertyMetadata()
           {
               BindsTwoWayByDefault = true,
               PropertyChangedCallback = (o, e) =>
               {
                   //Лямбда, вызываемая как статическая функция для обновления данных в объекте
                   //ImageControl that = o as ImageControl;
                   //that.Redraw();
               }
           });

        private static ObservableCollection<Manufacturer> Mans=null;
        private static async void MansRefresh(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool needsave = false;
            if (e.OldItems != null)
            {
                foreach(var item in e.OldItems)
                {
                    App.DataBase.Manufacturers.Remove(item as Manufacturer);
                }
                needsave = true;
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    App.DataBase.Manufacturers.Add(item as Manufacturer);
                }
                needsave = true;
            }
            if(needsave)await App.TrySaveChanges();
        }

        public ManufacturerControl()
        {
            if (Mans == null)
            {
                Mans = new ObservableCollection<Manufacturer>();
                foreach(var item in App.DataBase.Manufacturers)
                {
                    Mans.Add(item);
                }
                Mans.CollectionChanged += MansRefresh;
            }
            InitializeComponent();
            MainComboBox.ItemsSource = Mans;
        }


        private async void AddManClick(object sender, RoutedEventArgs e)
        {
            ManufacturerWindow w = new ManufacturerWindow();
            w.ShowDialog();
            if (w.Result != null)
            {
                Mans.Add(w.Result);
                Current = w.Result;
                await App.TrySaveChanges();
            }
        }

        private void EditMan(object sender, RoutedEventArgs e)
        {
            var man = MainComboBox.SelectedItem as Manufacturer;
            if (man == null)
            {
                Interaction.MsgBox("Сначала выберите производителя!");
                return;
            }
            ManufacturerWindow nmw = new ManufacturerWindow(man);
            nmw.ShowDialog();
        }

        private void ComboBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainComboBox.SelectedItem == null)
            {
                MainComboBox.Foreground = Brushes.Red;
            }
            else
            {
                MainComboBox.Foreground = Brushes.Black;
            }
        }
    }
}
