using UnityEngine;

namespace TankShooter.Movement
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] float turretRotationSpeed = 10f;

        public void RotateTurret(float turretRotationDegrees)
        {
            Quaternion targetRot = Quaternion.Euler(0, turretRotationDegrees, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot,
                Time.deltaTime * turretRotationSpeed);
        }
    }
}

