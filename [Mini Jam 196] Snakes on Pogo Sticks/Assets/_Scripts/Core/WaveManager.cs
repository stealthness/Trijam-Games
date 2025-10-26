using System;
using System.Collections.Generic;
using _Scripts.Managers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Core
{
    public class WaveManager : MonoBehaviour
    {
        public TextMeshProUGUI waveText;
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
            SpawnCoins(5);
            coinsPerWave = 5;
            coinsCollected = 0;
        }

        private void SpawnCoins(int numberOfCoins = 5)
        {
            DeactivateAllCoins();

            // create index list and shuffle (Fisher-Yates)
            List<int> indices = new List<int>(coins.Length);
            for (int i = 0; i < coins.Length; i++) indices.Add(i);

            for (int i = indices.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                int tmp = indices[i];
                indices[i] = indices[j];
                indices[j] = tmp;
            }
            
            for (var i = 0; i < numberOfCoins; i++)
            {
                var idx = indices[i];
                coins[idx].SetActive(true);
            }
            
        }

        private void DeactivateAllCoins()
        {
            // deactivate all coins first
            foreach (var coin in coins)
            {
                if (coin != null) coin.SetActive(false);
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
            coinsCollected = 0;
            currentWave++;
            SpawnCoins(5 + currentWave * 2);
            
            waveText.text = "Wave: " + currentWave;
        }
    }
}