using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CardGame.ViewModel;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class Minion : Entity
    {
        private Random rnd = new Random();

        private string[] attributes =
        {
            "Vicious",
            "Cursed",
            "Ravenous",
            "Corrupted",
            "Abyssal",
            "Infernal"
        };

        private string[] enemytype =
        {
            "Slime",
            "Skeleton",
            "Cultist",
            "Golem",
            "Zombie"
        };

        public Minion()
        {
            _cards = new Card[3];

            GenerateStats();
            GenerateDeck();
            PickNextCard();
        }

        private void GenerateStats()
        {
            _name = attributes[rnd.Next(attributes.Length)] + " " + enemytype[rnd.Next(enemytype.Length)];
            _health = rnd.Next(21, 50);
            _maxHealth = _health; 
            _shield = rnd.Next(0, 2) == 0 ? 0 : 5;
            _dead = false;
        }

        private void GenerateDeck()
        {
            _cards[0] = new Card("Attack", Actions.Attack, rnd.Next(4, 11));
            _cards[1] = new Card("Heal", Actions.Heal, rnd.Next(2, 6));
            _cards[2] = new Card("Shield", Actions.Shield, rnd.Next(2, 9));
        }

        private void PickNextCard()
        {
            if (_cards == null || _cards.Length == 0)
                return;

            do
            {
                int randomIndex = rnd.Next(_cards.Length);
                _nextCard = _cards[randomIndex];

            }
            while (_nextCard.Action == Actions.Heal && _health == _maxHealth);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UseCard()
        {
            if (!_dead)
            {
                if (_nextCard == null)
                    return;

                if (_nextCard.Action == Actions.Heal)
                {
                    _health += _nextCard.Value;

                    if (_health > _maxHealth)
                        _health = _maxHealth;

                    OnPropertyChanged(nameof(Health));
                }
                else if (_nextCard.Action == Actions.Shield)
                {
                    _shield += _nextCard.Value;
                    OnPropertyChanged(nameof(Health));
                }

                PickNextCard();
            }
        }

        public void Damage(int damage)
        {
            if (!_dead)
            {
                if (damage <= 0)
                    return;

                if (_shield > 0)
                {
                    int shieldDamage = Math.Min(_shield, damage);
                    _shield -= shieldDamage;
                    damage -= shieldDamage;
                }

                if (damage > 0)
                {
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
    }
}
