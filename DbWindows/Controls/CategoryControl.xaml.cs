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
using DbElems;

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public string Category
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty CategoryProperty =
           DependencyProperty.Register("Category", typeof(string), typeof(CategoryControl), new FrameworkPropertyMetadata()
           {
               BindsTwoWayByDefault = true
           });

        private static ObservableCollection<string> Categories = null;


        public CategoryControl()
        {
            InitializeComponent();

            //Если пусто - заполнить категории из базы данных
            if (Categories == null)
            {
                Categories = new ObservableCollection<string>();
                var list = App.DataBase.Set<Item>().OrderBy(p => p.Category).Select(g => new { Name = g.Category}).Distinct();
                foreach(var item in list)
                {
                    Categories.Add(item.Name);
                }

                var defaultlist = DefaultCategory.GetDefaultCategories().OrderBy(p => p.Category).Select(g => new { Name = g.Category }).Distinct();
                foreach(var item in defaultlist)
                {
                    if (!Categories.Contains(item.Name)) Categories.Add(item.Name);
                }
            }

            MainComboBox.ItemsSource = Categories;
            MainComboBox.LostFocus += UpdateList;
        }

        //Добавить элемент в автопредложенные после ввода
        private void UpdateList(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MainComboBox.Text))
            {
                if (!Categories.Contains(MainComboBox.Text)) Categories.Add(MainComboBox.Text);
            }
        }
    }
}
