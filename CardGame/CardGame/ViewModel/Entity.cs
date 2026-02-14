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

        public int CurrentHealth => _health;
        public int MaxHealth => _maxHealth;  
        public int Shield => _shield;

        public bool Dead => _dead;

        public Card NextCard => _nextCard;

        public string NextMove
        {
            get
            {
                if (_nextCard.Action == Actions.Empty)
                {
                    return string.Empty;
                }

                return $"{_nextCard.Action} ({_nextCard.Value})";

            }
        }

        public virtual void Damage(int amount)
        {
            int remainingDamage = amount;

            if (_shield > 0)
            {
                int shieldDamage = Math.Min(_shield, remainingDamage);
                _shield -= shieldDamage;
                remainingDamage -= shieldDamage;
                OnPropertyChanged(nameof(Shield));
                OnPropertyChanged(nameof(Health));
            }

            if (remainingDamage > 0)
            {
                _health -= remainingDamage;

                if (_health <= 0)
                {
                    _health = 0;
                    _dead = true;
                }

                OnPropertyChanged(nameof(CurrentHealth));
                OnPropertyChanged(nameof(Health));
                OnPropertyChanged(nameof(Dead));
            }
        }

        protected virtual void PickNextCard()
        {
            if (_cards == null || _cards.Length == 0)
            {
                _nextCard = new Card("Empty", Actions.Empty, 0); 
                return;
            }

            int index = random.Next(_cards.Length);
            _nextCard = _cards[index];
            OnPropertyChanged(nameof(NextCard));
            OnPropertyChanged(nameof(NextMove));
        }

        public virtual void UseCard()
        {
            if (_nextCard.Action == Actions.Empty)
            {
                return;
            }

            PickNextCard();
        }

        public Entity()
        {
            random = new Random();
            _shield = 0;
            _dead = false;
        }
    }
}