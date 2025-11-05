using UnityEngine;

namespace _Scripts.ImprovedTrijam.Manager
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;

        public GameObject startLevel;

        private void Awake()
        {
            Debug.Log("Improved:GameManager Awake");
            if (Instance == null)
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
            Debug.Log("GameManager is Started");
            Time.timeScale = 0;
            startLevel.SetActive(false);
            
        }
        
        /// <summary>
        /// Starts the game by setting the time scale to 1 and activating the start level.
        /// </summary>
        public void StartGame()
        {
            Time.timeScale = 1;
            startLevel.SetActive(true);
        }
        
        /// <summary>
        /// Game over sequence when the player dies. Shows the player died menu and pauses the game.
        /// </summary>
        public void GameOverPlayerDied()
        {
            Time.timeScale = 0;
            MenuManager.Instance.ShowPlayerDied();
        }

        /// <summary>
        /// Game won sequence when the player collects all ship parts. Shows the player won menu and pauses the game.
        /// </summary>
        public void GameWon()
        {
            Time.timeScale = 0;
            MenuManager.Instance.ShowPlayerWon();
        }
    }
}
