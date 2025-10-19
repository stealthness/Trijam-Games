using UnityEngine;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        private AudioSource _audioSource;
        
        
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
            _audioSource = GetComponent<AudioSource>();
        }


        public void AddCoin()
        {
            coinScore++;
            Debug.Log("Coins Collected: " + coinScore);        
            _audioSource.Play();
        }
    }
}