using System;
using System.Collections;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Messages
{
    
    /// <summary>
    /// GameManager is responsible for managing the overall game state,
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip successClip;
        public AudioClip failureClip;
        public ParticleSystem successParticles;
        
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

        /// <summary>
        /// Game is initialized in a paused state.
        /// </summary>
        void Start()
        {
            Debug.Log("Game Manage Started");
            Time.timeScale = 0;
        }


        /// <summary>
        /// Resets the current message and letter buttons to their initial states.
        /// </summary>
        public void ResetMessage()
        {
            MessageManager.Instance.ResetMessage();
            LetterButtonsUIManager.Instance.ResetLetters();
        }
        
        /// <summary>
        /// Handler for the Reset Button click event.
        /// Resets the message and score when the button is clicked.
        /// </summary>
        public void OnClickResetButton()
        {
            ResetMessage();
            ScoreManager.Instance.ResetScore();
        }

        /// <summary>
        /// Handler for game failure scenario.Plays failure sound and shows end menu.
        /// </summary>
        public void HandleGameFail()
        {
            Debug.Log("Game Over!");
            EndMenuManager.Instance.ShowEndMenu();
            _audioSource.PlayOneShot(failureClip);
        }
        
        /// <summary>
        /// Checks if the player has completed the message successfully.
        /// </summary>
        public void CheckForWin()
        {
            if (MessageManager.Instance.IsMessageComplete())
            {
                HandleGameWin();
            }
        }

        private void HandleGameWin()
        {
            Debug.Log("You Win!");
            successParticles.Play();
            _audioSource.PlayOneShot(successClip);
            ScoreManager.Instance.AddMessageScore();
            ScoreManager.Instance.UpdateScoreUI();
            // Disable all letter buttons to prevent further input until the next message is presented
            LetterButtonsUIManager.Instance.DisableAllLetterButtons();
            StartCoroutine(nameof(WaitAndReset), 3f);
        }

        /// <summary>
        /// Resets the message after waiting for a specified number of seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public IEnumerator WaitAndReset(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ResetMessage();
        }

        /// <summary>
        /// Starts the game by setting the time scale to normal.
        /// </summary>
        public void StartGame()
        {
            Time.timeScale = 1;
        }
    }
}
