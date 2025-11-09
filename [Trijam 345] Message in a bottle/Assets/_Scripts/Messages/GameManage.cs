using System;
using System.Collections;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Messages
{
    public class GameManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip successClip;
        public AudioClip failureClip;
        
        public static GameManager Instance;

        private void Awake()
        {
            if(!Instance || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Game Manage Started");

        }


        public void ResetMessage()
        {
            ScoreManager.Instance.ResetScore();
            MessageManager.Instance.ResetMessage();
            LetterUIManager.Instance.ResetLetters();
        }

        public void HandleGameFail()
        {
            Debug.Log("Game Over!");
            _audioSource.PlayOneShot(failureClip);
        }
        
        
        public void CheckForWin()
        {
            if (MessageManager.Instance.IsMessageComplete())
            {
                Debug.Log("You Win!");
                _audioSource.PlayOneShot(successClip);
                ScoreManager.Instance.AddScore(100);
                ScoreManager.Instance.UpdateScoreUI();
                StartCoroutine(nameof(WaitAndReset), 3f);
            }
        }

        public IEnumerator WaitAndReset(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ResetMessage();
        }

    }
}
