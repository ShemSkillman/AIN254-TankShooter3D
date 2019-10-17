using UnityEngine;

namespace TankShooter.Control
{
    public class CameraSwitch : MonoBehaviour
    {
        [SerializeField] Camera flyCamera;
        [SerializeField] Canvas mainCanvas;

        [SerializeField] Camera[] otherCameras;
        bool flyCamActive = false;


        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                flyCamActive = !flyCamActive;
                mainCanvas.gameObject.SetActive(!flyCamActive);

                foreach (Camera camera in otherCameras)
                {
                    camera.gameObject.SetActive(!flyCamActive);
                }

                flyCamera.gameObject.SetActive(flyCamActive);
            }
        }
    }
}

