
using System.Windows;

namespace CompareFolder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize main window and view model
            var mainWindow = new MainWindow();
            var viewModel = new CompareFolder.ViewModel.ViewModel();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
