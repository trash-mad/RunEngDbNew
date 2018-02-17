using DbElems;
using Microsoft.VisualBasic;
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

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для ChooseProjectWindow.xaml
    /// </summary>
    public partial class ChooseProjectWindow : Window
    {

        Project currentproj = null;
        public Project CurrentProject
        {
            get
            {
                return currentproj;
            }
            set
            {
                currentproj = value;
                Rebind();
            }
        }


        public ObservableCollection<Project> Projects { get; set; }

        public ChooseProjectWindow()
        {
            InitializeComponent();
            Projects = new ObservableCollection<Project>();
            App.DataBase.Configuration.LazyLoadingEnabled = false;
            //App.DataBase.Entry(SharedProject).Collection(x => x.Items).Load();

            Console.WriteLine(string.Format("Получение списка проектов. Всего проектов: {0}",  App.DataBase.Projects.Count()));
            foreach (var item in App.DataBase.Projects)
            {
                Projects.Add(item);
            }
            ProjectList.ItemsSource = Projects;
            Projects.CollectionChanged += ProjectsChanged;

            Action NewProjAction = () =>
            {
                Project tmp = new Project();
                tmp.Name = "Новый проект";
                tmp.Info = "Описание нового проекта...";
                tmp.SetRandomGuid();
                App.DataBase.Projects.Add(tmp);
                Projects.Add(tmp);
            };

            CreateNewButton.MouseDown += (o, e) => NewProjAction();

            CurrentProject = Projects.Last();
            Rebind();
        }

        private async void ProjectsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool needsave = false;
            if (e.OldItems!=null)
            {
                foreach(Project project in e.OldItems)
                {
                    App.DataBase.Set<Project>().Remove(project);
                }
                needsave = true;
            }

            if (e.NewItems != null)
            {
                foreach (Project project in e.NewItems)
                {
                    App.DataBase.Projects.Add(project);
                }
                needsave = true;
            }

            if(needsave) await App.TrySaveChanges();
        }

        private void ProjectSelected(object sender, SelectionChangedEventArgs e)
        {
            var newproj = (sender as ListBox).SelectedItem as Project;
            if (newproj == null) return;
            CurrentProject = newproj;
        }

        //После выбора нового проекта пересоединяет
        private void Rebind()
        {
            Binding binding = new Binding { Source = this, Path = new PropertyPath("CurrentProject"), Mode=BindingMode.TwoWay };
            ProjectControl.SetBinding(ProjectInfoControl.ProjectProperty,binding);
        }

        private void OpenProject()
        {
            ProjectWindow pw = new ProjectWindow(CurrentProject.Id);
            Hide();
            pw.ShowDialog();
        }

        private void RemoveProject(object sender, RoutedEventArgs e)
        {
            var project = ProjectList.SelectedItem as Project;
            if (project == null)
            {
                Interaction.MsgBox("Сначала выберите проект, нажав по нему мышкой!");
                return;
            }

            if (project.GUID.ToString() == Project.SharedProjectReservedGuid)
            {
                Interaction.MsgBox("Этот проект нельзя удалить!");
                return;
            }

            Projects.Remove(project);
        }

        private void BackupManagerButton(object sender, RoutedEventArgs e)
        {
            BackupWindow bw = new BackupWindow();
            bw.ShowDialog();
        }
    }
}
