using _Scripts.Messages;
using UnityEngine;

namespace _Scripts.Managers
{
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
        
        private void Start()
        {
            endMenu.SetActive(false);
        }

        public void ShowEndMenu()
        {
            endMenu.SetActive(true);
        }
        
        public void OnStartGameClicked()
        {
            endMenu.SetActive(false);
            GameManager.Instance.ResetMessage();
            ScoreManager.Instance.ResetScore();
        }
    }
}