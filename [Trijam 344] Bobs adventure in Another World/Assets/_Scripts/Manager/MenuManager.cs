using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Manager
{
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
            diedMenuPanel.SetActive(false);
            winPanel.SetActive(false);
        }
        
        public void OnStartButtonPressed()
        {
            GameManager.Instance.StartGame();
            startMenuPanel.SetActive(false);
            gameUIPanel.SetActive(true);
        }

        public void OnStartOkButtonClicked()
        {
            
            infoPanel.SetActive(false);
        }
        

        public void OnDiedOkButtonPressed()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowPlayerDied()
        {
            gameUIPanel.SetActive(false);
            diedMenuPanel.SetActive(true);
        }

        public void ShowPlayerWon()
        {
            winPanel.SetActive(true);
        }
    }
}