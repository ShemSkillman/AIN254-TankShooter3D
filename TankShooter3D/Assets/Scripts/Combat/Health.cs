using UnityEngine;

namespace TankShooter.Core
{
    public class Health : MonoBehaviour
    {
        public int Hitpoints { get { return hitpoints; }  }
        public int MaxHitpoints { get { return maxHitpoints; } }
        [SerializeField] int hitpoints = 100;
        int maxHitpoints;

        public delegate void OnHitpointsChange(int health);
        public event OnHitpointsChange onHitpointsChange;

        private void Start()
        {
            maxHitpoints = hitpoints;
            onHitpointsChange?.Invoke(hitpoints);
        }

        public void TakeDamage(int damage)
        {
            hitpoints -= damage;
            onHitpointsChange?.Invoke(hitpoints);

            if (hitpoints < 1)
            {
                if (tag == "Enemy") FindObjectOfType<Score>().EnemyDestroyed();
                Destroy(gameObject);
            }
        }
    }
}

