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
            
            float dot = Vector3.Dot(transform.right, targetDirection.normalized);
            
            if (Mathf.Abs(dot) < tolerance) return false;

            RotateTurret(Mathf.Sign(dot));
            return true;
        }
    }
}

