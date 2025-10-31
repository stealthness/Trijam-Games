using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Manager
{
    public class PumpkinSpawner : MonoBehaviour
    {
        public static PumpkinSpawner Instance;
        
        private List<GameObject> activePumpkins = new List<GameObject>();
        private List<GameObject> poolDeactivePumpkins = new List<GameObject>();
        
        public GameObject pumpkinPrefab;
        [SerializeField] private int spawnRangeX = 20;
        [SerializeField] private int spawnRangeY = 15;
        [SerializeField] private int startSpawnsAmount = 50;

        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            for (int i = 0; i < startSpawnsAmount; i++)
            {
                var randPos = new Vector3(
                    UnityEngine.Random.Range(-spawnRangeX, spawnRangeX),
                    UnityEngine.Random.Range(-spawnRangeY, spawnRangeY),
                    0);
                activePumpkins.Add(Instantiate(pumpkinPrefab, randPos, Quaternion.identity));
            }
        }

        public void DestroyPumpkin(GameObject pumpkin)
        {
            activePumpkins.Remove(pumpkin);
            poolDeactivePumpkins.Add(pumpkin);
            pumpkin.SetActive(false);
        }

        public Vector3 GetNearestPumpkin(Vector3 transformPosition)
        {
            GameObject nearestPumpkin = null;
            float minDistance = Mathf.Infinity;

            foreach (var pumpkin in activePumpkins)
            {
                float distance = Vector3.Distance(transformPosition, pumpkin.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPumpkin = pumpkin;
                }
            }

            return nearestPumpkin ? nearestPumpkin.transform.position : Vector3.zero;
        }
    }
}