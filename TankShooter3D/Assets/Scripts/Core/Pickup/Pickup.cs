using UnityEngine;

namespace TankShooter.Core.Pickup
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag != "Player") return;

            GameObject player = other.GetComponentInParent<Health>().gameObject;
            PickUp(player);
        }

        protected abstract void PickUp(GameObject player);
    }
}

