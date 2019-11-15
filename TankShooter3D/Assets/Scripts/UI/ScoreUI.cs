using UnityEngine;
using TankShooter.Combat;
using TankShooter.Core;
using TMPro;

namespace TankShooter.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI killCountText;
        [SerializeField] TextMeshProUGUI hitpointsText;

        GameObject player;
        Score score;
        
        int initialHP;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            score = FindObjectOfType<Score>();
        }

        private void Start()
        {
            initialHP = player.GetComponent<Health>().Hitpoints;
        }

        private void OnEnable()
        {
            player.GetComponent<Health>().onHitpointsChange += UpdateHealth;
            score.onEnemyKilled += UpdateKillCount;
        }

        private void OnDisable()
        {
            player.GetComponent<Health>().onHitpointsChange -= UpdateHealth;
            score.onEnemyKilled -= UpdateKillCount;
        }

        public void UpdateKillCount(int killCount)
        {
            killCountText.text = "Kill count:\n" + killCount.ToString();
        }

        public void UpdateHealth(int health)
        {
            hitpointsText.text = "Hitpoints:\n" + health.ToString() + "/" + initialHP.ToString(); 
        }
    }
}

