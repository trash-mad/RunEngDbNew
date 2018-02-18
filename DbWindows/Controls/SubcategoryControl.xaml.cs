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
    public partial class SubcategoryControl : UserControl
    {
        public string Category
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category")); }
        }

        public string Subcategory
        {
            get { return (string)GetValue(SubcategoryProperty); }
            set { SetValue(SubcategoryProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Subcategory")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty CategoryProperty =
           DependencyProperty.Register("Category", typeof(string), typeof(SubcategoryControl), new FrameworkPropertyMetadata()
           {
               PropertyChangedCallback = (o, e) =>
               {
                   //Обновить список подкатегорий для предсказания
                   SubcategoryControl that = o as SubcategoryControl;
                   that.UpdateSubcategories();
               }
           });

        public static DependencyProperty SubcategoryProperty =
           DependencyProperty.Register("Subcategory", typeof(string), typeof(SubcategoryControl), new FrameworkPropertyMetadata()
           {
               BindsTwoWayByDefault = true
           });

        ObservableCollection<string> Subcategories = new ObservableCollection<string>();
        public void UpdateSubcategories()
        {
            Subcategories.Clear();
            if (string.IsNullOrWhiteSpace(Category))
            {
                MainComboBox.Text = "";
            }
            else
            {
                var list = App.DataBase.Set<Item>().Where(p => p.Category == Category).OrderBy(p => p.Subcategory).Select(g => new { Name = g.Subcategory }).Distinct();
                foreach (var item in list)
                {
                    Subcategories.Add(item.Name);
                }

                var defaultlist = DefaultCategory.GetDefaultCategories().Where(p => p.Category == Category).OrderBy(p => p.Subcategory).Select(g => new { Name = g.Subcategory }).Distinct();
                foreach (var item in defaultlist)
                {
                    if (!Subcategories.Contains(item.Name)) Subcategories.Add(item.Name);
                }
            }
        }


        public SubcategoryControl()
        {
            InitializeComponent();
            MainComboBox.ItemsSource = Subcategories;
        }
    }
}
