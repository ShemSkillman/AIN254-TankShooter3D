using UnityEngine;
using TankShooter.Combat;

namespace TankShooter.Core.Pickup
{
    public class AmmoCrate : Pickup
    {
        [SerializeField] int ammoStorage = 15;

        protected override void PickUp(GameObject player)
        {
            GunShoot gunShoot = player.GetComponentInChildren<GunShoot>();
            if (gunShoot == null) return;

            ammoStorage = gunShoot.ReplenishAmmo(ammoStorage);
            if (ammoStorage < 1) Destroy(gameObject);
        }
    }
}

