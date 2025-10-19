using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
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
        
        public void RestartGame()
        {
            // Logic to restart the game
            Debug.Log("Game Restarted");
            SceneManager.LoadScene("BWGameScene");
        }
        
        
        public void GameOver()
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            
        }
    }
}
