using UnityEngine;

namespace _Scripts.Managers
{
    
    /// <summary>
    /// MusicManager is responsible for managing background music playback in the game.
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;
        public AudioSource musicSource;
        
        private void Awake()
        {
            if(!Instance || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
            if (!musicSource)
            {
                Debug.LogWarning("Music Source is not assigned in the inspector.");
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Toggles the background music on or off based on the provided boolean value.
        /// </summary>
        /// <param name="toggle"></param>
        public void ToggleMusic(bool toggle)
        {
            Debug.Log("MusicManager ToggleMusic: " + toggle);
            if (toggle)
            {
                musicSource.Play();
            }
            else
            {
                musicSource.Pause();
            }
        }

    }
}