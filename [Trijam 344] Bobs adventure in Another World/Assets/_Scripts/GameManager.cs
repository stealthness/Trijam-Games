using UnityEngine;

namespace _Script
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
        }
    
    
    }
}
