using System;
using CardGame.Model;
using CommunityToolkit.Mvvm.Input;

namespace CardGame.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private bool _hasNotChosen;
        private NavigationModel _model;

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

        public RelayCommand<object> ChooseCommand { get; set; }
        public RelayCommand ExitNavigationCommand { get; set; }

        public event EventHandler? ExitNavigationEvent;

        public void OnExitNavigation()
        {
            ExitNavigationEvent?.Invoke(this, EventArgs.Empty);
        }

        public NavigationViewModel(NavigationModel model)
        {
            _model = model;
            HasNotChosen = true;

            ChooseCommand = new RelayCommand<object>(param =>
            {
                _model.PickChoice(int.Parse(param.ToString()));
                HasNotChosen = false;
            });

            ExitNavigationCommand = new RelayCommand(OnExitNavigation);
        }
    }
}
