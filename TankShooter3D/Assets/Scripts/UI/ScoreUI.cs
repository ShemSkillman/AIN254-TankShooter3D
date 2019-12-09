using UnityEngine;
using TankShooter.Core;
using TMPro;

namespace TankShooter.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI killCountText;
        [SerializeField] TextMeshProUGUI scoreText;

        GameObject player;
        Score score;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            score = FindObjectOfType<Score>();
        }

        private void OnEnable()
        {
            score.onEnemyKilled += UpdateScore;
        }

        private void OnDisable()
        {
            score.onEnemyKilled -= UpdateScore;
        }

        public void UpdateScore(int killCount, int totalScore)
        {
            killCountText.text = "Kill count: " + killCount.ToString();
            scoreText.text = "Score: " + totalScore.ToString();
        }
    }
}

