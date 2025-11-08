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
            if (!scoreText)
            {
                Debug.LogWarning("Score Text is not assigned in the inspector.");
                return;
            }
            scoreText.text = "Score: " + score;
        }


    }
}