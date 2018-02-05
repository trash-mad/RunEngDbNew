using DbElems;
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
    public partial class ItemsControl : UserControl
    {
        ObservableCollection<Item> Items = new ObservableCollection<Item>();
        public Project CurrentProject { get; set; }

        private async void SaveItemChanges(Object o,PropertyChangedEventArgs e)
        {
            Console.WriteLine("Элемент изменен!");
            await App.TrySaveChanges();
        }

        private async void SaveItemsChanges(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool needsave = false;
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    CurrentProject.Items.Add(item as Item);
                    (item as Item).PropertyChanged += SaveItemChanges;
                }
                needsave = true;
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    CurrentProject.Items.Remove(item as Item);
                    (item as Item).PropertyChanged -= SaveItemChanges;
                }
                needsave = true;
            }

            if(needsave)await App.TrySaveChanges();
        }

        public ItemsControl(int id)
        {
            //Подгрузка списка из базы данных
            App.DataBase.Configuration.LazyLoadingEnabled = false;
            CurrentProject = App.DataBase.Projects.Where(p => p.Id == id).Single(); //Публичный проект, именно его синхронизируют хосты
            App.DataBase.Entry(CurrentProject).Collection(x => x.Items).Load();

            Console.WriteLine(string.Format("Получение записей из {0}. Всего записей: {1}", CurrentProject.Name, CurrentProject.Items.Count));
            foreach (var item in CurrentProject.Items.Where(p => true))
            {
                Items.Add(item);
                Items.Last().PropertyChanged += SaveItemChanges;
            }
            Items.CollectionChanged += SaveItemsChanges;

            //Загрузка вложенных коллекций
            


            InitializeComponent();
            ItemGrid.ItemsSource = Items;
        }


    }
}
