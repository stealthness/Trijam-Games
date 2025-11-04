using System;
using _Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    /// <summary>
    /// This class manages the game's user interface (UI), including updating key and coin counts,
    /// displaying game over screens, and updating weapon images.
    /// </summary>
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
        
        /// <summary>
        /// Uses a key if available, by decrementing the key count and updating the UI.
        /// </summary>
        public void UseKey()
        {
            if (HasAvailableKey())
            {
                keyCount--;
                keyText.text = "Keys: " + keyCount;
            }
        }
        
        /// <summary>
        /// Returns true if there is at least one available key, false otherwise.
        /// </summary>
        /// <returns>Returns true if there is at least one available key, false otherwise.</returns>
        public bool HasAvailableKey()
        {
            return keyCount > 0;
        }

        /// <summary>
        /// Updates the coin text display with the current coin count.
        /// </summary>
        /// <param name="coinTextValue"></param>
        /// <returns></returns>
        public void UpdateCoinText(int coinTextValue)
        {
            coinText.text = "Coins: " + coinTextValue;
        }

        
        /// <summary>
        /// Activates the winning screen and displays the percentage of coins collected.
        /// </summary>
        /// <returns></returns>
        public void ShowWinningScreen()
        {
            var percentCompleted = (int)((CoinCollectorManager.Instance.GetCoinCount()/ 20f) * 100);
            completedText.text = $"You Completed {percentCompleted}%";
            gameOverWonScreen.SetActive(true);
        }

        /// <summary>
        /// Shows or hides the first time screen based on the provided boolean value.
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        public void ShowFirstTimeScreen(bool show)
        {
            firstTimeScreen.SetActive(show);
        }
        
        /// <summary>
        /// Activates, and shows the died screen.
        /// </summary>
        /// <returns></returns>
        public void ShowDiedScreen()
        {
            gameOverDiedScreen.SetActive(true);
        }
        
        /// <summary>
        /// Activates and updates the weapon image based on the provided weapon type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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