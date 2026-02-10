using System;
using CardGame.Model;
using CommunityToolkit.Mvvm.Input;

namespace CardGame.ViewModel
{
    public class CardGameViewModel : ViewModelBase
    {
        private CardGameModel _model;

        private bool _enabled1;
        private bool _enabled2;
        private bool _enabled3;

        public bool Enabled1
        {
            get => _enabled1;
            set
            {
                _enabled1 = value;
                OnPropertyChanged();
            }
        }

        public bool Enabled2
        {
            get => _enabled2;
            set
            {
                _enabled2 = value;
                OnPropertyChanged();
            }
        }

        public bool Enabled3
        {
            get => _enabled3;
            set
            {
                _enabled3 = value;
                OnPropertyChanged();
            }
        }

        public Player Player => _model.Player;
        public Minion Enemy => _model.Minion;

        public Card Card1 => Player.Card1;
        public Card Card2 => Player.Card2;
        public Card Card3 => Player.Card3;

        public RelayCommand<object> UseCardCommand { get; set; }
        public RelayCommand NextRoundCommand { get; set; }

        private void model_NextRoundEvent(object? sender, EventArgs e)
        {
            Enabled1 = true;
            Enabled2 = true;
            Enabled3 = true;
        }

        private void model_CardUse(object? sender, EventArgs e)
        {
            Enabled1 = false;
            Enabled2 = false;
            Enabled3 = false;
            OnPropertyChanged(nameof(Card1));
            OnPropertyChanged(nameof(Card2));
            OnPropertyChanged(nameof(Card3));
        }

        public CardGameViewModel(CardGameModel model)
        {
            _model = model;

            Enabled1 = true;
            Enabled2 = true;
            Enabled3 = true;

            _model.CardUseEvent += model_CardUse;
            _model.NextRoundEvent += model_NextRoundEvent;

            UseCardCommand = new RelayCommand<object>(param => _model.PlayerCardUse(param));
            NextRoundCommand = new RelayCommand(_model.NextRound);
        }
    }
}
