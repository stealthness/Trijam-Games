using UnityEngine;

namespace _Scripts.Managers
{
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
            }
        }


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