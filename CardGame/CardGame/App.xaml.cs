using System.Windows;
using CardGame.Model;
using CardGame.View;
using CardGame.ViewModel;

namespace CardGame
{
    public partial class App : Application
    {
        private MainWindow _window;
        private CardGameViewModel _viewModel;
        private CardGameModel _model;
        private CombatPage _combatPage;

        private NavigationModel _navigationModel;
        private NavigationViewModel _navigationViewModel;
        private NavigationPage _navigationPage;
        private Player _player;

        private double _difficulty = 1.0;

        public App()
        {
            Startup += App_Startup;
        }

        private void ChangeToCombat()
        {
            _player.GenerateCurrenthand();
            _window.Content = new CombatPage { 
            DataContext= _viewModel,
            };
            
        }

        private void ChangeToNavigation(object? sender, GameEndEventArgs e)
        {
            if (e.EnemyDead)
            {
                _navigationViewModel.HasNotChosen = true;
                _navigationModel.GenerateNewNavigation();
                _navigationPage.DataContext = _navigationViewModel;
                _window.Content = _navigationPage;
            }
            else if (e.PlayerDead)
            {
                MessageBox.Show("Game Over! You have died.");
                _window.Close();
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0 && double.TryParse(e.Args[0], out double diff))
            {
                _difficulty = diff;
            }

            _window = new MainWindow();
            _player = new Player();

            _model = new CardGameModel(_player, _difficulty);
            _viewModel = new CardGameViewModel(_model);
            _combatPage = new CombatPage();

            _navigationModel = new NavigationModel(_player);
            _navigationViewModel = new NavigationViewModel(_navigationModel,_model);
            _navigationPage = new NavigationPage();

            _model.GameEndEvent += ChangeToNavigation;
            _navigationViewModel.ExitNavigationEvent += (s, args) => ChangeToCombat();

            _window.DataContext = _viewModel;
            _window.Content = _combatPage;
            _window.Show();
        }
    }
}
