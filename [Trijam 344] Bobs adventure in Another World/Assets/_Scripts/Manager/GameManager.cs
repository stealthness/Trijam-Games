using UnityEngine;

namespace _Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

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
        }
        
        public void StartGame()
        {
            Time.timeScale = 1;
        }
    
    
    }
}
