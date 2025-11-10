using _Scripts.Messages;
using UnityEngine;

namespace _Scripts.Managers
{
    public class StartMenuManager : MonoBehaviour
    {
        public GameObject startMenuPanel;

        private void Start()
        {
            startMenuPanel.SetActive(true);
        }


        public void OnStartGameButtonClicked()
        {
            startMenuPanel.SetActive(false);
            GameManager.Instance.StartGame();
        }
    }
}