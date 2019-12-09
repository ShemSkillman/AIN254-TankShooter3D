using TankShooter.Movement;
using UnityEngine;

namespace TankShooter.Core
{
    public class Health : MonoBehaviour
    {
        public int Hitpoints { get { return hitpoints; }  }
        public int MaxHitpoints { get { return maxHitpoints; } }
        [SerializeField] int hitpoints = 100;
        [SerializeField] TankExplosion explosionPrefab;
        [SerializeField] Transform tankBody;

        int maxHitpoints;
        Rigidbody[] tankParts;
        bool isDead = false;

        public delegate void OnHitpointsChange(int health);
        public event OnHitpointsChange onHitpointsChange;

        private void Start()
        {
            tankParts = GetComponentsInChildren<Rigidbody>();
            maxHitpoints = hitpoints;
            onHitpointsChange?.Invoke(hitpoints);
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

            hitpoints -= damage;
            onHitpointsChange?.Invoke(hitpoints);

            if (hitpoints < 1)
            {
                DestroyTank();
            }
        }

        private void DestroyTank()
        {
            isDead = true;

            if (tag == "Enemy") FindObjectOfType<Score>().EnemyDestroyed();

            if (explosionPrefab != null)
            {
                BroadcastMessage("Die");
                TankExplosion explosion = Instantiate(explosionPrefab, tankBody.position, Quaternion.identity);
                explosion.Explode(tankParts);
            }
        }
    }
}

