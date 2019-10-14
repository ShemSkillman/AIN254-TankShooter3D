using UnityEngine;

namespace TankShooter.Movement
{
    public class TankMove : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float rotationSpeed = 5f;

        public void MoveTank(float direction)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * direction * moveSpeed, Space.Self);
        }

        public void TurnTank(float direction)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * direction * rotationSpeed);
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
