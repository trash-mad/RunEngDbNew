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

namespace DbServerManagementStudio
{
    /// <summary>
    /// Логика взаимодействия для NewManufacturerWindow.xaml
    /// </summary>
    public partial class NewManufacturerWindow : Window
    {
        public NewManufacturerWindow()
        {
            InitializeComponent();
        }

        public DbElems.Manufacturer Result { get; set; }

        private void CreateMan(object sender, RoutedEventArgs e)
        {
            int irate;
            byte brate;

            if(!int.TryParse(ManRate.Text,out irate))
            {
                Interaction.MsgBox("Оценка это число!");
                return;
            }

            if (irate < 1 || irate > 5)
            {
                Interaction.MsgBox("Оценка может быть от одного до пяти!");
                return;
            }

            brate = Convert.ToByte(irate);

            if (string.IsNullOrWhiteSpace(ManName.Text))
            {
                Interaction.MsgBox("Имя обязательно для заполнения!");
            }

            Result = new DbElems.Manufacturer();
            Result.Name = ManName.Text;
            Result.Rate = brate;
            Result.Info = ManInfo.Text;
            Result.BitmapBase64 = ManLogo.Source;
            Close();
        }
    }
}
