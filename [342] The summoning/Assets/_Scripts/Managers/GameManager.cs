using _Scripts.Monk;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

		public GameObject[] monks;

		

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
            foreach (GameObject monk in monks)
            {
                if (monk != null)
                {
					if (monk.GetComponent<MonkHealth>().GetHealth() > 0){
                   		allMonksDefeated = false;
                    	break;
					}

                }
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

        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            Time.timeScale = 1;
        }

        public void GameOver(string reason)
        {
            StartManager.Instance.ShowGameOverPanel(reason);
            Time.timeScale = 0;
        }
    }
}
