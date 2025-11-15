using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static  GameManager Instance;
        
        [SerializeField] private int boxId1 = -1;
        [SerializeField] private int boxId2 = -1;
        [SerializeField] private string firstBoxName = "";
        public bool isPaused = false;
        public bool isOver = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }

        private void Start()
        {
            Debug.Log("Game Manager Started");
            BoxButtonManager.Instance.CreateBoxes();
        }
        
        
        private void StartGame()
        {
            Debug.Log("Game Started");
            // Add logic to initialize the game
        }
        
        
        public void OnStartButtonClicked()
        {
            Debug.Log("Start Button Clicked");
            SceneManager.LoadScene(0);
        }

        public void BoxOpened(int boxId, string boxName)
        {
            
            if (boxId1 == -1)
            {
                boxId1 = boxId;
                firstBoxName = boxName;
                Debug.Log("First Box Opened: " + firstBoxName);
            }
            else if (boxId2 == -1)
            {
                boxId2 = boxId;
                Debug.Log("Second Box Opened: " + boxName);
                
                // Check for match
                CheckForMatch(firstBoxName, boxName);
            }
            if (boxId1 != -1 && boxId2 != -1)
            {
                // Reset for next turn
                boxId1 = -1;
                boxId2 = -1;
                firstBoxName = "";
            }
        }

        private void CheckForMatch(string boxName1, string boxName2)
        {
            
            var name_part1 = boxName1.Substring(0, boxName1.Length - 2);
            var name_part2 = boxName2.Substring(0, boxName2.Length - 2);
            if (boxName1 != boxName2 && name_part2 == name_part1)
            {
                Debug.Log("Boxes Matched: " + boxName1);
                isPaused = true;

                StartCoroutine(DelayedRemoveBoxes( boxName1, boxName2));
            }
            else
            {
                Debug.Log("Boxes Did Not Match: " + boxName1 + " and " + boxName2);
            }
            
            Invoke(nameof(DelayOver), 1f);
            isPaused = true;
        }

        private IEnumerator DelayedRemoveBoxes(string boxName1, string boxName2)
        {
            yield return new WaitForSeconds(1f);
            BoxButtonManager.Instance.RemoveBoxes(boxName1, boxName2);
            if (BoxButtonManager.Instance.AreAllBoxesRemoved())
            {
                Debug.Log("All Boxes Removed! You Win!");
                isOver = true;
            }
            if (isOver)
            {
                EndMenuManager.Instance.ShowEndMenu();
            }
            else
            {
                isPaused = false;
            }
                    
        }
        
        
        private void DelayOver()
        {
            isPaused = false;
            BoxButtonManager.Instance.HideAllBoxes();
        }

        public void NewGame()
        {
            Debug.Log("Starting New Game");
            isOver = false;
            isPaused = false;
            ScoreManager.Instance.ResetScore();
            BoxButtonManager.Instance.CreateBoxes();
        }
    }
}
