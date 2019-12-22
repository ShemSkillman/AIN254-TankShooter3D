using TankShooter.Combat;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TankShooter.Core;

public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitpointsText;
    [SerializeField] Image fillArea;

    int initialHP;
    Slider healthBar;
    Health player;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
        player = GetComponentInParent<GameSession>().Player;
    }

    private void Start()
    {
        initialHP = player.Hitpoints;
    }

    private void OnEnable()
    {
        player.onHitpointsChange += UpdateHealth;
    }

    private void OnDisable()
    {
        player.onHitpointsChange -= UpdateHealth;
    }

    public void UpdateHealth(int health)
    {
        hitpointsText.text = health.ToString() + "/" + initialHP.ToString() + " HP";

        float healthPerc = health / (float)initialHP;

        if (healthPerc < 0.3f)
        {
            fillArea.color = Color.red;
        }
        else if (health < 0.6f)
        {
            fillArea.color = Color.yellow;
        }
        else
        {
            fillArea.color = Color.green;
        }

        healthBar.value = healthPerc;
    }


}
