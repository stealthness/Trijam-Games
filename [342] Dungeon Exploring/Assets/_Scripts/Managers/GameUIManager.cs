using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameUIManager : MonoBehaviour
    {
     
        public static GameUIManager Instance;
        
        public TextMeshProUGUI keyText;
        public TextMeshProUGUI coinText;
        public GameObject gameOverWonScreen;
        public GameObject gameOverDiedScreen;
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
        
        public void ShowDiedScreen()
        {
            gameOverDiedScreen.SetActive(true);
        }
    }
}