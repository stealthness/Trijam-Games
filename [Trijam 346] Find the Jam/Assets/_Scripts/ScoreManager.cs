using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        private const string LowestScoreKey = "LowestScore";
        
        public static ScoreManager Instance;
        
        public TextMeshProUGUI scoreText;
        
        [SerializeField] private int score = 0;
        [SerializeField] private int lowestScore = 0;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
            // Load persisted lowest score
            lowestScore = PlayerPrefs.GetInt(LowestScoreKey, 0);
            UpdateScoreText();
        }
        
        public void AddOnePoint()
        {
            Debug.Log("Point Added");
            score++;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = "" + score;
        }
        
        public void ResetScore()
        {
            Debug.Log("Score Reset");
            if (lowestScore == 0 || score < lowestScore)
            {
                lowestScore = score;
            }
            score = 0;
            UpdateScoreText();
        }

        public bool IsLowestScore()
        {
            if (lowestScore == 0 || score > lowestScore)
            {
                return false;
            }
            
            Debug.Log("New Lowest Score Achieved: " + score);
            lowestScore = score;
            PlayerPrefs.SetInt(LowestScoreKey, lowestScore);
            PlayerPrefs.Save();
            return true;
        }

        public string GetScore()
        {
            return "" + score;
        }
    }
}