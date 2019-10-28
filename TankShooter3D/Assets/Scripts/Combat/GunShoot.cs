using System.Collections;
using TankShooter.Core;
using TankShooter.Movement;
using UnityEngine;

namespace TankShooter.Combat
{
    public class GunShoot : MonoBehaviour
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] float shootForce = 1000f;
        [SerializeField] float recoilForce = 5000f;
        [SerializeField] Transform gunEnd;
        [SerializeField] float reloadTime = 0.3f;
        [SerializeField] int maxAmmoCapacity = 30;

        // Cache reference
        Animation anim;
        Rigidbody rb;

        // State
        bool isReloaded = true;
        int currentAmmo;

        private void Awake()
        {
            anim = GetComponent<Animation>();
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            currentAmmo = maxAmmoCapacity;
        }

        public void ShootGun()
        {
            if (!isReloaded) return;

            isReloaded = false;

            anim.Play();
            SpawnProjectile();
            Recoil();

            StartCoroutine(Reload());
        }

        private void SpawnProjectile()
        {
            Projectile projectile = Instantiate(projectilePrefab, gunEnd.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        }

        public void Recoil()
        {
            rb.AddForce(-transform.forward * recoilForce * rb.mass);
        }

        public int ReplenishAmmo(int newAmmo)
        {
            int ammoLeftover = newAmmo + currentAmmo - maxAmmoCapacity;

            if (ammoLeftover < 1)
            {
                currentAmmo += newAmmo;
                return 0;
            }
            else
            {
                currentAmmo = maxAmmoCapacity;
                return ammoLeftover;
            }
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

