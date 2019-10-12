using UnityEngine;

namespace TankShooter.Control
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] Camera gunnerCam;
        [SerializeField] float turretRotationSpeed = 10f;

        private void Update()
        {
            Quaternion cameraRot = Quaternion.Euler(0, gunnerCam.transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraRot,
                Time.deltaTime * turretRotationSpeed);                
        }
    }
}

