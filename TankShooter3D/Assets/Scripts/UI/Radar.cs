using UnityEngine.UI;
using UnityEngine;
using TankShooter.Core;
using System.Collections;
using TankShooter.Movement;
using System.Collections.Generic;

public class Radar : MonoBehaviour
{
    [SerializeField] Image projectileMarkerPrefab;
    [SerializeField] Image enemyMarkerPrefab;
    [SerializeField] Image playerMarkerPrefab;
    [SerializeField] int radarPingFrequencyPerSecond = 1;
    [SerializeField] float radarRange = 100;
    [SerializeField] Transform compassDirections;

    Transform playerTransform;
    Vector2 canvasPositionMultiplier;

    Image playerMarker;

    LayerMask enemyMask;
    LayerMask projectileMask;

    RectTransform canvasRect;

    private void Awake()
    {
        enemyMask = LayerMask.GetMask("Enemy detection");
        projectileMask = LayerMask.GetMask("Projectile");

        playerTransform = GetComponentInParent<GameSession>().Player.GetComponentInChildren<TankMove>().transform;
        canvasRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        canvasPositionMultiplier = new Vector2((canvasRect.sizeDelta.x / 2f) / radarRange, (canvasRect.sizeDelta.y / 2f) / radarRange);

        StartCoroutine(RadarUpdate());
    }

    private void Update()
    {
        UpdateCompass();
    }

    private void UpdateCompass()
    {
        compassDirections.eulerAngles = new Vector3(0f, 0f, playerTransform.eulerAngles.y);
    }

    IEnumerator RadarUpdate()
    {
        playerMarker = Instantiate(playerMarkerPrefab, canvasRect.transform);

        var trackedEnemies = new Dictionary<Transform, Image>();
        var trackedProjectiles = new Dictionary<Transform, Image>();

        while (true)
        {
            Collider[] enemyColliders = Physics.OverlapSphere(playerTransform.position, radarRange, enemyMask);
            Collider[] projectileColliders = Physics.OverlapSphere(playerTransform.position, radarRange, projectileMask);

            UpdateTrackEntity(GetEnemyTransforms(enemyColliders), 
                trackedEnemies, enemyMarkerPrefab);

            UpdateTrackEntity(GetProjectileTransforms(projectileColliders), 
                trackedProjectiles, projectileMarkerPrefab);

            UpdateTrackedEntityPositions(trackedEnemies, false);
            UpdateTrackedEntityPositions(trackedProjectiles, true);

            yield return new WaitForSeconds(1 / radarPingFrequencyPerSecond);
        }
    }

    private void UpdateTrackedEntityPositions(Dictionary<Transform, Image> trackedEntities, bool isRotate)
    {
        foreach (Transform entity in trackedEntities.Keys)
        {
            Vector3 entityWorldPos = playerTransform.InverseTransformPoint(entity.position);
            Vector2 entityCanvasPos = new Vector2(entityWorldPos.x, entityWorldPos.z) * canvasPositionMultiplier;            
            trackedEntities[entity].rectTransform.anchoredPosition = entityCanvasPos;
            
            if (isRotate)
            {
                Vector3 entityWorldDir = playerTransform.InverseTransformDirection(entity.forward);
                Vector2 entityPlanarDir = new Vector2(entityWorldDir.x, entityWorldDir.z);

                Quaternion rot = Quaternion.FromToRotation(Vector2.up, entityPlanarDir);
                trackedEntities[entity].rectTransform.eulerAngles = rot.eulerAngles;
            }
        }
    }

    private void UpdateTrackEntity(List<Transform> newEntities, Dictionary<Transform, Image> trackedEntities, Image markerPrefab)
    {
        List<Transform> oldEntities = new List<Transform>(trackedEntities.Keys);

        // Removes tracked entities that are no longer in radar range
        foreach (Transform entity in oldEntities)
        {
            if (!newEntities.Contains(entity))
            {
                Destroy(trackedEntities[entity].gameObject);
                trackedEntities.Remove(entity);
            }
        }

        // Adds new entities that are now within radar range to be tracked
        foreach (Transform entity in newEntities)
        {
            if (!trackedEntities.ContainsKey(entity))
            {
                Image entityMarker = Instantiate(markerPrefab, canvasRect.transform);
                trackedEntities[entity] = entityMarker;
            }
        }        
    }

    private List<Transform> GetProjectileTransforms(Collider[] projectileColldiers)
    {
        List<Transform> projectileTransforms = new List<Transform>();

        foreach (Collider projectile in projectileColldiers)
        {
            projectileTransforms.Add(projectile.transform);
        }

        return projectileTransforms;
    }

    private List<Transform> GetEnemyTransforms(Collider[] enemyColliders)
    {
        List<Transform> enemyTransforms = new List<Transform>();

        foreach (Collider enemy in enemyColliders)
        {
            Transform enemyTransform = enemy.GetComponentInParent<TankMove>().transform;
            enemyTransforms.Add(enemyTransform);
        }

        return enemyTransforms;
    }
}
