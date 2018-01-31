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

namespace DbServerManagementStudio
{
    public partial class ItemsControl : UserControl
    {
        ObservableCollection<Item> Items = new ObservableCollection<Item>();
        Project SharedProject = null;


        private async void SaveItemChanges()
        {
            Console.WriteLine("Элемент изменен!");
            await App.TrySaveChanges();
        }

        private async void SaveCollectionChanges(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    SharedProject.Items.Add(item as Item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    SharedProject.Items.Remove(item as Item);
                }
            }

            await App.TrySaveChanges();
        }

        public ItemsControl()
        {
            //Подгрузка списка из базы данных
            App.DataBase.Configuration.LazyLoadingEnabled = false;
            SharedProject =App.DataBase.Projects.Where(p => p.GUID.ToString() == Project.SharedProjectReservedGuid).Single(); //Публичный проект, именно его синхронизируют хосты
            App.DataBase.Entry(SharedProject).Collection(x => x.Items).Load();


            Console.WriteLine(string.Format("Получение записей из {0}. Всего записей: {1}", SharedProject.Name, SharedProject.Items.Count));
            foreach (var item in SharedProject.Items.Where(p => true))
            {
                Items.Add(item);
                Items.Last().PropertyChanged += (o,e)=>SaveItemChanges();
            }
            Items.CollectionChanged += SaveCollectionChanges;
            InitializeComponent();
            ItemGrid.ItemsSource = Items;
        }


    }
}
