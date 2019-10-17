using UnityEngine;

namespace TankShooter.Control
{
    public class TankHover : MonoBehaviour
    {
        [SerializeField] float maxHoverForce = 1500f;
        [Range(0f, 1f)]
        [SerializeField] float reduceHoverMult = 0.9f;
        [SerializeField] float targetHeight = 2f;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float height = transform.position.y;
            float hoverForce = maxHoverForce;
            
            if (height > targetHeight)
            {
                hoverForce *= reduceHoverMult;
            }

            rb.AddForce(Vector3.up * hoverForce);

        }
    }
}

