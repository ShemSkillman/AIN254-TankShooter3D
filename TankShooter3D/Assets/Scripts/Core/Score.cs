using UnityEngine;

namespace TankShooter.Core
{
    public class Score : MonoBehaviour
    {
        [SerializeField] int pointsPerKill = 15;

        int kills = 0;
        int totalScore = 0;

        public delegate void OnEnemyKilled(int killCount, int totalScore);
        public event OnEnemyKilled onEnemyKilled;

        public void EnemyDestroyed()
        {
            kills++;
            totalScore += pointsPerKill;
            
            onEnemyKilled?.Invoke(kills, totalScore);
        }
        
    }
}

