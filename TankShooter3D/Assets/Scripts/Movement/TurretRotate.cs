using UnityEngine;

namespace TankShooter.Movement
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] float turretRotationSpeed = 10f;
        [SerializeField] float tolerance = 0.01f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void RotateTurret(float direction)
        {
            rb.AddTorque(transform.up * direction * turretRotationSpeed * rb.mass);
        }

        public bool RotateTurretTowards(Vector3 point)
        {
            Vector3 targetDirection = point - transform.position;

            targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;
            Vector3 currentDirection = new Vector3(transform.right.x, 0, transform.right.z);
            
            float dot = Vector3.Dot(currentDirection, targetDirection);
            
            if (Mathf.Abs(dot) < tolerance) return false;

            RotateTurret(Mathf.Sign(dot));
            return true;
        }
    }
}

