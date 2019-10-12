using UnityEngine;

namespace TankShooter.Control
{
    public class GunElevate : MonoBehaviour
    {
        [SerializeField] Camera gunnerCamera;
        [SerializeField] Transform gunStart;
        [SerializeField] float gunElevateSpeed = 10f;

        private void Update()
        {
            float delta = Mathf.MoveTowardsAngle(transform.eulerAngles.x, gunnerCamera.transform.eulerAngles.x, 
                Time.deltaTime * gunElevateSpeed) - transform.eulerAngles.x;

            transform.RotateAround(gunStart.position, transform.right, delta);
        }
    }
}

