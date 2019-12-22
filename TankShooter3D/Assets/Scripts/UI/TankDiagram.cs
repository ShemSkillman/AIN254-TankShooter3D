using UnityEngine;
using TankShooter.Core;
using TankShooter.Movement;

namespace TankShooter.UI
{
    public class TankDiagram : MonoBehaviour
    {
        [SerializeField] RectTransform radarTank;
        [SerializeField] RectTransform radarTurret;

        Transform player;
        Transform tankBodyTransform;
        Transform tankTurretTransform;

        private void Awake()
        {
            player = GetComponentInParent<GameSession>().Player.transform;

            tankBodyTransform = player.GetComponentInChildren<TankMove>().transform;
            tankTurretTransform = player.GetComponentInChildren<TurretRotate>().transform;
        }

        private void Update()
        {
            if (player == null) return;

            radarTank.eulerAngles = new Vector3(0f, 0f, -tankBodyTransform.eulerAngles.y);
            radarTurret.eulerAngles = new Vector3(0f, 0f, -tankTurretTransform.eulerAngles.y);
        }
    }
}

