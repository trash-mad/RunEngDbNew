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
    public partial class OptionsWindow : Window
    {
        ObservableCollection<Option> Options = new ObservableCollection<Option>();
        Item CurrentItem = null;



        public IEnumerable<OptionType> OptionsList
        {
            get
            {
                return Enum.GetValues(typeof(OptionType)).Cast<OptionType>();
            }
        }

        public OptionsWindow(int id)
        {
            InitializeComponent();

            CurrentItem = CurrentItem = App.DataBase.Set<Item>().Find(id);
            App.DataBase.Entry(CurrentItem).Collection(x => x.Options).Load();

            foreach (var option in CurrentItem.Options)
            {
                Options.Add(option);
                Options.Last().PropertyChanged += OptionsDataChanged;
            }
            Options.CollectionChanged += OptionsChanged;

            MainGrid.ItemsSource = Options;
        }

        private async void OptionsDataChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            await App.TrySaveChanges();
        }

        private async void OptionsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool needsave = false;
            if (e.NewItems != null)
            {
                foreach (var option in e.NewItems)
                {
                    CurrentItem.Options.Add(option as Option);
                    (option as Option).PropertyChanged += OptionsDataChanged;
                }
                needsave = true;
            }
            if (e.OldItems != null)
            {
                foreach (var option in e.OldItems)
                {
                    CurrentItem.Options.Remove(option as Option);
                    (option as Option).PropertyChanged -= OptionsDataChanged;
                }
                needsave = true;
            }
            if (needsave) await App.TrySaveChanges();
        }
    }
}
