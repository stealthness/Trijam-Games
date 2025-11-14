using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static  GameManager Instance;
        
        [SerializeField] private int boxId1 = -1;
        [SerializeField] private int boxId2 = -1;
        [SerializeField] private String boxName1 = "";
        [SerializeField] private String boxName2 = "";
        public bool isPaused = false;

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
                boxName1 = boxName;
                Debug.Log("First Box Opened: " + boxName1);
            }
            else if (boxId2 == -1)
            {
                boxId2 = boxId;
                boxName2 = boxName;
                Debug.Log("Second Box Opened: " + boxName2);
                
                // Check for match
                CheckForMatch();
            }
            if (boxId1 != -1 && boxId2 != -1)
            {
                // Reset for next turn
                boxId1 = -1;
                boxId2 = -1;
                boxName1 = "";
                boxName2 = "";
            }
        }

        private void CheckForMatch()
        {
            
            var name_part1 = boxName1.Substring(0, boxName1.Length - 2);
            var name_part2 = boxName2.Substring(0, boxName2.Length - 2);
            if (boxName1 != boxName2 && name_part2 == name_part1)
            {
                Debug.Log("Boxes Matched: " + boxName1);
                BoxButtonManager.Instance.RemoveBoxes(boxName1, boxName2);
                if (BoxButtonManager.Instance.BoxesAreAllRemoved())
                {
                    Debug.Log("All Boxes Removed! You Win!");
                    // You can add additional logic here for winning the game
                    ScoreManager.Instance.ResetScore();
                    Invoke(nameof(StartGame), 2f);
                }
            }
            else
            {
                Debug.Log("Boxes Did Not Match: " + boxName1 + " and " + boxName2);
            }
            
            Invoke(nameof(DelayOver), 1f);
            isPaused = true;
        }

        private void DelayOver()
        {
            isPaused = false;
            BoxButtonManager.Instance.HideAllBoxes();
        }
    }
}
