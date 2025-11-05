using _Scripts.ImprovedTrijam.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ImprovedTrijam.Manager
{
    public class PlayerLivesUI : MonoBehaviour
    {
        public GameObject[] lives;
        public Sprite activeSprite;
        public Sprite inactiveSprite;
        
        private Image[] _heartSprites = new Image[3];

        private void Awake()
        {
            if (lives == null)
            {
                Debug.LogWarning("No lives found");
            }

            foreach (var live in lives)
            {
                if (live == null)
                {
                    Debug.LogWarning("No life found");
                }
            }
        }


        private void Start()
        {
            for (int i = 0 ; i < 3 ; i++)
            {
                _heartSprites[i] = lives[i].GetComponent<Image>();
            }
            
        }

        private void UpdateHealthUI(int health)
        {

            if (_heartSprites == null)
            {
                Debug.Log("_heartSprites is null");
                _heartSprites = new Image[3];
            }

            for (int i = 0 ; i < 3 ; i++)
            {
                if (_heartSprites[i] == null)
                {
                    Debug.Log("_heartSprites is null");
                    _heartSprites[i] = lives[i].GetComponent<Image>();
                }
            }
            
            Debug.Log("PlayerLivesUI UpdateHealthUI : " + health);
            if (health >= 3)
            {
                _heartSprites[0].sprite = activeSprite;
                _heartSprites[1].sprite = activeSprite;
                _heartSprites[2].sprite = activeSprite;
            }
            if (health == 2)
            {
                _heartSprites[0].sprite = activeSprite;
                _heartSprites[1].sprite = activeSprite;
                _heartSprites[2].sprite = inactiveSprite;
            }

            if (health == 1)
            {
                _heartSprites[0].sprite = activeSprite;
                _heartSprites[1].sprite = inactiveSprite;
                _heartSprites[2].sprite = inactiveSprite;
            }

        }


        private void OnEnable()
        {
            PlayerHealth.OnHealthChanged += UpdateHealthUI;
            PlayerHealth.OnPlayerDeath += UpdatePlayerDeath;
            

        }

        private void UpdatePlayerDeath()
        {
            GameManager.Instance.GameOverPlayerDied();
        }

        private void OnDisable()
        {
            PlayerHealth.OnHealthChanged -= UpdateHealthUI;
            PlayerHealth.OnPlayerDeath -= UpdatePlayerDeath;
        }
    }
}