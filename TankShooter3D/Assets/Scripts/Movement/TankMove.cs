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
    }
}
