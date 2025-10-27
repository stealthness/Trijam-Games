using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        public TextMeshProUGUI scoreText;
        
        private int _score;
        
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
            _score = 0;
        }
        
        public void AddScore(int points)
        {
            _score += points;
            scoreText.text = "Score: " + _score;
            Debug.Log($"Score: {_score}");
        }
    }
}