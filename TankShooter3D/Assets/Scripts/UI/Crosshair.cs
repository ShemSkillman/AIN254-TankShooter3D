using UnityEngine;
using TankShooter.Combat;
using UnityEngine.UI;
using TankShooter.Core;
using TMPro;
using TankShooter.Movement;
using System.Collections;

namespace TankShooter.UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] float maxTargetDistance = 1000;
        [SerializeField] float UIRefreshRatePerSecond = 30;

        [Header("UI components")]
        [SerializeField] RectTransform crossHair;
        [SerializeField] TextMeshProUGUI reloadText;
        [SerializeField] TextMeshProUGUI ammoCountText;
        [SerializeField] TextMeshProUGUI targetInfoText;
        [SerializeField] TextMeshProUGUI healthPercentageText;
        [SerializeField] Image aimingCircle;

        // Cache reference
        GunShoot playerGun;
        Health playerHealth;
        Health target;
        TankMove targetMove;

        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            playerGun = player.GetComponentInChildren<GunShoot>();
            playerHealth = player.GetComponent<Health>();
        }

        IEnumerator Start()
        {
            while (true)
            {
                float timeUntilReload = playerGun.GetTimeUntilReload();
                float reloadTime = playerGun.GetReloadTime();

                UpdateReloadText(timeUntilReload, reloadTime);
                UpdateAimingCircle(timeUntilReload, reloadTime);
                UpdateAmmoCountText();
                UpdateHealthPercentageText();
                UpdateTargetInformation();

                yield return new WaitForSeconds(1 / UIRefreshRatePerSecond);
            }
            
        }

        private void UpdateHealthPercentageText()
        {
            float healthPerc = (playerHealth.Hitpoints / (float)playerHealth.MaxHitpoints) * 100;
            healthPercentageText.text = "HP: " + Mathf.RoundToInt(healthPerc) + "%";
        }

        private void UpdateAmmoCountText()
        {
            ammoCountText.text = "Ammo: " + playerGun.GetCurrentAmmo() + "/" + playerGun.GetMaxAmmoCapacity();
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

        private void UpdateTargetInformation()
        {
            if (target == null)
            {
                targetInfoText.text = "N/A";
                return;
            }

            targetInfoText.text = string.Format("Distance: {0:0.0}m\nHealth: {1}%\nSpeed: {2:0} km/h", 
                playerGun.GetTargetDistance(target), target.GetHitpointsPercentage(), targetMove.GetTankSpeedKmpH());
        }

        public void SetTarget(Health target)
        {
            this.target = target;

            if (target != null) targetMove = target.GetComponentInChildren<TankMove>();
            else targetMove = null;
        }
    }
}

