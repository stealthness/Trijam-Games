using _Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    /// <summary>
    /// Manages the game menus including main menu, end menu, and turn indicators.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        /// <summary>
        /// Creates a singleton instance of the MenuManager.
        /// </summary>
        public static MenuManager Instance;

        public GameObject startMenu;
        public GameObject endMenu;
        public GameObject enemyTurnPanel;
        public GameObject playerTurnPanel;
        public GameObject noTurnPanel;

        public TextMeshProUGUI responseText;

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

        public void ShowMainMenu()
        {
            Debug.Log("Main Menu Shown");
            startMenu.SetActive(true);
            ShowTurnMenu(TurnType.NoTurn);
        }

        /// <summary>
        /// Handles the Start button click event to begin the game.
        /// </summary>
        public void OnStartButtonClicked()
        {
            Debug.Log("Start Button Clicked");
            startMenu.SetActive(false);
            GameManager.Instance.StartGame();
        }

        /// <summary>
        /// Displays the restart menu at the end of the game.
        /// </summary>
        public void ShowRestartMenu()
        {
            endMenu.SetActive(true);
        }

        /// <summary>
        /// Handles the Restart button click event to restart the game.
        /// </summary>
        public void OnRestartButtonClicked()
        {
            SceneManager.LoadScene("TurnGameScene");
        }

        
        /// <summary>
        /// Shows the turn menu based on the current game turn.
        /// </summary>
        /// <param name="gameTurn"></param>
        public void ShowTurnMenu(TurnType gameTurn)
        {
            enemyTurnPanel.SetActive(false);
            playerTurnPanel.SetActive(false);
            noTurnPanel.SetActive(false);
            switch (gameTurn)
            {
                case TurnType.PlayerTurn:
                    playerTurnPanel.SetActive(true);
                    break;
                case TurnType.EnemyTurn:
                    enemyTurnPanel.SetActive(true);
                    break;
                default:
                    noTurnPanel.SetActive(false);
                    break;
            }
        }

        public void ShowEndMenu(string eatonByASnake)
        {
            ShowTurnMenu(TurnType.NoTurn);
            endMenu.SetActive(true);
            responseText.text = eatonByASnake;
        }
    }
}