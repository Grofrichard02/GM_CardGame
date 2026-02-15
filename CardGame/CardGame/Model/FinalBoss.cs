using CardGame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class FinalBoss : Entity
    {
        private double _difficulty;

        private string[] attributes =
        {
            "Vicious",
            "Cursed",
            "Ravenous",
            "Corrupted",
            "Abyssal",
            "Infernal"
        };

        private string[] enemyType =
        {
            "Slime King",
            "Skeleton King",
            "Cultist Leader",
            "Golem Prime",
            "Emperor"
        };

        public FinalBoss(double difficulty) : base()
        {
            _difficulty = difficulty;
            _cards = new Card[6];  

            GenerateStats();
            GenerateDeck();
            PickNextCard();
        }

        private void GenerateStats()
        {
            _name = attributes[random.Next(attributes.Length)] + " " + enemyType[random.Next(enemyType.Length)];

            _maxHealth = (int)(random.Next(80, 151) * _difficulty);
            _health = _maxHealth;

            _shield = random.Next(0, 2) == 0 ? (int)(10 * _difficulty) : (int)(20 * _difficulty);
            _dead = false;
        }

        private void GenerateDeck()
        {
            _cards[0] = new Card("Basic Attack", Actions.Attack, (int)(random.Next(7, 13) * _difficulty));
            _cards[1] = new Card("Heavy Attack", Actions.Attack, (int)(random.Next(10, 16) * _difficulty));
            _cards[2] = new Card("Basic Heal", Actions.Heal, (int)(random.Next(7, 11) * _difficulty));
            _cards[3] = new Card("Cursed Heal", Actions.Heal, (int)(random.Next(10, 16) * _difficulty));
            _cards[4] = new Card("Basic Shield", Actions.Shield, (int)(random.Next(7, 11) * _difficulty));
            _cards[5] = new Card("Heavy Shield", Actions.Shield, (int)(random.Next(10, 16) * _difficulty));
        }

        protected override void PickNextCard()
        {
            if (_cards == null || _cards.Length == 0)
            {
                _nextCard = new Card("Empty", Actions.Empty, 0);
                OnPropertyChanged(nameof(NextCard));
                OnPropertyChanged(nameof(NextMove));
                return;
            }

            int attempts = 0;
            const int maxAttempts = 15; 

            do
            {
                int randomIndex = random.Next(_cards.Length);
                _nextCard = _cards[randomIndex];
                attempts++;

                if (attempts > maxAttempts)
                    break;

            } while (_nextCard.Action == Actions.Heal && _health == _maxHealth);

            OnPropertyChanged(nameof(NextCard));
            OnPropertyChanged(nameof(NextMove));
        }

        public override void UseCard()
        {
            if (_dead) return;

            if (_nextCard == null || _nextCard.Action == Actions.Empty)
            {
                PickNextCard();
                return;
            }

            switch (_nextCard.Action)
            {
                case Actions.Heal:
                    _health += _nextCard.Value;
                    if (_health > _maxHealth)
                        _health = _maxHealth;
                    OnPropertyChanged(nameof(Health));
                    break;

                case Actions.Shield:
                    _shield += _nextCard.Value;
                    OnPropertyChanged(nameof(Health));
                    OnPropertyChanged(nameof(Shield));
                    break;

                case Actions.Attack:
                    break;
            }

            PickNextCard();
        }
    }
}
