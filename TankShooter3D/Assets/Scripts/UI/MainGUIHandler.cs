using UnityEngine;
using TankShooter.Core;
using TankShooter.Movement;
using TMPro;

namespace TankShooter.UI
{
    public class MainGUIHandler : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI killCountText;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] TextMeshProUGUI speedText;

        TankMove playerMove;
        GameSession gameSession;

        private void Awake()
        {
            gameSession = FindObjectOfType<GameSession>();
            playerMove = gameSession.Player.GetComponentInChildren<TankMove>();
        }

        private void OnEnable()
        {
            gameSession.onEnemyKilled += UpdateScore;
            gameSession.onTimerTick += UpdateTimer;
        }

        private void OnDisable()
        {
            gameSession.onEnemyKilled -= UpdateScore;
            gameSession.onTimerTick -= UpdateTimer;
        }

        private void Update()
        {
            UpdateSpeed();
        }

        private void UpdateScore(int killCount, int totalScore)
        {
            killCountText.text = "Kill count: " + killCount.ToString();
            scoreText.text = "Score: " + totalScore.ToString();
        }

        private void UpdateTimer(string time)
        {
            timeText.text = "Time elapsed:\n" + time;
        }

        private void UpdateSpeed()
        {
            float speedKmpH = playerMove.GetTankSpeedKmpH();
            speedText.text = string.Format("{0:0} km/h", speedKmpH);
        }
    }
}

