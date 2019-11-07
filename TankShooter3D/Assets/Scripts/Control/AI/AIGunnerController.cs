using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;
using TankShooter.Core;
using System.Collections;

namespace TankShooter.Control
{
    public class AIGunnerController : MonoBehaviour
    {
        [SerializeField] Transform gunEnd;
        [SerializeField] float shootRange = 10f;
        [SerializeField] float sightRange = 20f;

        TurretRotate turretRotate;
        AIDriverController driver;
        GunElevate gunElevate;
        GunShoot gunShoot;

        Transform target;
        LayerMask targetMask;

        float timeSinceTargetInSight = 0f;
        float stoppingDistance;
        [SerializeField] float maxWaitTime = 6f;
        [SerializeField] float minStoppingDistance = 5f;

        private void Start()
        {
            turretRotate = GetComponentInChildren<TurretRotate>();
            gunElevate = GetComponentInChildren<GunElevate>();
            gunShoot = GetComponentInChildren<GunShoot>();
            driver = GetComponent<AIDriverController>();

            targetMask = LayerMask.GetMask("Player");
            stoppingDistance = UnityEngine.Random.Range(minStoppingDistance, shootRange);

            StartCoroutine(ScanForTargets());
        }

        private void Update()
        {
            IdentifyTarget();
            CheckTargetLost();
        }

        private void FixedUpdate()
        {
            ControlTurret();
            ControlGun();
        }

        private void CheckTargetLost()
        {
            timeSinceTargetInSight += Time.deltaTime;
            if (timeSinceTargetInSight > maxWaitTime)
            {
                driver.SetIsStationary(false);
            }
        }

        IEnumerator ScanForTargets()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);

                driver.SetToFollow(target);

                Collider[] targets = Physics.OverlapSphere(transform.position, sightRange, targetMask);
                if (targets.Length == 0)
                {
                    target = null;
                    continue;
                }

                target = targets[UnityEngine.Random.Range(0, targets.Length)].transform;
            }            
        }

        private void ControlTurret()
        {
            if (target == null) return;

            turretRotate.RotateTurretTowards(target.position);
        }

        private void ControlGun()
        {
            if (target == null) return;

            gunElevate.ElevateGunTowards(target.position);
        }

        private void IdentifyTarget()
        {
            bool hit = Physics.Raycast(gunEnd.position, gunEnd.forward, out RaycastHit ray, shootRange);
            if (!hit) return;

            Health health = ray.collider.gameObject.GetComponentInParent<Health>();
            if (health != null  && health.gameObject.tag == "Player")
            {
                if(ray.distance <= stoppingDistance) driver.SetIsStationary(true);
                timeSinceTargetInSight = 0f;
                gunShoot.ShootGun();
            }
        }
    }
}

