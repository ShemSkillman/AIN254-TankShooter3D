using UnityEngine;
using UnityEngine.Events;
using TankShooter.Core;

namespace TankShooter.Combat
{
    public class Health : MonoBehaviour
    {
        public int Hitpoints { get { return hitpoints; }  }
        public int MaxHitpoints { get { return maxHitpoints; } }
        [SerializeField] int hitpoints = 100;
        [SerializeField] TankExplosion explosionPrefab;
        [SerializeField] Transform tankBody;

        EnemySpawner spawner;

        int maxHitpoints;
        Rigidbody[] tankParts;
        bool isDead = false;

        public delegate void OnHitpointsChange(int health);
        public event OnHitpointsChange onHitpointsChange;

        public UnityEvent OnDie;

        private void Start()
        {
            tankParts = GetComponentsInChildren<Rigidbody>();
            maxHitpoints = hitpoints;
            onHitpointsChange?.Invoke(hitpoints);
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

            hitpoints = Mathf.Max(hitpoints - damage, 0);
            onHitpointsChange?.Invoke(hitpoints);

            if (hitpoints < 1)
            {
                ExplodeTank();
                OnDie.Invoke();
            }
        }

        private void ExplodeTank()
        {
            isDead = true;

            if (spawner != null) spawner.EnemyDestroyed();
            if (tag == "Enemy") FindObjectOfType<GameSession>().EnemyDestroyed();
            
            BroadcastMessage("Die");
            TankExplosion explosion = Instantiate(explosionPrefab, GetTankPosition(), Quaternion.identity, transform);
            explosion.onExplosionFinished += DestroyTank;
            explosion.Explode(tankParts);
        }

        public Vector3 GetTankPosition()
        {
            return tankBody.position;
        }

        private void DestroyTank()
        {
            Destroy(gameObject);
        }

        public int GetHitpointsPercentage()
        {
            float percentage = (hitpoints / (float)maxHitpoints) * 100f;
            return Mathf.RoundToInt(percentage);
        }

        public bool GetIsDead()
        {
            return isDead;
        }

        public void SetSpawner(EnemySpawner spawner)
        {
            this.spawner = spawner;
        }
    }
}

