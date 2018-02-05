using DbElems;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для ImagesControl.xaml
    /// </summary>
    public partial class ImagesControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {

        public int Id
        {
            get { return (int)GetValue(ImageIdProperty); }
            set { SetValue(ImageIdProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id")); }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty ImageIdProperty =
           DependencyProperty.Register("Id", typeof(int), typeof(ImagesControl), new FrameworkPropertyMetadata(){});

        public ImagesControl()
        {
            InitializeComponent();
        }

        private void ShowImages(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                Interaction.MsgBox("Сначала укажите название!");
                return;
            }
            ImagesWindow iw = new ImagesWindow(Id);
            iw.ShowDialog();
        }
    }
}
