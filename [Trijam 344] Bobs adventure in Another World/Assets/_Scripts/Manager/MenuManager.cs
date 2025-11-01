using UnityEngine;

namespace _Scripts.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject startMenuPanel;
        
        public void OnStartButtonPressed()
        {
            GameManager.Instance.StartGame();
            startMenuPanel.SetActive(false);
        }
    }
}