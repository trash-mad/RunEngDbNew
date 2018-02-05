using DbElems;
using DbWindows.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DbWindows
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static readonly Object lockobj = new Object();
        static DatabaseContext databaseContext;
        public static DatabaseContext DataBase
        {
            get
            {
                lock (lockobj)
                {
                    return databaseContext;
                }
            }
        }

        public static Project SharedProject { get; private set; }

        //Если сохранение на каждом шаге будут медленными, их можно будет выключить, если вызывать через метод
        public static async Task<bool> TrySaveChanges()
        {
            var ret = await Task.Run(() =>
            {
                lock (lockobj)
                {
                    Console.WriteLine("Начало сохранения в {0}", DateTime.Now);
                    try
                    {
                        databaseContext.SaveChanges();
                        Console.WriteLine("Сохранение в {0}", DateTime.Now);
                        return true;
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("\n|Сохранение сущности в базу пропущено: {0}", e.Message);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("|----Свойство: \"{0}\", Предупреждение: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        return false;
                    }
                }
            });
            return ret;
        }

        private static void RunConsole(Action rungui)
        {
            Console.WriteLine("CalcsGenerator");
            string thisname = Process.GetCurrentProcess().ProcessName;
            Console.WriteLine("Название текущего процесса: {0}", thisname);

            if (Process.GetProcessesByName(thisname).Count() > 1)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine("Найден другой процесс с названием {0}", thisname);
                Console.WriteLine("Не допускается запуск более одной копии процесса, обрабатываюшего базу данных");
                Console.WriteLine("Нажмите \"S\" чтобы автоматически закрыть процесс и запуститься");
                Console.WriteLine("Нажмите любую другую кнопку для выхода");

                ConsoleKeyInfo key = Console.ReadKey();
                var procarr = Process.GetProcessesByName(thisname);
                if (key.Key == ConsoleKey.S)
                {
                    int thisid = Process.GetCurrentProcess().Id;
                    foreach (var proc in procarr)
                    {
                        if (proc.Id != thisid) proc.Kill();
                    }
                }
                else
                {
                    App.Current.Shutdown();
                    return;
                }
            }

            Task.Run(() => App.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    rungui();
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Произошла ошибка исполнения!");
                    Console.WriteLine(ex.Message);
                }
            }));

            Task.Run(() =>
            {
                while (true)
                {
                    Console.Write("\n>");
                    string command = Console.ReadLine();

                    //Показать дерево проектов
                    if (command == "addtest")
                    {
                        //ConsoleCommands.AddItems();
                    }
                    else
                    {
                        Console.WriteLine("Комманда не найдена!");
                    }
                }
            });
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                databaseContext = new DatabaseContext();
                DataBase.Init();

                SharedProject = App.DataBase.Projects.Where(p => p.GUID.ToString() == Project.SharedProjectReservedGuid).Single();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Произошла ошибка: "+ex.Message);
                Console.Beep();
                Console.ReadLine();
            }

            //App.SharedProject.Items.Add(new Item() { Name = "!!!" });
            //SharedProject.Items.Last().Images.Add(new ItemImage());

            ChooseProjectWindow mw = new ChooseProjectWindow();
            mw.ShowDialog();
        }
    }
}
