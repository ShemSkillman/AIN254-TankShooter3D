using UnityEngine;

namespace TankShooter.Control
{
    public class MasterController : MKbGunnerController
    {
        MasterControls gamepadControl;
        PlayerDriverController driverController;

        Vector2 moveAndSteerInput;
        Vector2 aimInput;

        protected override void Awake()
        {
            base.Awake();

            gunnerController = GetComponent<PlayerGunnerController>();
            driverController = GetComponent<PlayerDriverController>();

            gamepadControl = new MasterControls();

            gamepadControl.Gameplay.MoveAndSteer.performed += ctx => moveAndSteerInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.MoveAndSteer.canceled += ctx => moveAndSteerInput = Vector2.zero;

            gamepadControl.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.Aim.canceled += ctx => aimInput = Vector2.zero;

            gamepadControl.Gameplay.LockTarget.performed += ctx => gunnerController.LockTarget(ctx.ReadValue<float>() != 0);

            gamepadControl.Gameplay.ShootGun.performed += ctx => gunnerController.FireGun(ctx.ReadValue<float>() != 0);

            gamepadControl.Gameplay.ZoomOut.performed += ctx => gunnerController.CameraZoom(ctx.ReadValue<float>());
            gamepadControl.Gameplay.ZoomIn.performed += ctx => gunnerController.CameraZoom(-ctx.ReadValue<float>());

            gamepadControl.Gameplay.SwitchCamera.performed += ctx => gunnerController.SwitchCamera(ctx.ReadValue<float>() != 0);
        }

        private void OnEnable()
        {
            gamepadControl.Gameplay.Enable();
        }

        private void OnDisable()
        {
            gamepadControl.Gameplay.Disable();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            driverController.Accelerate(Input.GetAxis("Vertical"));
            driverController.Steer(Input.GetAxis("Horizontal"));

            driverController.Accelerate(moveAndSteerInput.y);
            driverController.Steer(moveAndSteerInput.x);

            gunnerController.ControlGunElevation(aimInput.y);
            gunnerController.ControlTurretRotation(aimInput.x);

            gunnerController.MoveOrbitalCamera(aimInput);
        }

        protected override void Update()
        {
            base.Update();
            gunnerController.SwitchCamera(Input.GetKeyDown(KeyCode.Space));
        }
    }
}

