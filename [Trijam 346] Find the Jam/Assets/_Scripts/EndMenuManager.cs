using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class EndMenuManager : MonoBehaviour
    {
        public static EndMenuManager Instance;
        public GameObject endPanel;
        public TextMeshProUGUI scoreText;
        public GameObject banner;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }
        
        void Start()
        {
            endPanel.SetActive(false);
        }
        
        public void OnOKButtonClicked()
        {
            Debug.Log("Restart Button Clicked");
            GameManager.Instance.NewGame();
            banner.SetActive(false);
            endPanel.SetActive(false);
        }
        
        public void ShowEndMenu()
        {
            if (ScoreManager.Instance.IsLowestScore())
            {
                banner.SetActive(false);
            }
            scoreText.text = "Your score is "+ ScoreManager.Instance.GetScore();
            
            
            Debug.Log("Showing End Menu");
            endPanel.SetActive(true);
            Debug.Log("End Panel Showing End Menu");
            endPanel.SetActive(true);
            if (!endPanel.activeSelf)
            {
                Debug.LogError("End Panel is not active after setting it to active.");
            }
        }
    }
}