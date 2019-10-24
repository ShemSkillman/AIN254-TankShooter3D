using UnityEngine;

namespace TankShooter.Movement
{
    public class GunElevate : MonoBehaviour
    {
        [SerializeField] float gunElevateSpeed = 10f;

        [Header("Elevate towards accuracy")]
        [Range(0f, 1f)] [SerializeField] float tolerance = 0.01f;
        [Range(0f, 1f)] [SerializeField] float smoothing = 0.1f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void ElevateGun(float direction)
        {
            rb.AddTorque(transform.up * direction * gunElevateSpeed * rb.mass);
        }

        public void ElevateGunTowards(Vector3 point)
        {            
            Vector3 targetDirection = point - transform.position;

            float dot = Vector3.Dot(transform.up, targetDirection.normalized);
            float positiveDot = Mathf.Abs(dot);

            if (positiveDot < tolerance) return;

            float direction = Mathf.Sign(dot);
            if (positiveDot < smoothing) direction *= (positiveDot / smoothing);

            ElevateGun(direction);
            return;
        }
    }
}

