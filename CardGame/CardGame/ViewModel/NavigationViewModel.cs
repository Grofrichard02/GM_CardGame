using System;
using CardGame.Model;
using CommunityToolkit.Mvvm.Input;

namespace CardGame.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private bool _hasNotChosen;
        private NavigationModel _model;
        private CardGameModel _cardmodel;


        public string Name1 => _model.Choice1.Name;
        public string Name2 => _model.Choice2.Name;
        public string Description1 => _model.Choice1.Description;
        public string Description2 => _model.Choice2.Description;
        public Player Player => _model.Player;

        public bool HasNotChosen
        {
            get => _hasNotChosen;
            set
            {
                _hasNotChosen = value;
                OnPropertyChanged();
            }
        }

        public string UntilBoss => string.Empty;

        public RelayCommand<object> ChooseCommand { get; }
        public RelayCommand ExitNavigationCommand { get; }

        public event EventHandler? ExitNavigationEvent;

        public NavigationViewModel(NavigationModel model,CardGameModel cardmodel)
        {
            _model = model;
            _cardmodel = cardmodel;

            _hasNotChosen = true;

            ChooseCommand = new RelayCommand<object>(OnChoose);
            ExitNavigationCommand = new RelayCommand(OnExitNavigation);
        }

        private void OnChoose(object parameter)
        {
            int choice = 0;

            if (parameter is string strParam)
            {
                int.TryParse(strParam, out choice);
            }
            else if (parameter is int intParam)
            {
                choice = intParam;
            }

            if (choice > 0)
            {
                _model.PickChoice(choice);
                HasNotChosen = false;
            }
        }

        public void OnExitNavigation()
        {
            _cardmodel.Rounds += 1;
            ExitNavigationEvent?.Invoke(this, EventArgs.Empty);
            
        }
    }
}
