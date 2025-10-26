using UnityEngine;

namespace _Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        public GameObject menu;
        public GameObject enemyTurnPanel;
        public GameObject playerTurnPanel;
        public GameObject noTurnPanel;

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
            menu.SetActive(true);
            ShowTurnMenu(TurnType.NoTurn);
        }
        
        
        public void OnStartButtonClicked()
        {
            Debug.Log("Start Button Clicked");
            GameManager.Instance.StartGame();
            menu.SetActive(false);
        }
        
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
            menu.SetActive(true);
        }
        
    }
}