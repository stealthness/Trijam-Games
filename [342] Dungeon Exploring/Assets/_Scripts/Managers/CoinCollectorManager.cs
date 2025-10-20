using UnityEngine;

namespace _Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class CoinCollectorManager : MonoBehaviour
    {
        public static CoinCollectorManager Instance;
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


        private void Start()
        {
            coinScore = 0;
            GameUIManager.Instance.UpdateCoinText(coinScore);
        }

        public void AddCoin()
        {
            coinScore++;
            Debug.Log("Coins: " + coinScore);        
            GameUIManager.Instance.UpdateCoinText(coinScore);
            _audioSource.PlayOneShot(_coin);
        }

        public int GetCoinCount()
        {
            return coinScore;
        }
    }
}