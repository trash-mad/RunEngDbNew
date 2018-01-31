using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// <summary>
    /// Логика взаимодействия для ImageControl.xaml
    /// </summary>
    public partial class ImageControl : UserControl, INotifyPropertyChanged
    {
        public string Source
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Source")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty ImageProperty =
           DependencyProperty.Register("Source", typeof(string), typeof(ImageControl), new FrameworkPropertyMetadata()
           {
               BindsTwoWayByDefault = true,
               PropertyChangedCallback = (o, e) =>
               {
                   //Лямбда, вызываемая как статическая функция для обновления данных в объекте
                   ImageControl that = o as ImageControl;
                   that.Redraw();
               }
           });

        //Метод для перерисовки
        private void Redraw()
        {
            if (!string.IsNullOrWhiteSpace(Source))
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(Source));
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                LoadedImage.Source = biImg as ImageSource;
                ImageExpander.Header = "Просмотр";
            }
            else
            {
                LoadedImage.Source = new BitmapImage(new Uri("pack://application:,,,/DbServerManagementStudio;component/Images/NA.png"));
                ImageExpander.Header = "Не задано";
            }
        }

        public ImageControl()
        {
            InitializeComponent();
            Redraw();
        }

        private void ChooseImage(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.Filter = "Изображения (*.png)|*.png|Все файлы (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            System.Windows.Forms.DialogResult r = System.Windows.Forms.DialogResult.Cancel;
            App.Current.Dispatcher.Invoke(() => r = openFileDialog1.ShowDialog());

            if (r == System.Windows.Forms.DialogResult.OK)
            {
                Source=Convert.ToBase64String(File.ReadAllBytes(openFileDialog1.FileName));
            }
        }
    }
}
