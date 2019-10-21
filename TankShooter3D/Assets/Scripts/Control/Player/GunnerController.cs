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
        int layerMask;

        private void Start()
        {
            gunElevate = GetComponentInChildren<GunElevate>();
            turretRotate = GetComponentInChildren<TurretRotate>();
            gunShoot = GetComponentInChildren<GunShoot>();

            camTransform = gunnerCamera.transform;

            layerMask = 1 << 9;
            layerMask = ~layerMask;

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

            turretRotate.RotateTurretTowards(GetCameraAimLocation());
        }

        private void VerticalCameraRotation()
        {
            currentElevation = Mathf.Clamp(currentElevation - (Time.deltaTime * lookSensitivity * Input.GetAxis("Mouse Y")),
                            minElevation, maxElevation);
            camTransform.eulerAngles = new Vector3(currentElevation, camTransform.eulerAngles.y, camTransform.eulerAngles.z);

            gunElevate.ElevateGunTowards(GetCameraAimLocation());
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

        private Vector3 GetCameraAimLocation()
        {
            Vector3 rayOrigin = gunnerCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));

            if (Physics.Raycast(rayOrigin, gunnerCamera.transform.forward, out RaycastHit hit, 1000f, layerMask))
            {
                return hit.point;
            }
            else
            {
                return (rayOrigin + (gunnerCamera.transform.forward * 1000f));
            }
        }
    }
}

