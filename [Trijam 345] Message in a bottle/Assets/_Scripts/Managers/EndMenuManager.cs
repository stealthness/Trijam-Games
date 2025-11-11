using _Scripts.Messages;
using UnityEngine;

namespace _Scripts.Managers
{
    
    /// <summary>
    /// This class manages the end menu UI in the game.
    /// It handles showing and hiding the end menu
    /// </summary>
    public class EndMenuManager : MonoBehaviour
    {
        public static EndMenuManager Instance;
        
        public GameObject endMenu;
        
        private void Awake()
        {
            if(!Instance || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }
        
        /// <summary>
        ///  Initializes the end menu by hiding it at the start of the game.
        /// </summary>
        private void Start()
        {
            endMenu.SetActive(false);
        }

        /// <summary>
        /// Shows the end menu UI.
        /// </summary>
        public void ShowEndMenu()
        {
            endMenu.SetActive(true);
        }
        
        /// <summary>
        /// Handler for the Start Game button click event.
        /// Hides the end menu and resets the game state.
        /// </summary>
        public void OnStartGameClicked()
        {
            endMenu.SetActive(false);
            GameManager.Instance.ResetMessage();
            ScoreManager.Instance.ResetScore();
        }
    }
}