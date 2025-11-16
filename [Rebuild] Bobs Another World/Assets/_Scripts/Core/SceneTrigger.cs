using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Core
{
    public class SceneTrigger : MonoBehaviour
    {
        [Tooltip("The name of the scene to load when the player enters the trigger.")]
        public string sceneToLoadName;
        [Tooltip("The offset to apply to the loaded scene's GameArea object.")]
        public Vector2 offsetOnLoad = Vector2.zero;


        private Scene _sceneToLoad;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("Player Entered Scene Trigger: " + gameObject.name);
            LoadScene();
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
                if (scene.name == sceneToLoadName)
                {
                    Debug.Log($"{sceneToLoadName} already loaded.");
                    return;
                }
            }
            
            Debug.Log($"Search for {sceneToLoadName}");
            if (scene.name != sceneToLoadName)
            {
                SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive)!.completed += OnSceneLoaded;
            }
            else
            {
                Debug.Log($"Scene {sceneToLoadName} not found among game scenes");
            }
        }

        /// <summary>
        /// An async method to handle actions after the scene has been loaded.
        /// </summary>
        /// <param name="op"></param>
        private void OnSceneLoaded(AsyncOperation op)
        { 
            _sceneToLoad = SceneManager.GetSceneByName(sceneToLoadName);
            if (_sceneToLoad.name == SceneManager.GetActiveScene().name)
            {
                Debug.Log($"{_sceneToLoad.name} is already the active scene.");
                return;
            }
            if (!_sceneToLoad.IsValid())
            {
                Debug.Log($"{_sceneToLoad.name} is not a valid scene.");
                return;
            }
            Debug.Log("Successfully loaded scene: " + _sceneToLoad.name);
            ShiftScene(_sceneToLoad);

        }

        /// <summary>
        /// Shifts the GameArea object in the loaded scene by the specified offset.
        /// </summary>
        /// <param name="sceneToLoad"></param>
        private void ShiftScene(Scene sceneToLoad)
        {
            // Look for and shift the GameArea object in the loaded scene by the specified offset
            foreach (var obj in sceneToLoad.GetRootGameObjects())
            {
                Debug.Log($"Loaded {sceneToLoad.name}: {obj.name}");
                if (obj.name == "GameArea")
                {
                    Debug.Log($"GameArea shifted by {offsetOnLoad}");
                    obj.transform.position = offsetOnLoad;
                }
            }
        }

        /// <summary>
        /// Draws a red box to identify the trigger area in the Unity Editor.
        /// </summary>
        private void OnDrawGizmos()
        {
            var box = GetComponent<BoxCollider2D>();
            if (!box) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.size);
        }
    }
}