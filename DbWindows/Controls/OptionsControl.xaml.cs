using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

    public partial class OptionsControl : UserControl
    {
        public int Id
        {
            get { return (int)GetValue(LinksIdProperty); }
            set { SetValue(LinksIdProperty, value); }
        }

        public static DependencyProperty LinksIdProperty =
           DependencyProperty.Register("Id", typeof(int), typeof(OptionsControl), new FrameworkPropertyMetadata() { });

        public OptionsControl()
        {
            InitializeComponent();
        }

        private void ShowOptions(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                Interaction.MsgBox("Сначала укажите название!");
                return;
            }
            OptionsWindow ow = new OptionsWindow(Id);
            ow.ShowDialog();
        }
    }
}
