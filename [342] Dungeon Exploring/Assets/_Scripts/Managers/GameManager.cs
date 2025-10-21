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

        private void CheckFirstRun()
        {
            if (!PlayerPrefs.HasKey("FirstRun"))
            {
                PlayerPrefs.SetInt("FirstRun", 1);
                PlayerPrefs.Save();
                Time.timeScale = 0;
                GameUIManager.Instance.ShowFirstTimeScreen(true);
            }
            else
            {
                Time.timeScale = 1;
                GameUIManager.Instance.ShowFirstTimeScreen(false);
            }
        }

        public void FirstTimeStart()
        {
            Time.timeScale = 1;
            GameUIManager.Instance.ShowFirstTimeScreen(false);
        }
        
        private void Start()
        {
            CheckFirstRun();
        }

        public void RestartGame()
        {
            // Logic to restart the game
            Debug.Log("Game Restarted");
            Time.timeScale = 1;
            SceneManager.LoadScene("BWGameScene");
        }
        
        
        public void GameOver()
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            GameUIManager.Instance.ShowDiedScreen();
            
        }

        public void GameOverWon()
        {
            Time.timeScale = 0;
            Debug.Log("You Won!");
            GameUIManager.Instance.ShowWinningScreen();
        }
    }
}
