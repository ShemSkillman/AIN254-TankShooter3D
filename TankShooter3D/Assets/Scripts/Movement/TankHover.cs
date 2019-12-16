using System.Collections;
using UnityEngine;

namespace TankShooter.Movement
{
    public class TankHover : MonoBehaviour
    {
        [SerializeField] float maxHoverForce = 15f;
        [SerializeField] float targetHeight = 2f;
        [SerializeField] GameObject thrusterEffect;

        Rigidbody rb;
        Vector3 groundDir;

        Transform[] thrusters;
        LayerMask terrainLayer;
        bool isPowered = true;

        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
            thrusters = GetComponentsInChildren<Transform>();

            terrainLayer = LayerMask.GetMask("Terrain");
        }

        private void FixedUpdate()
        {
            if (!isPowered) return;

            Vector3 averageNormal = Vector3.zero;

            for (int i = 0; i < thrusters.Length; i++)
            {
                Physics.Raycast(thrusters[i].position, Vector3.down, out RaycastHit hit, targetHeight, terrainLayer);

                if (hit.collider == null)
                {                  
                    continue;
                }

                averageNormal += hit.normal;

                rb.AddForceAtPosition(hit.normal * maxHoverForce * (1f - (hit.distance / targetHeight)), thrusters[i].position, ForceMode.Acceleration);
            }

            groundDir = Vector3.Cross(rb.transform.right, averageNormal.normalized);
        }

        public void Die()
        {
            thrusterEffect.SetActive(false);
            isPowered = false;
        }

        public Vector3 GetGroundDirection()
        {
            return groundDir;
        }
    }
}

