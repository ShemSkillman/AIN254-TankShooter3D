using UnityEngine;

namespace TankShooter.Movement
{
    public class TankMove : MonoBehaviour
    {
        [SerializeField] float moveForce = 1000f;
        [SerializeField] float rotationSpeed = 1000f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void MoveTank(float direction)
        {
            rb.AddForce(transform.forward * direction * moveForce);
        }

        public void TurnTank(float direction)
        {
            rb.AddTorque(Vector3.up * direction * rotationSpeed);
        }

        // Returns true when tank is not facing the target point
        public bool TurnTankTowards(Vector3 point, float tolerance)
        {            
            Vector3 difference = point - transform.position;

            float dot = Vector3.Dot(transform.right, difference.normalized);

            if (Mathf.Abs(dot) < tolerance) return false;

            TurnTank(Mathf.Sign(dot));
            return true;
        }

        public bool MoveTankTowards(Vector3 point, float tolerance)
        {
            Vector3 difference = point - transform.position;

            if (AtPoint(point, tolerance)) return false;

            MoveTank(Mathf.Sign(difference.magnitude));
            return true;
        }

        private bool AtPoint(Vector3 point, float tolerance)
        {
            Vector2 tankPos = new Vector3(transform.position.x, transform.position.z);
            Vector2 waypointPos = new Vector3(point.x, point.z);

            float distance = Vector2.Distance(tankPos, waypointPos);

            if (distance < tolerance) return true;

            return false;
        }
    }
}
