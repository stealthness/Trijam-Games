using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public TurnType gameTurn;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
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
            MenuManager.Instance.ShowMainMenu();
            
        }

        public void StartGame()
        {
            Debug.Log("Game Started");
            SceneManager.LoadScene("TurnGameScene");
            Time.timeScale = 1;
            gameTurn = TurnType.PlayerTurn;
            MenuManager.Instance.ShowTurnMenu(TurnType.PlayerTurn);
        }

        public void NextTurn()
        {
            if (gameTurn == TurnType.PlayerTurn)
            {
                gameTurn = TurnType.EnemyTurn;
                MenuManager.Instance.ShowTurnMenu(gameTurn);
                StartEnemyTurn();
            }
            else if (gameTurn == TurnType.EnemyTurn)
            {
                gameTurn = TurnType.PlayerTurn;
                MenuManager.Instance.ShowTurnMenu(gameTurn);
            }
        }

        private void StartEnemyTurn()
        {
            Invoke(nameof(PlayerTurn), 2f);
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
    }
}
