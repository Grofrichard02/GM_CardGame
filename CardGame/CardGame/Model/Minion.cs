using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CardGame.ViewModel;
using static CardGame.Model.Card;

namespace CardGame.Model
{

    public class Minion : Entity
    {
        private Random rnd= new Random();
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
            _name = attributes[rnd.Next(attributes.Length)] + "" + enemytype[rnd.Next(enemytype.Length)];
            _health = rnd.Next(21, 50);
            _shield = rnd.Next(0, 2) == 0 ? 0 : 5;
        }
        private void GenerateDeck()
        {
            _cards[0]=(new Card("Basic Attack", Actions.Attack, 5));
            _cards[1] = (new Card("Heal", Actions.Heal, 3));
            _cards[2] = (new Card("Shield", Actions.Shield, 3));
        }

        private void PickNextCard()
        {
            if (_cards == null || _cards.Length == 0)
                return;
            int randomIndex = rnd.Next(_cards.Length);
            _nextCard=_cards[randomIndex];
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void UseCard()
        {
            if (_nextCard == null)
            return;

            if (_nextCard.Action == Actions.Heal)
            {
                _health += NextCard.Value;
                OnPropertyChanged(nameof(Health));
            }
            else if (_nextCard.Action == Actions.Shield)
            {
                _shield+= NextCard.Value;
                OnPropertyChanged(nameof(Health));
            }
            PickNextCard();
        }
        public void Damage(int damage)
        {
            if (damage <= 0)
                return;
            if (_shield>0)
            {
                int shieldDamage = Math.Min(_shield, damage);
                _shield -= shieldDamage;
                damage -= shieldDamage;
            }
            if (damage > 0)
            {
                _health -= damage;
            }
            OnPropertyChanged(nameof(Health));
        }
    }
}
