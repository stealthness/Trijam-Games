using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class StartManager : MonoBehaviour
    {
        public static StartManager Instance;
        
        public GameObject GameUI;
        public GameObject StartUI;
        public GameObject EndUI;
        public TextMeshProUGUI reasonText;

        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowPanel(UIState panel)
        {
            HideAllUI();
            switch (panel)
            {
                case UIState.Start:
                    StartUI.SetActive(true);
                    break;
                case UIState.Game:
                    GameUI.SetActive(true);
                    break;
                case UIState.End:
                    EndUI.SetActive(true);
                    break;
            }
        }
        
        public void OnStartButtonClick()
        {
            GameManager.Instance.StartGame();
            ShowPanel(UIState.Game);
        } 
        
        public void OnRestartButtonClick()
        {
            SceneManager.LoadScene(0);
        }
        
        private void HideAllUI()
        {
            GameUI.SetActive(false);
            StartUI.SetActive(false);
            EndUI.SetActive(false);
        }

        public void ShowGameOverPanel(string reason)
        {
            HideAllUI();
            EndUI.SetActive(true);
            reasonText.text = reason;
        }
    }

    public enum UIState
    {
        Start,
        Game,
        End
    }
}