using UnityEngine;

namespace TankShooter.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 100;
        [SerializeField] Canvas playerUI;

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

