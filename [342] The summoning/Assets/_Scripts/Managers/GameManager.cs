using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
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
            Time.timeScale = 0;
            StartManager.Instance.ShowPanel(UIState.Start);
        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            Time.timeScale = 1;
        }
    
    }
}
