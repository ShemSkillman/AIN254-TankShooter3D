using UnityEngine.UI;
using UnityEngine;
using TankShooter.Core;
using System.Collections;
using TankShooter.Movement;
using System.Collections.Generic;

public class Radar : MonoBehaviour
{
    [SerializeField] Image projectilePrefab;
    [SerializeField] Image enemyPrefab;
    [SerializeField] Image playerPrefab;
    [SerializeField] int radarPingFrequencyPerSecond = 1;
    [SerializeField] float radarRange = 100;

    RadarEntity radarPlayer;
    List<RadarEntity> radarEnemies;
    List<RadarEntity> radarProjectiles;

    LayerMask enemyMask;
    LayerMask projectileMask;

    RectTransform canvasRect;
    Transform playerTransform;

    private void Awake()
    {
        GameObject playerTank = GameObject.FindWithTag("Player");
        playerTransform = playerTank.GetComponentInChildren<TankMove>().transform;

        enemyMask = LayerMask.GetMask("Enemy detection");

        canvasRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(RadarUpdate());

        radarEnemies = new List<RadarEntity>();
        radarProjectiles = new List<RadarEntity>();
    }

    IEnumerator RadarUpdate()
    {
        Image playerMarker = Instantiate(playerPrefab, canvasRect.transform);
        radarPlayer = new RadarEntity(playerMarker, playerTransform);

        print(canvasRect.sizeDelta);

        while (true)
        {
            Collider[] enemyColliders = Physics.OverlapSphere(playerTransform.position, radarRange, enemyMask);

            foreach(Collider enemy in enemyColliders)
            {
                Transform enemyTransfom = enemy.GetComponentInParent<TankMove>().transform;
                Image enemyMarker = Instantiate(enemyPrefab, canvasRect.transform);

                RadarEntity radarEnemy = new RadarEntity(enemyMarker, enemyTransfom);
                radarEnemies.Add(radarEnemy);
            }

            yield return new WaitForSeconds(1 / radarPingFrequencyPerSecond);
        }
    }

    private class RadarEntity
    {
        public Image Marker { get; }
        public Transform WorldTransform { get;}

        public RadarEntity(Image marker, Transform worldTransform)
        {
            Marker = marker;
            WorldTransform = worldTransform;
        }
    }
}
