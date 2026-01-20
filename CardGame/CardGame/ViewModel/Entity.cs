using System;
using CardGame.Model;
using static CardGame.Model.Card;

namespace CardGame.ViewModel
{
    public abstract class Entity : ViewModelBase
    {
        protected Random random { get; set; }
        protected string _name { get; set; }
        protected int _health { get; set; }
        protected int _shield { get; set; }
        protected Card _nextCard { get; set; }
        protected Card[] _cards { get; set; }

        public string Name => _name;
        public string Health => $"{_health}+{_shield}";
        public Card NextCard => _nextCard;
        public Actions NextMove => _nextCard.Action;

    }
}
