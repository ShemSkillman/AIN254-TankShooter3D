using UnityEngine;
using TankShooter.Core;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace TankShooter.Combat
{
    public class TargetSystem : MonoBehaviour
    {
        [SerializeField] float maxTargetDistance = 100f;
        [SerializeField] Camera gunnerCam;
        [SerializeField] Image targetMarkerPrefab;

        LayerMask enemyMask;
        Plane[] planes;
        Dictionary<Health, Image> trackedTargets;

        RectTransform canvasRect;
        bool targetSelected = false;

        private void Awake()
        {
            canvasRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            enemyMask = LayerMask.GetMask("Enemy detection");
            trackedTargets = new Dictionary<Health, Image>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckAvailableTargets();

            if (Input.GetKeyDown(KeyCode.T)) SelectTarget();
        }

        private void CheckAvailableTargets()
        {
            if (targetSelected) return;

            Collider[] enemies = Physics.OverlapSphere(transform.position, maxTargetDistance, enemyMask);

            if (enemies.Length < 1) return;

            planes = GeometryUtility.CalculateFrustumPlanes(gunnerCam);

            List<Health> newEnemiesInSight = new List<Health>();

            foreach (Collider enemy in enemies)
            {
                if (GeometryUtility.TestPlanesAABB(planes, enemy.bounds))
                {
                    newEnemiesInSight.Add(enemy.GetComponentInParent<Health>());
                }
            }

            UpdateTargetMarkers(newEnemiesInSight);
            UpdateTargetMarkerPositions();
        }

        private void UpdateTargetMarkers(List<Health> newTargets)
        {
            List<Health> currentTargets = new List<Health>(trackedTargets.Keys);

            // Removes targets that can no longer be seen
            foreach (Health target in currentTargets)
            {
                if (!newTargets.Contains(target))
                {
                    RemoveTargetMarker(target);
                }
            }

            // Adds new targets that can now be seen
            foreach (Health target in newTargets)
            {
                if (!trackedTargets.ContainsKey(target))
                {
                    Image targetMarker = Instantiate(targetMarkerPrefab, canvasRect.transform);
                    trackedTargets[target] = targetMarker;
                }
            }            
        }

        private void RemoveTargetMarker(Health target)
        {
            Image oldTargetMarker = trackedTargets[target];
            trackedTargets.Remove(target);
            Destroy(oldTargetMarker);
        }

        private void UpdateTargetMarkerPositions()
        {
            List<Health> currentTargets = new List<Health>(trackedTargets.Keys);

            foreach (Health enemy in currentTargets)
            {
                if (enemy.GetIsDead())
                {
                    RemoveTargetMarker(enemy);
                    continue;
                }

                Vector3 enemyTankPos = enemy.GetTankPosition();
                Vector3 viewportPos = gunnerCam.WorldToViewportPoint(enemyTankPos);

                Vector2 canvasPos = new Vector2(
                ((viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                ((viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

                trackedTargets[enemy].rectTransform.anchoredPosition = canvasPos;
            }
        }

        private void SelectTarget()
        {
            float closestDistance = Mathf.Infinity;
            Health closestTarget = null;

            foreach(Health target in trackedTargets.Keys)
            {
                Image targetMarker = trackedTargets[target];
                RectTransform rect = targetMarker.GetComponent<RectTransform>();

                float distance = Vector2.Distance(rect.position, canvasRect.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            print(closestTarget.gameObject.name);
        }
    }
}

