using UnityEngine;

namespace TankShooter.Movement
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] float turretRotationForce = 10f;

        [Header("Rotate towards accuracy")]
        [Range(0f, 1f)] [SerializeField] float tolerance = 0.01f;
        [Range(0f, 1f)] [SerializeField] float smoothing = 0.1f;

        Rigidbody rb;
        HingeJoint hj;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            hj = GetComponent<HingeJoint>();
        }

        public void RotateTurret(float input)
        {
            rb.AddTorque(transform.up * input * turretRotationForce, ForceMode.VelocityChange);
        }

        public void RotateTurretTowards(Vector3 point)
        {
            Vector3 turretPos = transform.position + hj.anchor;

            Vector3 targetDirection = point - turretPos;
            
            float dot = Vector3.Dot(transform.right, targetDirection.normalized);
            float positiveDot = Mathf.Abs(dot);
            
            if (positiveDot < tolerance) return;

            float direction = Mathf.Sign(dot);
            if (positiveDot < smoothing) direction *= (positiveDot / smoothing);

            RotateTurret(direction);
            return;
        }
    }
}

