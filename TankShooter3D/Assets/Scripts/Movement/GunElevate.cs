using UnityEngine;

namespace TankShooter.Movement
{
    public class GunElevate : MonoBehaviour
    {
        [SerializeField] Transform gunStart;
        [SerializeField] float gunElevateSpeed = 10f;

        public void ElevateGun(float elevationDegrees)
        {
            float delta = Mathf.MoveTowardsAngle(transform.eulerAngles.x, elevationDegrees,
                Time.deltaTime * gunElevateSpeed) - transform.eulerAngles.x;

            transform.RotateAround(gunStart.position, transform.right, delta);
        }
    }
}

