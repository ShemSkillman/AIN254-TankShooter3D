using System.Collections;
using UnityEngine;
using TankShooter.Core;

namespace TankShooter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float lifeTime = 10f;
        [SerializeField] int damage = 30;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            Health health = other.gameObject.GetComponentInParent<Health>();
            if (health == null) return;

            health.TakeDamage(30);
            Destroy(gameObject);
        }
    }
}

