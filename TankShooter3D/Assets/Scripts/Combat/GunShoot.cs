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

        bool isReloaded = true;

        public void ShootGun()
        {
            if (!isReloaded) return;

            isReloaded = false;

            Projectile projectile = Instantiate(projectilePrefab, gunEnd.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

            StartCoroutine(Reload());
        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadTime);
            isReloaded = true;
        }
    }
}

