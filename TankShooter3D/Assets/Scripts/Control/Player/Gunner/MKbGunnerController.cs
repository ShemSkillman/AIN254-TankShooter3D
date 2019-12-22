using UnityEngine;

namespace TankShooter.Control
{
    public class MKbGunnerController : MonoBehaviour
    {
        protected PlayerGunnerController gunnerController;

        protected virtual void Awake()
        {
            gunnerController = GetComponent<PlayerGunnerController>();
        }

        protected virtual void FixedUpdate()
        {
            gunnerController.ControlTurretRotation(Input.GetAxis("Mouse X"));
            gunnerController.ControlGunElevation(Input.GetAxis("Mouse Y"));
        }

        protected virtual void Update()
        {
            gunnerController.CameraZoom(Input.GetAxisRaw("Mouse ScrollWheel"));
            gunnerController.FireGun(Input.GetButton("Fire1"));
            gunnerController.LockTarget(Input.GetKeyDown(KeyCode.T));
        }
    }
}

