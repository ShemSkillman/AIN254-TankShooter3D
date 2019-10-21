using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;
using System;

namespace TankShooter.Control
{
    public class AIGunnerController : MonoBehaviour
    {
        [SerializeField] Transform playerTank;
        [SerializeField] Transform gunEnd;
        [SerializeField] Transform gunStart;
        [SerializeField] float range = 10f;

        TurretRotate turretRotate;
        GunElevate gunElevate;
        GunShoot gunShoot;
        Transform turret;

        private void Start()
        {
            turretRotate = GetComponentInChildren<TurretRotate>();
            gunElevate = GetComponentInChildren<GunElevate>();
            gunShoot = GetComponentInChildren<GunShoot>();
            turret = turretRotate.transform;
        }

        private void Update()
        {
            ControlTurret();
            ControlGun();
        }

        private void ControlTurret()
        {
            if (playerTank == null) return;

            turretRotate.RotateTurretTowards(playerTank.position);
        }

        private void ControlGun()
        {
            gunElevate.RotateGunTowards(playerTank.position);

            bool hit = Physics.Raycast(gunEnd.position, gunEnd.forward, out RaycastHit ray, range);

            if (hit) IdentifyTarget(ray);
        }

        private void IdentifyTarget(RaycastHit ray)
        {
            Health health = ray.collider.gameObject.GetComponentInParent<Health>();
            if (health != null  && health.gameObject.tag == "Player")
            {
                gunShoot.ShootGun();
            }
        }
    }
}

