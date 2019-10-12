using UnityEngine;

namespace TankShooter.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 100;

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}

