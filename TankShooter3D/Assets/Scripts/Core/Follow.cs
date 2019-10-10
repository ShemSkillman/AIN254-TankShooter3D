using UnityEngine;

namespace TankShooter.Control
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] Transform followObject;

        private void Update()
        {
            transform.position = followObject.position;
        }
    }
}

