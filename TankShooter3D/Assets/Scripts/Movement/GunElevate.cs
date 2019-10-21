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

            float dot = Vector3.Dot(tankGun.up, targetDirection.normalized);
            print(dot);

            if (Mathf.Abs(dot) < tolerance) return;

            ElevateGun(Mathf.Sign(dot));
            return;
        }
    }
}

