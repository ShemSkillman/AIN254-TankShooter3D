using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;
using TankShooter.UI;
using Cinemachine;

namespace TankShooter.Control
{
    public class PlayerGunnerController : MonoBehaviour
    {
        [SerializeField] Camera gunnerCamera;
        [SerializeField] GameObject mainWeapon;
        [Range(0f, 1f)]
        [SerializeField] float normalAimSensitivity = 1f;
        [Range(0f, 1f)]
        [SerializeField] float targetLockAimSensivity = 0.25f;

        [Header("Third person camera")]
        [SerializeField] Camera thirdPersonCamera;
        [SerializeField] CinemachineFreeLook freeLook;
        [SerializeField] float maxAimDistance = 100f;

        [Header("Camera zoom properties")]
        float fovChange;
        [SerializeField] int zoomIncrements = 3;
        [SerializeField] float minFov = 10;
        [SerializeField] float maxFov = 120;        

        GunElevate gunElevate;
        TurretRotate turretRotate;
        GunShoot gunShoot;
        TargetSystem targetSystem;

        LayerMask aimMask;
        float aimSensitivity;
        bool isDead = false;
        bool isThirdPerson = false;

        private void Awake()
        {
            gunElevate = mainWeapon.GetComponentInChildren<GunElevate>();
            turretRotate = mainWeapon.GetComponentInChildren<TurretRotate>();
            gunShoot = mainWeapon.GetComponentInChildren<GunShoot>();
            targetSystem = GetComponentInChildren<TargetSystem>();
        }

        protected void Start()
        {
            fovChange = (maxFov - minFov) / zoomIncrements;
            aimSensitivity = normalAimSensitivity;

            aimMask = LayerMask.GetMask("Terrain", "Default");
        }

        private void OnEnable()
        {
            targetSystem.onTargetLocked += ChangeAimSensitivity;
        }

        private void OnDisable()
        {
            targetSystem.onTargetLocked -= ChangeAimSensitivity;
        }

        private void FixedUpdate()
        {
            if (!isThirdPerson || isDead) return;

            bool isHit = Physics.Raycast(thirdPersonCamera.transform.position, thirdPersonCamera.transform.forward,
                out RaycastHit hit, maxAimDistance, aimMask);

            if (!isHit) hit.point = thirdPersonCamera.transform.position + (thirdPersonCamera.transform.forward * maxAimDistance);

            turretRotate.RotateTurretTowards(hit.point);
            gunElevate.ElevateGunTowards(hit.point);
        }

        public void ControlTurretRotation(float input)
        {
            if (isDead || isThirdPerson) return;

            input = Mathf.Clamp(input, -1f, 1f);
            turretRotate.RotateTurret(input * aimSensitivity);
        }

        public void ControlGunElevation(float input)
        {
            if (isDead || isThirdPerson) return;

            input = Mathf.Clamp(input, -1f, 1f);
            gunElevate.ElevateGun(input * aimSensitivity);
        }

        public void MoveOrbitalCamera(Vector2 input)
        {
            if (!isThirdPerson)
            {
                freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
                freeLook.m_XAxis.m_InputAxisName = "Mouse X";
                return;
            }

            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisName = "";

            freeLook.m_XAxis.m_InputAxisValue = input.x;
            freeLook.m_YAxis.m_InputAxisValue = input.y;

            print(freeLook.m_XAxis.m_InputAxisValue);
            print(freeLook.m_YAxis.m_InputAxisValue);
        }

        public void CameraZoom(float input)
        {
            if (isDead || isThirdPerson) return;

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
            if (isDead || isThirdPerson) return;

            if (isInput) targetSystem.LockTarget();
        }

        public void Die()
        {
            isDead = true;
        }

        public void ChangeAimSensitivity(bool isTargetLocked)
        {
            if (isTargetLocked) aimSensitivity = targetLockAimSensivity;
            else aimSensitivity = normalAimSensitivity;
        }

        public void SwitchCamera(bool isInput)
        {
            if (!isInput || thirdPersonCamera == null) return;

            isThirdPerson = !isThirdPerson;

            thirdPersonCamera.gameObject.SetActive(isThirdPerson);
            gunnerCamera.gameObject.SetActive(!isThirdPerson);
        }
    }
}

