using System;
using UnityEngine;

namespace _Scripts.Snakes
{
    public class SnakeManager : MonoBehaviour
    {
        public GameObject[] snakes;
        
        public static SnakeManager Instance;

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


        public void SpawnSnakes(int numberOfSnakes)
        {
            DeactivateAllSnakes();
            
            var maxSnakes = Mathf.Min(numberOfSnakes, snakes.Length);

            for (int i = 0; i < maxSnakes; i++)
            {
                snakes[i].SetActive(true);
            }
        }

        private void DeactivateAllSnakes()
        {
            foreach (GameObject snake in snakes)
            {
                snake.SetActive(false);
            }
        }
    }
}