using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Debug.Log("Game Manager Started");
            Time.timeScale = 0;
            MenuManager.Instance.ShowMainMenu();
        }

        public void StartGame()
        {
            Debug.Log("Game Started");
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1;
        }
    }
}
