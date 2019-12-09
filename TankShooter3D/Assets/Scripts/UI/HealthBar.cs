using TankShooter.Core;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitpointsText;
    [SerializeField] Image fillArea;

    int initialHP;
    GameObject player;
    Slider healthBar;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        healthBar = GetComponent<Slider>();
    }

    private void Start()
    {
        initialHP = player.GetComponent<Health>().Hitpoints;
    }

    private void OnEnable()
    {
        player.GetComponent<Health>().onHitpointsChange += UpdateHealth;
    }

    private void OnDisable()
    {
        player.GetComponent<Health>().onHitpointsChange -= UpdateHealth;
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
