using System;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class NavigationChoice
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private int _choice;
        private Actions _action;
        private int _value;
        private Card _card;
        private Player _player;

        private void GenerateChoice()
        {
            Random rnd = new Random();
            _choice = rnd.Next(0, 3);

            switch (_choice)
            {
                case 0:
                    _action = rnd.Next(0, 2) == 0 ? Actions.Heal : Actions.Shield;
                    _value = rnd.Next(5, 11);
                    Name = $"Add {_action}";
                    Description = $"Adds {_value} {_action}";
                    break;

                case 1:
                    int subChoice = rnd.Next(1, 4);
                    switch (subChoice)
                    {
                        case 1:
                            _action = Actions.Attack;
                            _value = rnd.Next(10, 26);
                            break;
                        case 2:
                            _action = Actions.Heal;
                            _value = rnd.Next(5, 16);
                            break;
                        case 3:
                            _action = Actions.Attack;
                            _value = rnd.Next(3, 13);
                            break;
                    }

                    _card = new Card("Navigation Card", _action, _value);
                    Name = "Adds Navigation Card";
                    Description = $"Adds Card:\n{_action} : {_value}";
                    break;

                case 2:
                    _value = rnd.Next(2, 6);
                    Name = "Increase Max health";
                    Description = $"Increase your Max health by {_value}";
                    break;
            }
        }

        public void ApplyChoice()
        {
            switch (_choice)
            {
                case 0:
                    if (_action == Actions.Heal)
                        _player.AddHealth(_value);
                    else if (_action == Actions.Shield)
                        _player.AddShield(_value);
                    break;

                case 1:
                    _player.AddCardToDeck(_card);
                    break;

                case 2:
                    _player.IncreaseMaxHealth(_value);
                    break;
            }
        }

        public NavigationChoice(Player player)
        {
            _player = player;
            GenerateChoice();
        }
    }
}
