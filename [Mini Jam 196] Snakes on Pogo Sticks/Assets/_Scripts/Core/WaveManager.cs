using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Core
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;
        [SerializeField] private int currentWave = 0;
        [SerializeField] private int coinsCollected = 0;
        [SerializeField] private int coinsPerWave = 5;

         private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            
            if (coinsCollected >= coinsPerWave)
            {
                GameManager.Instance.NextWave();
            }
        }

        public GameObject[] coins;
        
        private void Start()
        {
            SpawnCoins();
            coinsPerWave = coins.Length;
            coinsCollected = 0;
        }

        private void SpawnCoins()
        {
            foreach (var coin in coins)
            {
                coin.SetActive(true);
            }
        }
        
        public void CollectCoin(GameObject coin)
        {
            coin.SetActive(false);
            // Additional logic for collecting a coin can be added here
            coinsCollected++;

        }

        public void StartNextWave()
        {
            SpawnCoins();
            coinsCollected = 0;
            currentWave++;
        }
    }
}