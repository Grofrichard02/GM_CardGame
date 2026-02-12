using System;

namespace CardGame.Model
{
    public class GameEndEventArgs : EventArgs
    {
        public bool PlayerDead { get; }
        public bool EnemyDead { get; }
        public bool BossDead { get; }

        public GameEndEventArgs(bool playerDead, bool enemyDead, bool bossDead)
        {
            PlayerDead = playerDead;
            EnemyDead = enemyDead;
            BossDead = bossDead;
        }
    }
}
