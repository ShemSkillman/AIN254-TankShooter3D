using UnityEngine;

namespace TankShooter.Control
{
    public class CameraSwitch : MonoBehaviour
    {
        [SerializeField] Camera flyCamera;
        Camera[] cameras;
        bool flyCamActive = false;

        private void Start()
        {
            cameras = Camera.allCameras;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                flyCamActive = !flyCamActive;

                foreach (Camera camera in cameras)
                {
                    camera.gameObject.SetActive(!flyCamActive);
                }

                flyCamera.gameObject.SetActive(flyCamActive);
            }
        }
    }
}

