using System.Collections;
using UnityEngine;

namespace TankShooter.Combat
{
    public class GunShoot : MonoBehaviour
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] float shootForce = 1000f;
        [SerializeField] Transform gunEnd;
        [SerializeField] float reloadTime = 0.3f;
        [SerializeField] int maxAmmoCapacity = 30;

        // State
        bool isReloaded = true;
        int currentAmmo;

        private void Start()
        {
            currentAmmo = maxAmmoCapacity;
        }

        public void ShootGun()
        {
            if (!isReloaded) return;

            isReloaded = false;

            Projectile projectile = Instantiate(projectilePrefab, gunEnd.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

            StartCoroutine(Reload());
        }

        public void ReplenishAmmo(int ammoCount)
        {
            currentAmmo = Mathf.Min(currentAmmo + ammoCount, maxAmmoCapacity);
        }

        IEnumerator Reload()
        {
            while (currentAmmo < 1) yield return null;
            currentAmmo--;
            yield return new WaitForSeconds(reloadTime);
            isReloaded = true;
        }
    }
}

