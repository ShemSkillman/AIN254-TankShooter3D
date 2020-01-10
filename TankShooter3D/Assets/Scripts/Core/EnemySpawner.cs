using System.Collections;
using System.Collections.Generic;
using TankShooter.Combat;
using UnityEngine;


namespace TankShooter.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] int initialTankSpawn = 10;
        [SerializeField] float initialSpawnTime = 60f;
        [SerializeField] float decreaseSpawnTimeMultiplier = 0.9f;
        [SerializeField] int maxEnemyCount = 100;
        [SerializeField] GameObject[] enemyTankPrefabs;
        [SerializeField] Transform enemyTanksParentTransform;

        SpawnPoint[] spawnPoints;
        public int enemyCount = 0;
        public int spawnQueue = 0;
        float currentSpawnTime;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>();
        }

        // Start is called before the first frame update
        IEnumerator Start()
        {
            spawnQueue = initialTankSpawn;
            currentSpawnTime = initialSpawnTime;

            while (true)
            {
                yield return new WaitForSeconds(currentSpawnTime);

                spawnQueue++;
                currentSpawnTime *= decreaseSpawnTimeMultiplier;
            }
        }

        private void Update()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (spawnQueue < 1 || enemyCount > maxEnemyCount) return;

            SpawnPoint spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject randomEnemy = enemyTankPrefabs[Random.Range(0, enemyTankPrefabs.Length)];

            if (!spawn.IsFree) return;

            GameObject instance = Instantiate(randomEnemy, spawn.transform.position, randomEnemy.transform.rotation, enemyTanksParentTransform);
            instance.GetComponent<Health>().SetSpawner(this);

            spawnQueue--;
            enemyCount++;
        }

        public void EnemyDestroyed()
        {
            enemyCount--;
            spawnQueue++;
        }
    }
}

