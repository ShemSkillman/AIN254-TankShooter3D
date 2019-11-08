using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;

namespace TankShooter.Control
{
    public class GunnerController : MonoBehaviour
    {
        [SerializeField] Camera gunnerCamera;
        [Range(0f, 1f)]
        [SerializeField] float mouseSensitivity = 1f;

        [Header("Camera zoom properties")]
        [SerializeField] float zoomSensitivity = 5;
        [SerializeField] float minFov = 10;
        [SerializeField] float maxFov = 120;        

        GunElevate gunElevate;
        TurretRotate turretRotate;
        GunShoot gunShoot;

        private void Start()
        {
            gunElevate = GetComponentInChildren<GunElevate>();
            turretRotate = GetComponentInChildren<TurretRotate>();
            gunShoot = GetComponentInChildren<GunShoot>();
        }

        private void FixedUpdate()
        {
            ControlTurretRotation();
            ControlGunElevation();                 
        }

        private void Update()
        {            
            CameraZoom();
            FireGun();
        }        

        private void ControlTurretRotation()
        {
            turretRotate.RotateTurret(Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * mouseSensitivity);
        }

        private void ControlGunElevation()
        {
            gunElevate.ElevateGun(Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * mouseSensitivity);
        }

        private void CameraZoom()
        {
            gunnerCamera.fieldOfView = Mathf.Clamp(gunnerCamera.fieldOfView - (Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity),
                            minFov, maxFov);
        }

        private void FireGun()
        {
            if (Input.GetButton("Fire1"))
                gunShoot.ShootGun();
        }
    }
}

