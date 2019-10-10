using UnityEngine;

namespace TankShooter.Control
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] Camera gunnerCam;
        [SerializeField] float turretRotationSpeed = 10f;

        private void Update()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, gunnerCam.transform.rotation, 
                Time.deltaTime * turretRotationSpeed);
        }
    }
}

