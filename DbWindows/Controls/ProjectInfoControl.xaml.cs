using DbElems;
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
    public partial class ProjectInfoControl : UserControl
    {
        public Project Project
        {
            get { return (Project)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Project")); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static DependencyProperty ProjectProperty =
           DependencyProperty.Register("Project", typeof(Project), typeof(ProjectInfoControl), new FrameworkPropertyMetadata()
           {
               BindsTwoWayByDefault = true,
               PropertyChangedCallback = (o, e) =>
               {
                   //Лямбда, вызываемая как статическая функция для обновления данных в объекте
                   //ProjectInfoControl that = o as ProjectInfoControl;
                   //that.Rebind();
               }
           });

        public Action NewProject { get; set; }

        public ProjectInfoControl()
        {
            InitializeComponent();
        }

        private async void OkClick(object sender, RoutedEventArgs e)
        {
            if (Project.Id == 0)
            {
                await App.TrySaveChanges();
            }
            NewProject?.Invoke();
        }
    }
}
