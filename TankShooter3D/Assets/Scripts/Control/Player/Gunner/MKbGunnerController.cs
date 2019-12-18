using UnityEngine;

namespace TankShooter.Control
{
    public class MKbGunnerController : MonoBehaviour
    {
        PlayerGunnerController gunnerController;

        private void Awake()
        {
            gunnerController = GetComponent<PlayerGunnerController>();
        }

        private void FixedUpdate()
        {
            gunnerController.ControlTurretRotation(Input.GetAxis("Mouse X"));
            gunnerController.ControlGunElevation(Input.GetAxis("Mouse Y"));
        }

        private void Update()
        {
            gunnerController.CameraZoom(Input.GetAxisRaw("Mouse ScrollWheel"));
            gunnerController.FireGun(Input.GetButton("Fire1"));
            gunnerController.LockTarget(Input.GetKeyDown(KeyCode.T));
        }
    }
}

