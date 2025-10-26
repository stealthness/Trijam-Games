using UnityEngine;

namespace _Scripts.Player
{
    public class RandomGrunts : MonoBehaviour
    {
        public AudioClip[] grunts;
        
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayRandomGrunt()
        {
            if (grunts.Length == 0) return;
            
            int randomIndex = Random.Range(0, grunts.Length);
            _audioSource.PlayOneShot(grunts[randomIndex]);
        }
    }
}