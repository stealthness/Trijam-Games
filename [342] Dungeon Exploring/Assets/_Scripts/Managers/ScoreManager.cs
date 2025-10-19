using UnityEngine;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public AudioSource _audioSource;
        public AudioClip _coin;
        
        
        [SerializeField] private int coinScore;

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
        }


        public void AddCoin()
        {
            coinScore++;
            Debug.Log("Coins Collected: " + coinScore);        
            _audioSource.Play();
            _audioSource.PlayOneShot(_coin);
        }
    }
}