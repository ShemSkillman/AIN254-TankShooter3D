using System.Collections;
using UnityEngine;

namespace TankShooter.Control
{
    public class TankHover : MonoBehaviour
    {
        [SerializeField] float maxHoverForce = 1500f;
        [SerializeField] float targetHeight = 2f;

        Rigidbody rb;
        float height = 0f;
        Vector3[] thrustersPos;

        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
            GetThrusters();

            StartCoroutine(CalculateHeight());
        }

        private void GetThrusters()
        {
            Transform[] children = GetComponentsInChildren<Transform>();
            thrustersPos = new Vector3[children.Length];

            for (int i = 0; i < children.Length; i++)
            {
                thrustersPos[i] = children[i].position;
            }
        }

        private void FixedUpdate()
        {
            rb.AddForce(Vector3.up * maxHoverForce * (targetHeight / height));
        }

        IEnumerator CalculateHeight()
        {
            int layerMask = 1 << 13;
            while (true)
            {
                Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask);
                height = hit.distance;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

