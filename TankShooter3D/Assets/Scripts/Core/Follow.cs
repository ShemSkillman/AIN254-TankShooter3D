using UnityEngine;

namespace TankShooter.Core
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] Transform followObject;
        [SerializeField] bool followPosition = true;
        [SerializeField] bool followRotation = true;

        Vector3 offset;

        private void Start()
        {
            offset = transform.position - followObject.position;
        }

        private void Update()
        {
            if (followObject == null) return;

            if (followPosition)
                transform.position = followObject.position + offset;

            if (followRotation)
                transform.rotation = followObject.rotation;
        }
    }
}

