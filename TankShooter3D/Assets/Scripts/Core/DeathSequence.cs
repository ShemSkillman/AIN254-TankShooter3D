using UnityEngine;
using TankShooter.Combat;

namespace TankShooter.Core
{
    public class DeathSequence : MonoBehaviour
    {
        [SerializeField] GameObject orbitalDeathCam;

        Health playerHealth;

        private void Awake()
        {
            playerHealth = GetComponentInChildren<Health>();
        }

        public void TankDie()
        {
            Instantiate(orbitalDeathCam, playerHealth.GetTankPosition(), Quaternion.identity);
        }

        private void DisplayGameStatistics()
        {

            GameSession score = GetComponent<GameSession>();

            int totalScore = score.TotalScore;
            int kills = score.Kills;
        }

    }
}

