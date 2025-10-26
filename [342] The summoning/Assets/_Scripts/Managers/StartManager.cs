using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class StartManager : MonoBehaviour
    {
        public static StartManager Instance;
        
        public GameObject GameUI;
        public GameObject StartUI;
        public GameObject EndUI;

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
        
        private void HideAllUI()
        {
            GameUI.SetActive(false);
            StartUI.SetActive(false);
            EndUI.SetActive(false);
        }
        
    }

    public enum UIState
    {
        Start,
        Game,
        End
    }
}