using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using DbElems;

namespace DbWindows
{
    public partial class LinksWindow : Window
    {
        ObservableCollection<Link> Links = new ObservableCollection<Link>();
        Item CurrentItem = null;

        public LinksWindow(int id)
        {
            InitializeComponent();

            CurrentItem = App.DataBase.Set<Item>().Find(id);
            App.DataBase.Entry(CurrentItem).Collection(x => x.Links).Load();

            foreach (var link in CurrentItem.Links)
            {
                Links.Add(link);
                Links.Last().PropertyChanged += LinksDataChanged;
            }
            Links.CollectionChanged += LinksChanged;

            MainGrid.ItemsSource = Links;
        }

        private async void LinksDataChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await App.TrySaveChanges();
        }

        private async void LinksChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool needsave = false;
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    CurrentItem.Links.Add(item as Link);
                    (item as Link).PropertyChanged += LinksDataChanged;
                }
                needsave = true;
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    CurrentItem.Links.Remove(item as Link);
                    (item as Link).PropertyChanged -= LinksDataChanged;
                }
                needsave = true;
            }
            if (needsave) await App.TrySaveChanges();
        }
    }
}
