using _Scripts.Monk;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

		public GameObject[] monks;
        private MonkHealth[] _monksHealth;

		

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
		
		public void Update()
		{
			bool allMonksDefeated = true;
            var totalhealth = 0;
            foreach (MonkHealth health in _monksHealth)
            {
                if (health.currentHealth > 0)
                {
                    allMonksDefeated = false;
                    totalhealth += health.currentHealth;
                }
            }

            if (GameUIManager.Instance != null)
            {
                GameUIManager.Instance.UpdateMonksHealth(totalhealth);
            }
            
			if (allMonksDefeated)
            {
                GameOver("All monks have been defeated!");
            }

		}



        private void Start()
        {
            Time.timeScale = 0;
            StartManager.Instance.ShowPanel(UIState.Start);
			monks = GameObject.FindGameObjectsWithTag("Monk");
            _monksHealth = new MonkHealth[monks.Length];
            for (int i = 0; i < monks.Length; i++)
            {
                _monksHealth[i] = monks[i].GetComponent<MonkHealth>();
            }

        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            MusicManager.Instance.StartMusic();
            if (GameUIManager.Instance != null)
            {
                GameUIManager.Instance.UpdatePlayerHealth(100);
            }
            
            Time.timeScale = 1;
        }

        public void GameOver(string reason)
        {
            StartManager.Instance.ShowGameOverPanel(reason);
            Time.timeScale = 0;
        }
    }
}
