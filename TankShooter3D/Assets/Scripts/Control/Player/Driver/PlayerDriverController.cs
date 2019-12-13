using UnityEngine;
using TankShooter.Movement;
using TankShooter.Combat;

namespace TankShooter.Control
{
    public class PlayerDriverController : MonoBehaviour
    {
        TankMove tankMove;
        DriverAim driverAim;
        
        bool isDead = false;

        private void Awake()
        {
            tankMove = GetComponentInChildren<TankMove>();
            driverAim = GetComponentInChildren<DriverAim>();
        }

        public void Accelerate(float input)
        {
            if (isDead) return;

            input = Mathf.Clamp(input, -1f, 1f);
            tankMove.MoveTank(input);
        }

        public void Steer(float input)
        {
            if (isDead) return;

            input = Mathf.Clamp(input, -1f, 1f);
            tankMove.TurnTank(input);
        }

        public void MoveReticle(Vector2 movement)
        {
            if (isDead) return;

            driverAim.MoveReticle(movement);
        }

        public void ShootTurrets(float input)
        {
            if (isDead) return;

            driverAim.ShootTurrets(input);
        }

        public void Die()
        {
            isDead = true;
        }
    }
}

