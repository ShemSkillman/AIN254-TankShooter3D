using UnityEngine;

namespace TankShooter.Core.Pickup
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") return;

            GameObject player = other.GetComponent<Health>().gameObject;
            PickUp(player);
        }

        protected abstract void PickUp(GameObject player);
    }
}

