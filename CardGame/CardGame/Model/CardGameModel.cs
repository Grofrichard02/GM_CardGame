using System;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class CardGameModel
    {
        private Player _player;
        private Minion _minion;

        public Player Player => _player;
        public Minion Minion => _minion;

        public event EventHandler? CardUseEvent;
        public event EventHandler? NextRoundEvent;
        public event EventHandler<GameEndEventArgs>? GameEndEvent;

        public CardGameModel(Player player)
        {
            _player = player;
            _minion = new Minion();
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
                _minion.Damage(card.Value);
            }

            _player.UseCard(cardIndex);

            CardUseEvent?.Invoke(this, EventArgs.Empty);

            if (_player.Dead || _minion.Dead)
            {
                GameEndEvent?.Invoke(this,
                    new GameEndEventArgs(_player.Dead, _minion.Dead, false));

                _minion = new Minion();
            }
        }

        public void NextRound()
        {
            Card card = _minion.NextCard;

            if (card.Action == Actions.Attack)
            {
                _player.Damage(card.Value);
            }

            _minion.UseCard();
            _player.GenerateCurrenthand();

            CardUseEvent?.Invoke(this, EventArgs.Empty);

            if (_player.Dead || _minion.Dead)
            {
                GameEndEvent?.Invoke(this,
                    new GameEndEventArgs(_player.Dead, _minion.Dead, false));

                _minion = new Minion();
            }

            NextRoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
