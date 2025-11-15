using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
            {
                Destroy(Instance);
            }

            Instance = this;
        }

        public void Start()
        {
            Debug.Log("Game Start");
        }
    }
}
