using UnityEngine;

namespace TankShooter.Core.Pickup
{
    public abstract class Pickup : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            Health player = other.GetComponentInParent<Health>();
            if (player == null || player.tag != "Player") return;

            PickUp(player.gameObject);
        }

        protected abstract void PickUp(GameObject player);
    }
}

