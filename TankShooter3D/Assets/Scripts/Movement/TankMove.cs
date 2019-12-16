using UnityEngine;

namespace TankShooter.Movement
{
    public class TankMove : MonoBehaviour
    {
        [SerializeField] float moveForce = 10f;
        [SerializeField] float rotationSpeed = 20f;

        Rigidbody rb;
        TankHover tankHover;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            tankHover = GetComponentInChildren<TankHover>();
        }

        public void MoveTank(float input)
        {
            rb.AddForce(tankHover.GetGroundDirection() * input * moveForce, ForceMode.Acceleration);
        }        

        public void TurnTank(float input)
        {
            rb.AddTorque(Vector3.up * input * rotationSpeed, ForceMode.Acceleration);
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

        // Returns true when tank is not at the target destination
        public bool MoveTankTowards(Vector3 point, float tolerance)
        {
            Vector3 difference = point - transform.position;

            if (AtPoint(point, tolerance)) return false;

            float direction = Vector3.Dot(transform.forward, difference.normalized);

            MoveTank(Mathf.Sign(direction));
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

        public float GetTankSpeedKmpH()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            float speedKmpH = (localVelocity.magnitude / 1000) * 3600 * Mathf.Sign(localVelocity.z);

            return speedKmpH;
        }
    }
}
