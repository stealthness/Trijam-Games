using System;
using System.Collections.Generic;
using _Scripts.Managers;
using _Scripts.Player;
using _Scripts.Snakes;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Core
{
    /// <summary>
    /// Handles wave management, including spawning coins and tracking collected coins.
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        /// <summary>
        /// Creates a singleton instance of the WaveManager.
        /// </summary>
        public static WaveManager Instance;
        
        public TextMeshProUGUI waveText;
        [SerializeField] private int currentWave = 1;
        [SerializeField] private int coinsCollected = 0;
        [SerializeField] private int coinsPerWave = 5;

        private AudioSource _audioSource;
        
        public AudioClip coinSound;
        public AudioClip waveEndSound;
        
         private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _audioSource = GetComponent<AudioSource>();
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
            coinsPerWave = 5;
            coinsCollected = 0;
            SpawnCoins(coinsPerWave * currentWave);
            SnakeManager.Instance.SpawnSnakes(currentWave);
        }

        private void SpawnCoins(int numberOfCoins = 5)
        {
            DeactivateAllCoins();

            // create index list and shuffle (Fisher-Yates)
            var indices = GameObjectUtils.ShuffleGameObjectsIndices(coins);
            
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
            _audioSource.PlayOneShot(coinSound);
            coin.SetActive(false);
            ScoreManager.Instance.AddScore(100);
            coinsCollected++;

        }

        public void StartNextWave()
        {
            _audioSource.PlayOneShot(waveEndSound);
            FindAnyObjectByType<PlayerController>().ResetPosition();
            coinsCollected = 0;
            currentWave++;
            coinsPerWave = Math.Min(5 + currentWave * 2, coins.Length);
            SpawnCoins(coinsPerWave);
            SnakeManager.Instance.SpawnSnakes(currentWave);
            
            waveText.text = "Level: " + currentWave;
        }
    }
}