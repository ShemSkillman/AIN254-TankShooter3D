using System.Collections;
using UnityEngine;

namespace TankShooter.Combat
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] float shootForce = 1000f;
        [SerializeField] Transform gunEnd;
        [SerializeField] float reloadTime = 0.3f;

        bool isReloaded = true;

        private void Update()
        {
            if (Input.GetButton("Fire1") && isReloaded)
            {
                Fire();
            }
        }

        public void Fire()
        {
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

