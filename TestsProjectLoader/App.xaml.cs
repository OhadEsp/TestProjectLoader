using DllFileVMM;
using System.Windows;

namespace TestsProjectLoader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow app = new MainWindow();
            DllFileViewModel context = new DllFileViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
