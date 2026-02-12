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
        protected int _maxHealth { get; set; }
        protected int _shield { get; set; }
        protected bool _dead { get; set; }
        protected Card _nextCard { get; set; }
        protected Card[] _cards { get; set; }

        public string Name => _name;
        public string Health => $"{_health}/{_maxHealth}+{_shield}";

        public bool Dead => _dead;

        public Card NextCard => _nextCard;
        public Actions NextMove => _nextCard.Action;
    }
}
