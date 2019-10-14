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
    }
}
