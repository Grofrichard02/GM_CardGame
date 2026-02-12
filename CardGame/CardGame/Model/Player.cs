using System;
using CardGame.ViewModel;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class Player : Entity
    {
        private Card[] _currentHand;

        public Card Card1 => _currentHand[0];
        public Card Card2 => _currentHand[1];
        public Card Card3 => _currentHand[2];

        public Player()
        {
            _cards = new Card[10];
            _nextCard = new Card("", Actions.Empty, 0);

            GenerateStats();
            GenerateDeck();
            GenerateCurrenthand();
        }

        private void GenerateStats()
        {
            _name = "Hero";
            _health = 100;
            _maxHealth = _health;
            _shield = 10;
            _dead = false;
        }

        private void GenerateDeck()
        {
            int index = 0;

            _cards[index++] = new Card("Basic Attack", Actions.Attack, 10);
            _cards[index++] = new Card("Basic Attack", Actions.Attack, 10);
            _cards[index++] = new Card("Basic Attack", Actions.Attack, 10);

            _cards[index++] = new Card("Basic Heal", Actions.Heal, 2);
            _cards[index++] = new Card("Basic Heal", Actions.Heal, 2);

            _cards[index++] = new Card("Basic Shield", Actions.Shield, 4);
            _cards[index++] = new Card("Basic Shield", Actions.Shield, 4);

            _cards[index++] = new Card("Advanced Attack", Actions.Attack, 20);
            _cards[index++] = new Card("Advanced Heal", Actions.Heal, 10);
            _cards[index++] = new Card("Advanced Shield", Actions.Shield, 8);
        }

        public void GenerateCurrenthand()
        {
            _currentHand = new Card[3];
            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                _currentHand[i] = _cards[rnd.Next(_cards.Length)];
            }
        }

        public void UseCard(int index)
        {
            if (!_dead)
            {
                if (_currentHand[index].Action == Actions.Heal)
                {
                    _health += _currentHand[index].Value;
                    if (_health > _maxHealth)
                        _health = _maxHealth;
                }
                else if (_currentHand[index].Action == Actions.Shield)
                {
                    _shield += _currentHand[index].Value;
                }

                OnPropertyChanged(nameof(Health));
                _currentHand[index] = new Card("", Actions.Empty, 0);
            }
        }

        public void Damage(int damage)
        {
            if (!_dead)
            {
                if (_shield >= damage)
                {
                    _shield -= damage;
                }
                else
                {
                    damage -= _shield;
                    _shield = 0;
                    _health -= damage;
                }

                if (_health <= 0)
                {
                    _health = 0;
                    _dead = true;
                }

                OnPropertyChanged(nameof(Health));
                OnPropertyChanged(nameof(Dead));
            }
        }

        public Card GetCard(int index)
        {
            return _currentHand[index];
        }

        public void AddCardToDeck(Card card)
        {
            Card[] newCards = new Card[_cards.Length + 1];

            for (int i = 0; i < _cards.Length; i++)
            {
                newCards[i] = _cards[i];
            }

            newCards[newCards.Length - 1] = card;
            _cards = newCards;
        }

        public void AddShield(int amount)
        {
            _shield += amount;
            OnPropertyChanged(nameof(Health));
        }

        public void AddHealth(int amount)
        {
            _health += amount;

            if (_health > _maxHealth)
                _health = _maxHealth;

            OnPropertyChanged(nameof(Health));
        }

        public void IncreaseMaxHealth(int amount)
        {
            _maxHealth += amount;
            OnPropertyChanged(nameof(Health));
        }
    }
}
