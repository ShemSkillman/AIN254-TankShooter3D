using UnityEngine;
using TankShooter.Core;

namespace TankShooter.Combat
{
    public class Health : MonoBehaviour
    {
        public int Hitpoints { get { return hitpoints; }  }
        [SerializeField] int hitpoints = 100;

        public delegate void OnHitpointsChange(int health);
        public event OnHitpointsChange onHitpointsChange;

        private void Start()
        {
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

