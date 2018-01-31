using DbElems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbServerManagementStudio
{
    public static class ConsoleCommands
    {
        public static void AddItems()
        {
            /*OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Изображения (*.png)|*.png|Все файлы (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            DialogResult r= DialogResult.Cancel;
            App.Current.Dispatcher.Invoke(() => r = openFileDialog1.ShowDialog());

            if (r == DialogResult.OK)
            {
                //App.DataBase.Items.Add(new DbElems.Item("1", "2", "3", "4", new DbElems.Manufacturer(), Logo: File.ReadAllBytes(openFileDialog1.FileName)));
                App.TrySaveChanges();
            }*/
        }
    }
}
