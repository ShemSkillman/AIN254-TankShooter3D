using UnityEngine;

namespace TankShooter.Control
{
    public class GamepadGunnerController : MonoBehaviour
    {
        GunnerControls gamepadControl;
        PlayerGunnerController gunnerController;

        Vector2 aimInput;

        private void Awake()
        {
            gamepadControl = new GunnerControls();
            gunnerController = GetComponent<PlayerGunnerController>();

            gamepadControl.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.Aim.canceled += ctx => aimInput = Vector2.zero;

            gamepadControl.Gameplay.ZoomOut.performed += ctx => gunnerController.CameraZoom(ctx.ReadValue<float>());
            gamepadControl.Gameplay.ZoomIn.performed += ctx => gunnerController.CameraZoom(-ctx.ReadValue<float>());

            gamepadControl.Gameplay.Shoot.performed += ctx => gunnerController.FireGun(ctx.ReadValue<float>() != 0);
        }

        private void OnEnable()
        {
            gamepadControl.Gameplay.Enable();
        }

        private void OnDisable()
        {
            gamepadControl.Gameplay.Disable();
        }

        private void FixedUpdate()
        {
            gunnerController.ControlTurretRotation(aimInput.x);
            gunnerController.ControlGunElevation(aimInput.y);
        }

    }
}

