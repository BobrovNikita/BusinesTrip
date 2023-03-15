using DBCourse_Azuavchikova.MVC.Controllers;
using DBCourse_Azuavchikova.MVC.Views.Abstractions;
using DBCourse_Azuavchikova.MVC.Views;

namespace DBCourse_Azuavchikova
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            IMainView view = new MainView();
            new MainController(view);
            Application.Run((Form)view);
        }
    }
}