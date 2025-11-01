using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject startMenuPanel;
        public GameObject infoPanel;
        public GameObject diedMenuPanel;


        private void Start()
        {
            startMenuPanel.SetActive(true);
            diedMenuPanel.SetActive(false);
        }
        
        public void OnStartButtonPressed()
        {
            GameManager.Instance.StartGame();
            startMenuPanel.SetActive(false);
        }

        public void OnStartOkButtonClicked()
        {
            
            infoPanel.SetActive(false);
        }
        

        public void OnDiedOkButtonPressed()
        {
            SceneManager.LoadScene(0);
        }
    }
}