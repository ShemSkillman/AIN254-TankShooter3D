using UnityEngine;
using TankShooter.Core;
using System.Collections.Generic;
using UnityEngine.UI;

namespace TankShooter.Combat
{
    public class TargetSystem : MonoBehaviour
    {
        [SerializeField] float maxTargetDistance = 100f;
        [SerializeField] Camera gunnerCam;
        [SerializeField] Image targetMarkerPrefab;

        LayerMask enemyMask;
        Plane[] planes;

        RectTransform canvasRect;

        private void Awake()
        {
            canvasRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            enemyMask = LayerMask.GetMask("Enemy");
        }

        // Update is called once per frame
        void Update()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, maxTargetDistance, enemyMask);
            planes = GeometryUtility.CalculateFrustumPlanes(gunnerCam);

            List<Health> enemiesInSight = new List<Health>();

            foreach (Collider enemy in enemies)
            {
                if (GeometryUtility.TestPlanesAABB(planes, enemy.bounds))
                {
                    Vector3 enemyTankPos = enemy.GetComponent<Health>().GetTankPosition();
                    Vector3 viewportPos = gunnerCam.WorldToViewportPoint(enemyTankPos);

                    Vector2 canvasPos = new Vector2(
                    ((viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                    ((viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

                    //shootLocationMarkers[i].rectTransform.anchoredPosition = canvasPos;
                }
            }            
        }
    }
}

