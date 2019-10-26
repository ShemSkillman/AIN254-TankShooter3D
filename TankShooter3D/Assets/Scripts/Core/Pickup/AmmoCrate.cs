using UnityEngine;
using TankShooter.Combat;

namespace TankShooter.Core.Pickup
{
    public class AmmoCrate : Pickup
    {
        [SerializeField] int ammoStorage = 15;

        protected override void PickUp(GameObject player)
        {
            GunShoot gunShoot = GetComponent<GunShoot>();

            gunShoot.ReplenishAmmo(ammoStorage);
        }
    }
}

