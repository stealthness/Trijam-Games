using _Scripts.ImprovedTrijam.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.ImprovedTrijam
{
    
    /// <summary>
    /// This class is the improved menu manager for the Improved Trijam version of the game.
    /// It handles the start menu, player death menu, in-game UI, and win panel
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        /// <summary>
        /// The panel that contains the start menu UI elements.
        /// </summary>
        public GameObject startMenuPanel;
        /// <summary>
        /// Is sub panel inside Start Menu that contains the info about Trijam.
        /// </summary>
        public GameObject infoPanel;
        /// <summary>
        /// The panel that contains the Player has died message
        /// </summary>
        public GameObject diedMenuPanel;
        /// <summary>
        /// The in game UI panel.
        /// </summary>
        public GameObject gameUIPanel;
        
        public GameObject winPanel;

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
            startMenuPanel.SetActive(true);
            infoPanel.SetActive(true);
            diedMenuPanel.SetActive(false);
            winPanel.SetActive(false);
        }
        
        /// <summary>
        /// Starts the game when the start button is pressed.
        /// </summary>
        public void OnStartButtonPressed()
        {
            GameManager.Instance.StartGame();
            startMenuPanel.SetActive(false);
            gameUIPanel.SetActive(true);
        }

        /// <summary>
        /// Deactivates the info panel when the OK button is clicked.
        /// </summary>
        public void OnStartOkButtonClicked()
        {
            infoPanel.SetActive(false);
        }
        
        /// <summary>
        /// Restarts the game when the player presses the OK button on the death menu.
        /// </summary>
        public void OnDiedOkButtonPressed()
        {
            SceneManager.LoadScene("ImprovedTrijamGameScene");
        }

        /// <summary>
        /// Shows the player died menu and hides the in-game UI.
        /// </summary>
        public void ShowPlayerDied()
        {
            gameUIPanel.SetActive(false);
            diedMenuPanel.SetActive(true);
        }

        /// <summary>
        /// Shows the player won panel.
        /// </summary>
        public void ShowPlayerWon()
        {
            winPanel.SetActive(true);
        }
    }
}