using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string startSceneToLoad = "RocketScene";
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
            {
                Destroy(Instance);
            }

            Instance = this;
            LoadScene();
        }

        public void Start()
        {
            Debug.Log("Game Start");
        }
        
        
        /// <summary>
        /// Loads the specified scene additively if it is not already loaded.
        /// </summary>
        private void LoadScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            // Check if the scene is already loaded
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                scene = SceneManager.GetSceneAt(i);
                if (scene.name == startSceneToLoad)
                {
                    Debug.Log($"{startSceneToLoad} already loaded.");
                    return;
                }
            }
            
            Debug.Log($"Search for {startSceneToLoad}");
            if (scene.name != startSceneToLoad)
            {
                SceneManager.LoadSceneAsync(startSceneToLoad, LoadSceneMode.Additive);
            }
            else
            {
                Debug.Log($"Scene {startSceneToLoad} not found among game scenes");
            }
        }
    }
}
