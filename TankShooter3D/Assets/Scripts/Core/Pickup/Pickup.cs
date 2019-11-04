using UnityEngine;

namespace TankShooter.Core.Pickup
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            GameObject player = other.GetComponentInParent<Health>().gameObject;
            if (player.tag != "Player") return;

            PickUp(player);
        }

        protected abstract void PickUp(GameObject player);
    }
}

