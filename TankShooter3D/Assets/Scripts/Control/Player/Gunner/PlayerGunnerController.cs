using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;
using TankShooter.UI;

namespace TankShooter.Control
{
    public class PlayerGunnerController : MonoBehaviour
    {
        [SerializeField] Camera gunnerCamera;
        [SerializeField] GameObject mainWeapon;
        [Range(0f, 1f)]
        [SerializeField] float aimSensitivity = 1f;

        [Header("Camera zoom properties")]
        float fovChange;
        [SerializeField] int zoomIncrements = 3;
        [SerializeField] float minFov = 10;
        [SerializeField] float maxFov = 120;        

        GunElevate gunElevate;
        TurretRotate turretRotate;
        GunShoot gunShoot;
        TargetSystem targetSystem;

        bool isDead = false;

        protected void Start()
        {
            gunElevate = mainWeapon.GetComponentInChildren<GunElevate>();
            turretRotate = mainWeapon.GetComponentInChildren<TurretRotate>();
            gunShoot = mainWeapon.GetComponentInChildren<GunShoot>();
            targetSystem = GetComponentInChildren<TargetSystem>();

            fovChange = (maxFov - minFov) / zoomIncrements;
        }

        public void ControlTurretRotation(float input)
        {
            if (isDead) return;

            input = Mathf.Clamp(input, -1f, 1f);
            turretRotate.RotateTurret(input * aimSensitivity);
        }

        public void ControlGunElevation(float input)
        {
            if (isDead) return;

            input = Mathf.Clamp(input, -1f, 1f);
            gunElevate.ElevateGun(input * aimSensitivity);
        }

        public void CameraZoom(float input)
        {
            if (isDead) return;

            if (input < 0f)
            {
                gunnerCamera.fieldOfView = Mathf.Clamp(gunnerCamera.fieldOfView + fovChange, minFov, maxFov);
            }
            else if (input > Mathf.Epsilon)
            {
                gunnerCamera.fieldOfView = Mathf.Clamp(gunnerCamera.fieldOfView - fovChange, minFov, maxFov);
            }
            else
            {
                return;
            }            
        }

        public void FireGun(bool isInput)
        {
            if (isDead) return;

            if (isInput) gunShoot.ShootGun();
        }

        public void LockTarget(bool isInput)
        {
            if (isDead) return;

            if (isInput) targetSystem.LockTarget();
        }

        public void Die()
        {
            isDead = true;
        }
    }
}

