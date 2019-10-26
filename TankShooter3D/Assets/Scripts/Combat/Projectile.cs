using UnityEngine;
using TankShooter.Core;
using System.Collections.Generic;

namespace TankShooter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float lifeTime = 10f;
        [SerializeField] int damage = 30;

        [Header("FX")]
        [SerializeField] GameObject shotVFX;
        [SerializeField] GameObject hitVFX;
        [SerializeField] AudioClip shotSFX;
        [SerializeField] AudioClip hitSFX;
        public List<GameObject> trails;

        //State
        bool collided = false;

        // Cache references
        AudioSource audioSource;
        Rigidbody rb;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            Destroy(gameObject, lifeTime);

            if (shotVFX != null) SpawnShotFX();
            if (shotSFX != null) audioSource.PlayOneShot(shotSFX);
        }

        private void SpawnShotFX()
        {
            GameObject shotVFXInstance = Instantiate(shotVFX, transform.position, transform.rotation);
            //shotVFXInstance.transform.forward = transform.forward; // Set direction
            
            float destroyDelay = GetLongestEffectDuration(shotVFXInstance);
            Destroy(shotVFXInstance, destroyDelay);
        }

        // Used to know when to destroy a gameobject that has finished all the associated VFX
        private float GetLongestEffectDuration(GameObject source)
        {
            ParticleSystem[] particleSystems = source.GetComponentsInChildren<ParticleSystem>();
            
            float longestDuration = 0;

            foreach(var ps in particleSystems)
            {
                if (ps.main.duration > longestDuration)
                {
                    longestDuration = ps.main.duration;
                }
            }

            return longestDuration;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (collided == true) return;
            collided = true;

            if (hitVFX != null) SpawnHitVFX(other.GetContact(0));
            if (hitSFX != null) audioSource.PlayOneShot(hitSFX);
            DestroyTrails();

            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            DealDamage(other.gameObject);
            Destroy(gameObject);
        }

        // Some trails are destroyed when projectile collides
        private void DestroyTrails()
        {
            if (trails.Count > 0)
            {
                for (int i = 0; i < trails.Count; i++)
                {
                    var ps = trails[i].GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Stop();
                        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                    }
                }
            }
        }

        private void SpawnHitVFX(ContactPoint contactPoint)
        {
            Quaternion hitRot = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
            Vector3 hitPos = contactPoint.point;

            GameObject hitVFXInstance = Instantiate(hitVFX, hitPos, hitRot);

            float destroyDelay = GetLongestEffectDuration(hitVFXInstance);
            Destroy(hitVFXInstance, destroyDelay);
        }

        private void DealDamage(GameObject target)
        {
            Health health = target.GetComponentInParent<Health>();
            if (health == null) return;

            health.TakeDamage(30);
        }

    }
}

