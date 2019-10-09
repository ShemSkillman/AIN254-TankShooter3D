using UnityEngine;

namespace TankShooter.Control
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float rotationSpeed = 5f;

        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed, Space.Self);
            transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Horizontal") * rotationSpeed);
        }
    }
}
