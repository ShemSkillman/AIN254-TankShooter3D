using UnityEngine;
using TankShooter.Combat;
using UnityEngine.UI;
using TankShooter.Core;
using TMPro;

namespace TankShooter.UI
{
    public class CrosshairUI : MonoBehaviour
    {
        [Header("UI components")]
        [SerializeField] TextMeshProUGUI reloadText;
        [SerializeField] TextMeshProUGUI ammoCountText;
        [SerializeField] TextMeshProUGUI distanceToTarget;
        [SerializeField] TextMeshProUGUI healthPercentageText;
        [SerializeField] Image aimingCircle;

        // Cache reference
        GunShoot playerGun;
        Health playerHealth;

        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            playerGun = player.GetComponentInChildren<GunShoot>();
            playerHealth = player.GetComponent<Health>();
        }

        private void Update()
        {
            float timeUntilReload = playerGun.GetTimeUntilReload();
            float reloadTime = playerGun.GetReloadTime();

            UpdateReloadText(timeUntilReload, reloadTime);
            UpdateAimingCircle(timeUntilReload, reloadTime);
            UpdateAmmoCountText();
            UpdateHealthPercentageText();
        }

        private void UpdateHealthPercentageText()
        {
            healthPercentageText.text = playerHealth.Hitpoints.ToString();
        }

        private void UpdateAmmoCountText()
        {
            ammoCountText.text = playerGun.GetCurrentAmmo() + "/" + playerGun.GetMaxAmmoCapacity();
        }

        private void UpdateReloadText(float timeUntilReload, float reloadTime)
        {
            if (timeUntilReload <= Mathf.Epsilon)
            {
                reloadText.text = string.Format("Ready\n{0:0.00} secs", reloadTime);
                reloadText.color = Color.green;
                return;
            }

            reloadText.text = string.Format("Reloading\n{0:0.00} secs", timeUntilReload);
            reloadText.color = Color.red;
        }

        private void UpdateAimingCircle(float timeUntilReload, float reloadTime)
        {
            if (timeUntilReload <= Mathf.Epsilon)
            {
                aimingCircle.fillAmount = 1f;
                aimingCircle.color = Color.green;
                return;
            }

            float reloadPercentage = (reloadTime - timeUntilReload) / reloadTime;

            aimingCircle.fillAmount = reloadPercentage;
            aimingCircle.color = Color.red;
        }
    }
}

