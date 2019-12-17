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
        [SerializeField] int maxAimIterations = 100;

        TurretRotate turretRotate;
        AIDriverController driver;
        GunElevate gunElevate;
        GunShoot gunShoot;

        Transform target;
        Rigidbody targetRb;
        Health targetHealth;
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

            targetMask = LayerMask.GetMask("Player detection");
            stoppingDistance = UnityEngine.Random.Range(minStoppingDistance, shootRange);

            StartCoroutine(ScanForTargets());
        }

        private void Update()
        {
            CheckTargetLost();
        }

        private void FixedUpdate()
        {
            AimAtTarget();
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

                if (target != null) continue;

                driver.SetToFollow(target);

                Collider[] targetColliders = Physics.OverlapSphere(transform.position, sightRange, targetMask);
                if (targetColliders.Length == 0)
                {
                    target = null;
                    continue;
                }

                foreach(Collider targetCollider in targetColliders) // Enemy tank targets biggest part of player tank, the body
                {
                    Health enemy = targetCollider.GetComponentInParent<Health>();
                    if (enemy.GetIsDead()) continue;

                    target = targetCollider.transform;
                    targetRb = target.GetComponentInParent<Rigidbody>();
                    targetHealth = target.GetComponentInParent<Health>();
                    break;
                }
            }            
        }

        private void AimAtTarget()
        {
            if (target == null) return;

            Vector3 aimPos = GetAim(targetHealth.GetTankPosition(), targetRb.velocity, gunShoot.SelectedProjectile, 0);

            ControlTurret(aimPos);
            ControlGun(aimPos);
            ShootGun(aimPos);
        }

        private Vector3 GetAim(Vector3 targetPos, Vector3 targetVelocity, Projectile projectile, int i)
        {
            i++;

            float distanceToTarget = Vector3.Distance(gunEnd.position, targetPos);
            float timeToReachTarget = distanceToTarget / projectile.ShootForce;

            Vector3 futureTargetPos = target.position + targetVelocity * timeToReachTarget;
            if (targetPos != futureTargetPos && i < maxAimIterations)
            {
                return GetAim(futureTargetPos, targetVelocity, projectile, i);
            }
            else
            {
                return targetPos;
            }
        }

        private void ControlTurret(Vector3 aimPos)
        {
            turretRotate.RotateTurretTowards(aimPos);
        }

        private void ControlGun(Vector3 aimPos)
        {
            gunElevate.ElevateGunTowards(aimPos);
        }

        private void ShootGun(Vector3 targetLocation)
        {
            Vector3 targetDir = (targetLocation - gunEnd.position).normalized;
            float dot =  Vector3.Dot(gunEnd.forward, targetDir);

            if (dot < 0.99f) return;

            float targetLocationDistance = Vector3.Distance(gunEnd.position, targetLocation);
            int layerMask = ~targetMask.value;

            bool isBlocked = Physics.Raycast(gunEnd.position, gunEnd.forward, targetLocationDistance, layerMask);
            
            if (isBlocked) return;

            timeSinceTargetInSight = 0f;
            gunShoot.ShootGun();
        }

        public void Die()
        {
            Destroy(this);
        }
    }
}

