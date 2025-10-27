using UnityEngine;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;
        
        private AudioSource _audioSource;
        
        
        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void StartMusic()
        {
            _audioSource.Play();
        }

        public void TogglePauseMusic()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.UnPause();
            }
        }
    }
}