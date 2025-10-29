using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    /// <summary>
    /// This class manages the overall game state, including starting, restarting, and ending the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            // Ensure only one instance of GameManager exists (Singleton pattern)
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Checks if this is the first time the game is being run and shows the First Time Run message screen if so.
        /// </summary>
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

        /// <summary>
        /// Restarts the game by reloading the main game scene.
        /// </summary>
        public void RestartGame()
        {
            // Logic to restart the game
            Debug.Log("Game Restarted");
            Time.timeScale = 1;
            SceneManager.LoadScene("BWGameScene");
        }
        
        /// <summary>
        /// Show the Game Over with Player Died screen screen and pause the game.
        /// </summary>
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
