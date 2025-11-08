using System;
using System.Collections;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Messages
{
    public class GameManager : MonoBehaviour
    {

        
        public static GameManager Instance;

        private void Awake()
        {
            if(!Instance || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Game Manage Started");

        }


        public void ResetMessage()
        {
            MessageManager.Instance.ResetMessage();
            LetterUIManager.Instance.ResetLetters();
        }

        
        public void CheckForWin()
        {
            if (MessageManager.Instance.IsMessageComplete())
            {
                Debug.Log("You Win!");
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
