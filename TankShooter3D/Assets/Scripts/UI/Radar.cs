using UnityEngine;
using UnityEngine.UI;

namespace TankShooter.UI
{
    public class Radar : MonoBehaviour
    {
        [SerializeField] RectTransform radarTank;
        [SerializeField] Transform tank;

        [SerializeField] RectTransform radarTurret;
        [SerializeField] Transform turret;

        private void Update()
        {
            radarTank.rotation = new Quaternion(radarTank.rotation.x, radarTank.rotation.y, -tank.rotation.y, tank.rotation.w);
            radarTurret.rotation = new Quaternion(radarTurret.rotation.x, radarTurret.rotation.y, -turret.rotation.y, turret.rotation.w);            
        }
    }
}

