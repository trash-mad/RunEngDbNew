using DbElems;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для ImagesWindow.xaml
    /// </summary>
    public partial class ImagesWindow : Window
    {
        ObservableCollection<ItemImage> Images = new ObservableCollection<ItemImage>();

        Item CurrentItem = null;

        public ImagesWindow(int id)
        {
            InitializeComponent();

            //TODO
            var items = from p in App.DataBase.Projects select p.Items;
            
            foreach(var col in items)
            {
                foreach(var item in col)
                {
                    if (item.Id == id) CurrentItem = item;
                }
            }
            
            if (CurrentItem == null) throw new Exception("Не удалось найти элемент!");

            App.DataBase.Entry(CurrentItem).Collection(x => x.Images).Load();

            foreach (var image in CurrentItem.Images)
            {
                Images.Add(image);
            }
            Images.CollectionChanged += ImagesChanged;

            ImageList.ItemsSource = Images;
        }

        private async void ImagesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool needsave = false;
            if (e.NewItems!=null)
            {
                foreach(var item in e.NewItems)
                {
                    CurrentItem.Images.Add(item as ItemImage);
                }
                needsave = true;
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    CurrentItem.Images.Remove(item as ItemImage);
                }
                needsave = true;
            }
            if (needsave) await App.TrySaveChanges();
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Изображения (*.png)|*.png|Все файлы (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ShowDialog();

            if (!string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                Images.Add(new ItemImage() { Value = File.ReadAllBytes(openFileDialog1.FileName) });
                Interaction.MsgBox("Добавлено!");
            }
        }

        private void RemoveImage(object sender, RoutedEventArgs e)
        {
            var current = ImageList.SelectedItem;
            if (current == null)
            {
                Interaction.MsgBox("Сначала выберите картинку!");
                return;
            }
            Images.Remove(current as ItemImage);
        }
    }
}
