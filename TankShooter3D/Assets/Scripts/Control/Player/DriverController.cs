using UnityEngine;
using TankShooter.Movement;
using UnityEngine.InputSystem;

namespace TankShooter.Control
{
    public class DriverController : Controller
    {
        TankMove tankMove;
        DriverControls gamepadControl;

        Vector2 moveInput;
        Vector2 steerInput;

        private void Awake()
        {
            tankMove = GetComponentInChildren<TankMove>();

            gamepadControl = new DriverControls();

            gamepadControl.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

            gamepadControl.Gameplay.Steer.performed += ctx => steerInput = ctx.ReadValue<Vector2>();
            gamepadControl.Gameplay.Steer.canceled += ctx => steerInput = Vector2.zero;
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
            Accelerate(Input.GetAxis("Vertical"));
            Steer(Input.GetAxis("Horizontal"));

            Accelerate(moveInput.y);
            Steer(steerInput.x);
        }

        private void Accelerate(float input)
        {
            tankMove.MoveTank(input);
        }

        private void Steer(float input)
        {
            tankMove.TurnTank(input);
        }
    }
}

