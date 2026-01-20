using System;

namespace CardGame.ViewModel
{
    public abstract class Entity : ViewModelBase
    {
        private Random random { get; set; }
        private string _name { get; set; }
        private int _health { get; set; }
        private int _shield { get; set; }
        private Card[] _nextCard { get; set; }
        private Card[] _cards { get; set; }

        public string Name => _name;
        public string Health => $"{_health}+{_shield}";
        public Card NextCard => _nextCard;
        public Actions NextMove => _nextCard.Actions;

    }
}
