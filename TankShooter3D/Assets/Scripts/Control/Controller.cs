using UnityEngine;

namespace TankShooter.Control
{
    public abstract class Controller : MonoBehaviour
    {
        protected void Die()
        {
            Destroy(this);
        }
    }
}

