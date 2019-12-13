using System.Collections;
using TankShooter.Core;
using TankShooter.Movement;
using UnityEngine;

namespace TankShooter.Combat
{
    public class GunShoot : MonoBehaviour
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] float recoilForce = 5000f;
        [SerializeField] Transform gunEnd;
        [SerializeField] float reloadTime = 0.3f;
        [SerializeField] int maxAmmoCapacity = 30;

        public Projectile SelectedProjectile { get { return projectilePrefab; } }

        // Cache reference
        Animation anim;
        Rigidbody rb;

        // State
        bool isReloaded = true;
        int currentAmmo;
        float timeUntilReload = 0f;

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

            if (anim != null) anim.Play();
            SpawnProjectile();
            Recoil();

            StartCoroutine(Reload());
        }

        private void SpawnProjectile()
        {
            Projectile projectile = Instantiate(projectilePrefab, gunEnd.transform.position, transform.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(transform.forward * projectile.ShootForce, ForceMode.VelocityChange);
        }

        public void Recoil()
        {
            rb.AddForce(-transform.forward * recoilForce);
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

            timeUntilReload = reloadTime;
            while(timeUntilReload > 0f)
            {
                timeUntilReload -= Time.deltaTime;
                yield return null;
            }

            isReloaded = true;
        }

        public float GetTimeUntilReload()
        {
            if (timeUntilReload < 0f) timeUntilReload = 0f;
            return timeUntilReload;
        }

        public float GetReloadTime()
        {
            return reloadTime;
        }

        public int GetCurrentAmmo()
        {
            return currentAmmo;
        }

        public int GetMaxAmmoCapacity()
        {
            return maxAmmoCapacity;
        }

        public float GetTargetDistance(float maxDistance)
        {
            Physics.Raycast(gunEnd.position, transform.forward, out RaycastHit ray, maxDistance);
            return ray.distance;
        }

        public Vector3 GetShootLocation(float maxDistance)
        {
            bool isHit = Physics.Raycast(gunEnd.position, transform.forward, out RaycastHit ray, maxDistance);

            if (!isHit) ray.point = gunEnd.position + transform.forward * maxDistance;

            return ray.point;
        }
    }
}

