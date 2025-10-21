using System;
using _Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameUIManager : MonoBehaviour
    {
     
        public static GameUIManager Instance;
		public AudioSource audioSource;
        public AudioClip _key;
        
        public TextMeshProUGUI keyText;
        public TextMeshProUGUI coinText;
        public GameObject weaponImagePanel;
        public GameObject gameOverWonScreen;
        public GameObject gameOverDiedScreen;
        public GameObject firstTimeScreen;
        public TextMeshProUGUI completedText;
        
        [SerializeField] private int keyCount;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void AddKey()
        {
            keyCount++;
            Debug.Log("Keys Collected: " + keyCount);
            keyText.text = "Keys: "+ keyCount;
			audioSource.PlayOneShot(_key);
            
        }
        

        public void UseKey()
        {
            if (keyCount > 0)
            {
                keyCount--;
                Debug.Log("Key Used. Remaining Keys: " + keyCount);
                keyText.text = "Keys: " + keyCount;
            }
            else
            {
                Debug.Log("No keys available to use.");
            }
        }
        
        public bool HasAvailableKey()
        {
            return keyCount > 0;
        }


        public void UpdateCoinText(int coinTextValue)
        {
            coinText.text = "Coins: " + coinTextValue;
        }

        public void ShowWinningScreen()
        {
            var percentCompleted = (int)((CoinCollectorManager.Instance.GetCoinCount()/ 20f) * 100);
            completedText.text = $"You Completed {percentCompleted}%";
            gameOverWonScreen.SetActive(true);
        }

        public void ShowFirstTimeScreen(bool show)
        {
            Debug.Log("ShowFirstTime(" + show + ")");
            firstTimeScreen.SetActive(show);
        }
        
        public void ShowDiedScreen()
        {
            gameOverDiedScreen.SetActive(true);
        }
        
        public void UpdateWeaponImage(WeaponTypes type)
        {
            switch (type)
            {
                case WeaponTypes.Daggers:
                    weaponImagePanel.SetActive(true);
                    break;
                case WeaponTypes.None:
                default:
                    weaponImagePanel.SetActive(false);
                    break;
            }
        }
    }
}