using UnityEngine;
using TankShooter.Combat;
using UnityEngine.UI;
using TankShooter.Core;
using TMPro;
using System;
using System.Collections;

namespace TankShooter.UI
{
    public class CrosshairUI : MonoBehaviour
    {
        [SerializeField] float maxTargetDistance = 1000;
        [SerializeField] float UIRefreshRatePerSecond = 30;

        [Header("UI components")]
        [SerializeField] TextMeshProUGUI reloadText;
        [SerializeField] TextMeshProUGUI ammoCountText;
        [SerializeField] TextMeshProUGUI targetDistanceText;
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

        IEnumerator Start()
        {
            while(true)
            {
                float timeUntilReload = playerGun.GetTimeUntilReload();
                float reloadTime = playerGun.GetReloadTime();

                UpdateReloadText(timeUntilReload, reloadTime);
                UpdateAimingCircle(timeUntilReload, reloadTime);
                UpdateAmmoCountText();
                UpdateHealthPercentageText();
                UpdateTargetDistanceText();

                yield return new WaitForSeconds(1 / UIRefreshRatePerSecond);
            }
            
        }

        private void UpdateTargetDistanceText()
        {
            targetDistanceText.text = string.Format("{0:0.0}m", playerGun.GetTargetDistance(maxTargetDistance));
        }

        private void UpdateHealthPercentageText()
        {
            float healthPerc = (playerHealth.Hitpoints / (float)playerHealth.MaxHitpoints) * 100;
            healthPercentageText.text = Mathf.RoundToInt(healthPerc) + "%";
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

