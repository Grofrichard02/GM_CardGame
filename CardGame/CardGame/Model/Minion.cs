using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CardGame.ViewModel;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class Minion : Entity
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

        private string[] enemytype =
        {
            "Slime",
            "Skeleton",
            "Cultist",
            "Golem",
            "Zombie"
        };

        public Minion(double difficulty) : base()
        {
            _difficulty = difficulty;
            _cards = new Card[3];

            GenerateStats();
            GenerateDeck();
            PickNextCard();
        }

        private void GenerateStats()
        {
            _name = attributes[random.Next(attributes.Length)] + " " + enemytype[random.Next(enemytype.Length)];

            // Health difficulty alapján
            _maxHealth = (int)(random.Next(21, 50) * _difficulty);
            _health = _maxHealth;

            // Shield difficulty alapján
            _shield = random.Next(0, 2) == 0 ? 0 : (int)(5 * _difficulty);
            _dead = false;
        }

        private void GenerateDeck()
        {
            _cards[0] = new Card("Attack", Actions.Attack, (int)(random.Next(4, 11) * _difficulty));
            _cards[1] = new Card("Heal", Actions.Heal, (int)(random.Next(2, 6) * _difficulty));
            _cards[2] = new Card("Shield", Actions.Shield, (int)(random.Next(2, 9) * _difficulty));
        }
    }
}
