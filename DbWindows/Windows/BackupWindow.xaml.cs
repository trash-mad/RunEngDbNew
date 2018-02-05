using DbElems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для BackupWindow.xaml
    /// </summary>
    public partial class BackupWindow : Window
    {
        public BackupWindow()
        {
            InitializeComponent();
        }

        private void Backup(object sender, RoutedEventArgs e)
        {
            AsyncExecute ae = new AsyncExecute(() =>
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "red files (*.red)|*.red|All files (*.*)|*.*";
                saveFileDialog1.RestoreDirectory = true;

                App.Current.Dispatcher.Invoke(() => saveFileDialog1.ShowDialog());

                if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
                {
                    App.DataBase.Configuration.LazyLoadingEnabled = false;
                    List<Project> tmp = App.DataBase.Projects.ToList();

                    foreach (var project in tmp)
                    {
                        App.DataBase.Entry(project).Collection(x => x.Items).Load();
                        foreach (var item in project.Items)
                        {
                            App.DataBase.Entry(item).Collection(x => x.Options).Load();
                            App.DataBase.Entry(item).Collection(x => x.Links).Load();
                            App.DataBase.Entry(item).Collection(x => x.Images).Load();
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(writer, tmp);
                        }
                    }
                }
            });
            ae.ShowDialog();
        }

        private void Restore(object sender, RoutedEventArgs e)
        {
            AsyncExecute ae = new AsyncExecute(async() =>
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "red files (*.red)|*.red|All files (*.*)|*.*";

                App.Current.Dispatcher.Invoke(() => openFileDialog1.ShowDialog());

                if (!string.IsNullOrWhiteSpace(openFileDialog1.FileName))
                {
                    using(StreamReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List<Project> tmp = (List<Project>)serializer.Deserialize(reader, typeof(List<Project>));
                        App.DataBase.Database.ExecuteSqlCommand("DELETE FROM dbo.Items");
                        App.DataBase.Database.ExecuteSqlCommand("DELETE FROM dbo.Projects");
                        foreach(var item in tmp)
                        {
                            App.DataBase.Projects.Add(item);
                        }
                        await App.TrySaveChanges();
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location);
                            Process.GetCurrentProcess().Kill();
                        });
                    }
                }
            });
            ae.ShowDialog();
        }
    }
}
