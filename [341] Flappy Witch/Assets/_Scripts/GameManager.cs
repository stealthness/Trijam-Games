using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public float gameSpeed = 2f;

        public static GameManager Instance;

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
        }


        private void Start()
        {
            Debug.Log("GameManager Start");
        }


        public void GameOver()
        {
            Debug.Log("Game Over!");
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
