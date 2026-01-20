using System.Configuration;
using System.Data;
using System.Windows;
using CardGame.Model;
using CardGame.ViewModel;

namespace CardGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _window;
        private CardGameViewModel _viewModel;
        private CardGameModel _model;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _window = new MainWindow();
            _model = new CardGameModel();
            _viewModel = new CardGameViewModel(_model);
            _window.DataContext = _viewModel;
            _window.Show();
        }
    }

}
