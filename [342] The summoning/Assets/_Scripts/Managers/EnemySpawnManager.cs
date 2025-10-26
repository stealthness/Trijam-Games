using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public static int EnemyCount = 0;

        public GameObject enemyPrefab;
        public float spawnInterval = 5f;
        public int maxEnemies = 100;
        [SerializeField] private int waveCount = 1;


        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            InvokeRepeating(nameof(Spawn), 0, spawnInterval);
        }

        private void Spawn()
        {
            Debug.Log("EnemySpawnManager: Wave Count " + waveCount);
            for (int i = 0; i < waveCount; i++)
            {
                if (EnemyCount >= maxEnemies)
                {
                    return;
                }

                SpawnEnemy();
            }

            waveCount++;
        }

        private void SpawnEnemy()
        {
            EnemyCount++;
            var spawnXPosition = UnityEngine.Random.Range(25f, 20f);
            var  sign =  UnityEngine.Random.value > 0.5f ? 1: -1;
            Debug.Log("EnemySpawnManager: Spawning enemy at X position " + (sign * spawnXPosition));
            var spawnPosition = new Vector3(sign * spawnXPosition, 0f, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}