using UnityEngine;

namespace TankShooter.Movement
{
    public class GunElevate : MonoBehaviour
    {
        [SerializeField] float gunElevateSpeed = 10f;
        [SerializeField] Transform tankGun;
        [SerializeField] float tolerance = 0.01f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void ElevateGun(float direction)
        {
            rb.AddTorque(transform.up * direction * gunElevateSpeed * rb.mass);
        }

        public void RotateGunTowards(Vector3 point)
        {
            Vector3 targetDirection = point - tankGun.position;

            targetDirection = new Vector3(0, targetDirection.y, targetDirection.z).normalized;
            Vector3 currentDirection = new Vector3(0, tankGun.up.y, tankGun.up.z);

            float dot = Vector3.Dot(currentDirection, targetDirection);
            print(dot);

            if (Mathf.Abs(dot) < tolerance) return;

            ElevateGun(Mathf.Sign(dot));
            return;
        }
    }
}

