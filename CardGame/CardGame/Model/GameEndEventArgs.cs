using System;

namespace CardGame.Model
{
    public class GameEndEventArgs : EventArgs
    {
        public bool PlayerDead { get; }
        public bool EnemyDead { get; }

        public GameEndEventArgs(bool playerDead, bool enemyDead)
        {
            PlayerDead = playerDead;
            EnemyDead = enemyDead;
        }
    }
}
