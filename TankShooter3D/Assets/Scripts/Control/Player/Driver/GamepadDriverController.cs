using System.Collections;
using UnityEngine;

namespace TankShooter.Control
{
    public class GamepadDriverController : MonoBehaviour
    {
        Coroutine shooting;

        DriverControls gamepadControl;
        PlayerDriverController driverController;

        Vector2 moveAndSteerInput;
        Vector2 aimInput;

        private void Awake()
        {
            driverController = GetComponent<PlayerDriverController>();

            gamepadControl = new DriverControls();

            gamepadControl.Gameplay.MoveAndSteer.performed += ctx => moveAndSteerInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.MoveAndSteer.canceled += ctx => moveAndSteerInput = Vector2.zero;

            gamepadControl.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.Aim.canceled += ctx => aimInput = Vector2.zero;

            gamepadControl.Gameplay.Startshoot.performed += ctx => shooting = StartCoroutine(Shooting());
            gamepadControl.Gameplay.Endshoot.performed += ctx => StopAllCoroutines();
        }

        private void OnEnable()
        {
            gamepadControl.Gameplay.Enable();
        }

        private void OnDisable()
        {
            gamepadControl.Gameplay.Disable();
        }

        IEnumerator Shooting()
        {
            while (true)
            {
                yield return null;
                driverController.ShootTurrets(1f);
            }
        }

        private void FixedUpdate()
        {
            driverController.Accelerate(moveAndSteerInput.y);
            driverController.Steer(moveAndSteerInput.x);
            driverController.MoveReticle(aimInput);
        }
    }
}

