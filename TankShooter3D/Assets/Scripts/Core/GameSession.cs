using System.Collections;
using UnityEngine;
using TankShooter.Combat;

namespace TankShooter.Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] int pointsPerKill = 15;
        [SerializeField] Health player;

        public int Kills { get { return kills; } }
        public int TotalScore { get { return totalScore; } }
        public string Time { get { return time; } }
        public Health Player { get { return player; } }

        int kills = 0;
        int totalScore = 0;
        string time;

        public delegate void OnEnemyKilled(int killCount, int totalScore);
        public event OnEnemyKilled onEnemyKilled;

        public delegate void OnTimerTick(string time);
        public event OnTimerTick onTimerTick;

        private void Start()
        {
            StartCoroutine(UpdateTimer());
        }

        public void EnemyDestroyed()
        {
            kills++;
            totalScore += pointsPerKill;

            onEnemyKilled?.Invoke(kills, totalScore);
        }

        IEnumerator UpdateTimer()
        {
            int totalSeconds = 0;

            while (true)
            {
                yield return new WaitForSeconds(1f);

                totalSeconds += 1;
                int seconds = totalSeconds;

                int hours = seconds / 3600;
                seconds -= hours * 3600;

                int minutes = seconds / 60;
                seconds -= minutes * 60;

                time = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

                onTimerTick?.Invoke(time);
            }

        }


    }
}

