using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        public GameObject startMenuPanel;
        public GameObject infoPanel;
        public GameObject diedMenuPanel;
        public GameObject gameUIPanel;

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
    }
}