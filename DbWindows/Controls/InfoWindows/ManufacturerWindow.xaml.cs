using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для NewManufacturerWindow.xaml
    /// </summary>
    public partial class ManufacturerWindow : Window
    {
        public DbElems.Manufacturer Result { get; private set; }

        public ManufacturerWindow()
        {
            Result = new DbElems.Manufacturer();
            InitializeComponent();
        }

        public ManufacturerWindow(DbElems.Manufacturer man)
        {
            Result = man;
            InitializeComponent();
        }
    }
}
