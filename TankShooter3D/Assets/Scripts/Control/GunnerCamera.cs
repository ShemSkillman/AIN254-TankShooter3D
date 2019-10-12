using UnityEngine;

namespace TankShooter.Control
{
    public class GunnerCamera : MonoBehaviour
    {
        [Header("Camera rotation properties")]
        [SerializeField] float lookSensitivity = 10f;
        [SerializeField] float minElevation = -25f;
        [SerializeField] float maxElevation = 30f;

        [Header("Camera zoom properties")]
        [SerializeField] float scrollSensitivity = 5;
        [SerializeField] float minFov = 10;
        [SerializeField] float maxFov = 120;

        Camera gunnerCam;
        float currentElevation = 0f;

        private void Start()
        {
            gunnerCam = GetComponent<Camera>();
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * lookSensitivity * Input.GetAxis("Mouse X"), Space.World);

            currentElevation = Mathf.Clamp(currentElevation - (Time.deltaTime * lookSensitivity * Input.GetAxis("Mouse Y")),
                minElevation, maxElevation);

            transform.eulerAngles = new Vector3(currentElevation, transform.eulerAngles.y, transform.eulerAngles.z);
            

            gunnerCam.fieldOfView = Mathf.Clamp(gunnerCam.fieldOfView - (Input.GetAxisRaw("Mouse ScrollWheel") * scrollSensitivity), 
                minFov, maxFov);
        }
    }
}

