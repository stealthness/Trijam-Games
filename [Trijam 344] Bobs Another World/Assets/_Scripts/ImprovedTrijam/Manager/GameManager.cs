using UnityEngine;

namespace _Scripts.ImprovedTrijam.Manager
{
    public class GameManager : MonoBehaviour
    {


        
        public static GameManager Instance;

        public GameObject startLevel;

        private void Awake()
        {
            if (Instance == null)
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
            Debug.Log("GameManager is Started");
            Time.timeScale = 0;
            startLevel.SetActive(false);
            
        }
        
        public void StartGame()
        {
            Time.timeScale = 1;
            startLevel.SetActive(true);
        }


        public void GameOverPlayerDied()
        {
            Time.timeScale = 0;
            _Scripts.Manager.MenuManager.Instance.ShowPlayerDied();
        }

        public void GameWon()
        {
            Time.timeScale = 0;
            _Scripts.Manager.MenuManager.Instance.ShowPlayerWon();
            
        }
    }
}
