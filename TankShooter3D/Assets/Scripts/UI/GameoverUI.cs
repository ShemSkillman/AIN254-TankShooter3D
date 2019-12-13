using TMPro;
using UnityEngine;
using TankShooter.Core;

namespace TankShooter.UI
{
    public class GameoverUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI killsText;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI timeText;

        GameSession gameSession;

        private void Awake()
        {
            gameSession = GetComponentInParent<GameSession>();
        }

        private void Start()
        {
            killsText.text = "Total kills: " + gameSession.Kills.ToString();
            scoreText.text = "Total score: " + gameSession.TotalScore.ToString();
            timeText.text = "Game duration: " + gameSession.Time.ToString();
        }
    }
}

