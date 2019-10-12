using UnityEngine;

namespace TankShooter.Core
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

