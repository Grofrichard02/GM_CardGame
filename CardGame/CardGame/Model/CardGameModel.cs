using CardGame.ViewModel;
using System;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class CardGameModel
    {
        private Player _player;
        private Entity _enemy;  
        private double _difficulty;  
        private int _score;  

        public Player Player => _player;
        public Entity Enemy => _enemy;  
        public int Rounds { get; private set; }  

        public event EventHandler? CardUseEvent;
        public event EventHandler? NextRoundEvent;
        public event EventHandler<GameEndEventArgs>? GameEndEvent;

        public CardGameModel(Player player, double difficulty)
        {
            _player = player;
            _difficulty = difficulty;  
            _score = 0;  
            Rounds = 1;  
            _enemy = new Minion(_difficulty);  
        }

        public void PlayerCardUse(object? index)
        {
            int cardIndex = 0;

            if (index is string strIndex)
            {
                int.TryParse(strIndex, out cardIndex);
            }
            else if (index is int intIndex)
            {
                cardIndex = intIndex;
            }

            Card card = _player.GetCard(cardIndex);

            if (card.Action == Actions.Attack)
            {
                _enemy.Damage(card.Value);  
            }

            _player.UseCard(cardIndex);

            CardUseEvent?.Invoke(this, EventArgs.Empty);

            if (_player.Dead || _enemy.Dead)  
            {
                GameEndEvent?.Invoke(this,
                    new GameEndEventArgs(_player.Dead, _enemy.Dead));  

                _enemy = new Minion(_difficulty);  
            }
        }

        public void NextRound()
        {
            Card card = _enemy.NextCard;  

            if (card.Action == Actions.Attack)
            {
                _player.Damage(card.Value);
            }

            _enemy.UseCard();  
            _player.GenerateCurrenthand();

            CardUseEvent?.Invoke(this, EventArgs.Empty);

            if (_player.Dead || _enemy.Dead)  
            {
                GameEndEvent?.Invoke(this,
                    new GameEndEventArgs(_player.Dead, _enemy.Dead));  

                _enemy = new Minion(_difficulty);  
            }

            NextRoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}