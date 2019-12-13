using UnityEngine;

namespace TankShooter.Control
{
    public class MKbDriverController : MonoBehaviour
    {
        PlayerDriverController driverController;

        private void Awake()
        {
            driverController = GetComponent<PlayerDriverController>();
        }

        private void FixedUpdate()
        {
            driverController.Accelerate(Input.GetAxis("Vertical"));
            driverController.Steer(Input.GetAxis("Horizontal"));
            driverController.MoveReticle(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }

        private void Update()
        {
            driverController.ShootTurrets(Input.GetAxis("Fire1"));
        }
    }
}

