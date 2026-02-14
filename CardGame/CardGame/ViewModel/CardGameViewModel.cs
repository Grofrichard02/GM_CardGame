using System;
using CardGame.Model;
using CommunityToolkit.Mvvm.Input;

namespace CardGame.ViewModel
{
    public class CardGameViewModel : ViewModelBase
    {
        private CardGameModel _model;

        private bool _enabled;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public Player Player => _model.Player;
        public Entity Enemy => _model.Enemy;  

        public Card Card1 => Player.Card1;
        public Card Card2 => Player.Card2;
        public Card Card3 => Player.Card3;

        public string UntilBoss
        {
            get
            {
                int roundsLeft = 5 - _model.Rounds;

                if (roundsLeft <= 0)
                {
                    return string.Empty;  
                }

                return $"{roundsLeft} round left until Boss";
            }
        }

        public RelayCommand<object> UseCardCommand { get; set; }
        public RelayCommand NextRoundCommand { get; set; }

        private void model_NextRoundEvent(object? sender, EventArgs e)
        {
            Enabled = true;
            OnPropertyChanged(nameof(UntilBoss));  
        }

        private void model_CardUse(object? sender, EventArgs e)
        {
            Enabled = false;
            OnPropertyChanged(nameof(Card1));
            OnPropertyChanged(nameof(Card2));
            OnPropertyChanged(nameof(Card3));
            OnPropertyChanged(nameof(Enemy));  
            OnPropertyChanged(nameof(UntilBoss));  
        }

        public CardGameViewModel(CardGameModel model)
        {
            _model = model;

            Enabled = true;

            _model.CardUseEvent += model_CardUse;
            _model.NextRoundEvent += model_NextRoundEvent;

            UseCardCommand = new RelayCommand<object>(param => _model.PlayerCardUse(param));
            NextRoundCommand = new RelayCommand(_model.NextRound);
        }
    }
}