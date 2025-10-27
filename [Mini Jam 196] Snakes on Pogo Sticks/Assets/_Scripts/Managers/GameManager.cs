using _Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public GameObject EmptyBoard;
        public TurnType gameTurn;
        
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
            EmptyBoard.SetActive(true);
            MenuManager.Instance.ShowMainMenu();
            
        }

        public void StartGame()
        {
            Debug.Log("Game Started");
            Time.timeScale = 1;
            gameTurn = TurnType.PlayerTurn;
            EmptyBoard.SetActive(false);
            MenuManager.Instance.ShowTurnMenu(TurnType.PlayerTurn);
        }

        public void NextTurn()
        {
            if (gameTurn == TurnType.PlayerTurn)
            {
                gameTurn = TurnType.EnemyTurn;
                StartEnemyTurn();
            }
            else if (gameTurn == TurnType.EnemyTurn)
            {
                gameTurn = TurnType.PlayerTurn;
            }
            MenuManager.Instance.ShowTurnMenu(gameTurn);
        }

        private void StartEnemyTurn()
        {
            Invoke(nameof(PlayerTurn), enemyTurnDelay);
        }

        public void PlayerTurn()
        {
            gameTurn = TurnType.PlayerTurn;
            MenuManager.Instance.ShowTurnMenu(gameTurn);
        }
        
        
        public void EnemyTurn()
        {
            gameTurn = TurnType.EnemyTurn;
            MenuManager.Instance.ShowTurnMenu(gameTurn);
            StartEnemyTurn();
        }

        public void GameOver()
        {
            gameTurn = TurnType.NoTurn;
            MenuManager.Instance.ShowRestartMenu();
        }

        public void NextWave()
        {
            Debug.Log("Wave Complete");
            WaveManager.Instance.StartNextWave();
        }
    }
}
