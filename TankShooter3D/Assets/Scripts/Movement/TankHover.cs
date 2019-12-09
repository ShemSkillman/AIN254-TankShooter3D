using System.Collections;
using UnityEngine;

namespace TankShooter.Control
{
    public class TankHover : MonoBehaviour
    {
        [SerializeField] float maxHoverForce = 15f;
        [SerializeField] float targetHeight = 2f;
        [SerializeField] GameObject thrusterEffect;

        Rigidbody rb;

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

            for (int i = 0; i < thrusters.Length; i++)
            {
                Physics.Raycast(thrusters[i].position, Vector3.down, out RaycastHit hit, targetHeight, terrainLayer);

                if (hit.collider == null) continue;

                rb.AddForceAtPosition(Vector3.up * maxHoverForce * rb.mass * (1f - (hit.distance / targetHeight)), thrusters[i].position);
            }
        }

        public void Die()
        {
            print("thrustersdown");
            thrusterEffect.SetActive(false);
            isPowered = false;
        }
    }
}

