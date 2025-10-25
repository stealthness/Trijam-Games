using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public float spawnInterval = 2f;


        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            InvokeRepeating(nameof(Spawn), spawnInterval, spawnInterval);
        }
        
        private void Spawn()
        {
            var spawnXPosition = UnityEngine.Random.value > 0.5f ? 20f : -20f;
            var spawnPosition = new Vector3(spawnXPosition, 0f, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}