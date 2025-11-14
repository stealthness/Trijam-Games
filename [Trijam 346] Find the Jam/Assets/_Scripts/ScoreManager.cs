using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class ScoreManager : MonoBehaviour
    {
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
    }
}