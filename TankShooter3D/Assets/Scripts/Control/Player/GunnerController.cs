using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;

namespace TankShooter.Control
{
    public class GunnerController : MonoBehaviour
    {
        [SerializeField] Camera gunnerCamera;

        [Header("Camera rotation properties")]
        [SerializeField] float lookSensitivity = 10f;
        [SerializeField] float minElevation = -25f;
        [SerializeField] float maxElevation = 30f;

        [Header("Camera zoom properties")]
        [SerializeField] float zoomSensitivity = 5;
        [SerializeField] float minFov = 10;
        [SerializeField] float maxFov = 120;

        GunElevate gunElevate;
        TurretRotate turretRotate;
        GunShoot gunShoot;
        Transform camTransform;

        float currentElevation = 0f;

        private void Start()
        {
            gunElevate = GetComponentInChildren<GunElevate>();
            turretRotate = GetComponentInChildren<TurretRotate>();
            gunShoot = GetComponentInChildren<GunShoot>();
            camTransform = gunnerCamera.transform;
        }

        private void Update()
        {
            HorizontalCameraRotation();
            VerticalCameraRotation();
            CameraZoom();
            FireGun();
        }        

        private void HorizontalCameraRotation()
        {
            camTransform.Rotate(Vector3.up * Time.deltaTime * lookSensitivity * Input.GetAxis("Mouse X"), Space.World);

            turretRotate.RotateTurret(camTransform.eulerAngles.y);
        }

        private void VerticalCameraRotation()
        {
            currentElevation = Mathf.Clamp(currentElevation - (Time.deltaTime * lookSensitivity * Input.GetAxis("Mouse Y")),
                            minElevation, maxElevation);
            camTransform.eulerAngles = new Vector3(currentElevation, camTransform.eulerAngles.y, camTransform.eulerAngles.z);

            gunElevate.ElevateGun(currentElevation);
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

