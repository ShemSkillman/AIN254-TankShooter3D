using System.Collections;
using UnityEngine;

namespace TankShooter.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float lifeTime = 10f;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}

