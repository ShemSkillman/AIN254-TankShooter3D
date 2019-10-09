using UnityEngine;

namespace TankShooter.Control
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] float lookSensitivity = 10f;

        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * lookSensitivity * Input.GetAxis("Mouse X"));
        }
    }
}

