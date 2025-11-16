using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Core
{
    public class SceneTrigger : MonoBehaviour
    {

        private Scene _sceneToLoad;
        public string sceneToLoadName;
        public Vector2 offsetOnLoad = Vector2.zero;



        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Entered Scene Trigger: " + gameObject.name);
                LoadScene();
            }
        }

        
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
            foreach (var obj in _sceneToLoad.GetRootGameObjects())
            {
                Debug.Log($"Loaded {_sceneToLoad.name}: {obj.name}");
                if (obj.name == "GameArea")
                {
                    Debug.Log($"GameArea shifted by {offsetOnLoad}");
                    obj.transform.position = offsetOnLoad;
                }
            }
        }

        private void OnDrawGizmos()
        {
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            
            if (box != null)
            {
                Gizmos.color = new Color(0, 1, 0, 0.5f);
                Gizmos.DrawCube(transform.position + (Vector3)box.offset, box.size);
            }
        }
    }
}