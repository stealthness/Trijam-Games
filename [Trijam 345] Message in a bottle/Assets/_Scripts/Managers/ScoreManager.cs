using System;
using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public TextMeshProUGUI scoreText;
        [SerializeField] private int score;
        [SerializeField] private int messageScoreDecreaseAmount = 10;
        [SerializeField] private int messageScoreMaxAmount = 100;
        [SerializeField] private int currentMessageScore = 100;

        private void Awake()
        {
            if(!Instance || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }
        
        public void AddScore(int points)
        {
            score += points;
        }

        public void UpdateScoreUI()
        {
            ResetMessageScore();
            Debug.Log("Updating Score UI: " + score + "Message Score: " + currentMessageScore);
            if (!scoreText)
            {
                Debug.LogWarning("Score Text is not assigned in the inspector.");
                return;
            }
            scoreText.text = "Score\n" + score;
        }


        public void DecreaseMessageScore()
        {
            currentMessageScore -= messageScoreDecreaseAmount;
        }

        public void AddMessageScore()
        {
            AddScore(currentMessageScore);
        }

        public void ResetMessageScore()
        {
            currentMessageScore = messageScoreMaxAmount;
        }
        
        public bool IsMessageScoreDepleted()
        {
            return currentMessageScore <= 0;
        }
        

        public void ResetScore()
        {
            
            score = 0;
            currentMessageScore = messageScoreMaxAmount;
            UpdateScoreUI();
        }
    }
}