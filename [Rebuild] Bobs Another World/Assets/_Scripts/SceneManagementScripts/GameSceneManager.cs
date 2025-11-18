using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.SceneManagementScripts
{
    public class GameSceneManager : MonoBehaviour
    {
        public String firstSceneToLoadName;

        private Scene firstSceneToLoad;

        private void Awake()
        {
            var async = SceneManager.LoadSceneAsync(firstSceneToLoadName, LoadSceneMode.Additive);
            if (async != null) async.completed += OnFirstSceneLoaded;
        }

        private void OnFirstSceneLoaded(AsyncOperation obj)
        {
            firstSceneToLoad = SceneManager.GetSceneByName(firstSceneToLoadName);
            if (firstSceneToLoad.IsValid())
            {
                Debug.Log($"First scene {firstSceneToLoadName} loaded successfully.");
            }
            else
            {
                Debug.LogError($"Failed to load the first scene: {firstSceneToLoadName}");
            }
        }

        private void Start()
        {
            Debug.Log("GameSceneManager started.");
            
            
        }
    }
}