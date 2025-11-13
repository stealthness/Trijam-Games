using System;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static  GameManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start()
        {
            Debug.Log("Game Manager Started");
        }
    }
}
