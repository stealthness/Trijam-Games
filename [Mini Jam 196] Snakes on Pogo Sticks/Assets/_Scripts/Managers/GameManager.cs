using _Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    
    /// <summary>
    /// The GameManager class is responsible for managing the overall game state,
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// A singleton instance of the GameManager.
        /// </summary>
        public static GameManager Instance;
        /// <summary>
        /// An empty board GameObject to display when the game is not active.
        /// </summary>
        public GameObject emptyBoard;
        /// <summary>
        /// The current turn type in the game.
        /// </summary>
        public TurnType gameTurn;
        
        [Tooltip("Delay in seconds before the enemy takes its turn.")]
        [SerializeField] private float enemyTurnDelay = 2f;

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
            Debug.Log("Game Manager Started");
            Time.timeScale = 0;
            gameTurn = TurnType.NoTurn;
            emptyBoard.SetActive(true);
            MenuManager.Instance.ShowMainMenu();
        }

        /// <summary>
        /// Starts the game by setting the time scale to normal and initializing the player's turn.
        /// </summary>
        public void StartGame()
        {
            Debug.Log("Game Started");
            Time.timeScale = 1;
            gameTurn = TurnType.PlayerTurn;
            emptyBoard.SetActive(false);
            MenuManager.Instance.ShowTurnMenu(TurnType.PlayerTurn);
        }

        /*/// <summary>
        /// Starts the enemy's turn after a specified delay.
        /// </summary>
        private void StartEnemyTurn()
        {
        }*/

        /// <summary>
        /// Starts the player's turn and updates the UI accordingly.
        /// </summary>
        public void PlayerTurn()
        {
            gameTurn = TurnType.PlayerTurn;
            MenuManager.Instance.ShowTurnMenu(gameTurn);
        }
        
        /// <summary>
        /// Starts the enemy's turn and updates the UI accordingly.
        /// </summary>
        public void EnemyTurn()
        {
            gameTurn = TurnType.EnemyTurn;
            MenuManager.Instance.ShowTurnMenu(gameTurn);
            Invoke(nameof(PlayerTurn), enemyTurnDelay);
        }

        public void GameOver()
        {
            gameTurn = TurnType.NoTurn;
            MenuManager.Instance.ShowRestartMenu();
        }

        public void NextWave()
        {
            WaveManager.Instance.StartNextWave();
        }
    }
}
