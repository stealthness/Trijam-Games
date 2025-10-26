using UnityEngine;

namespace _Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        public GameObject menu;
        
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
        }
        
        
        public void OnStartButtonClicked()
        {
            Debug.Log("Start Button Clicked");
            GameManager.Instance.StartGame();
            menu.SetActive(false);
        }
    }
}