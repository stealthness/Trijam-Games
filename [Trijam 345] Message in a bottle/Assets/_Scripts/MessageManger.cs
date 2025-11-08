
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class MessageManager : MonoBehaviour
    {
        public static MessageManager Instance;
        
        
        public RectTransform messagePanel;
        public GameObject letterImagePrefab;
        public Sprite[] letterSprites;
        
        [SerializeField] private float distanceBetweenLetters = 0.0f;
        [SerializeField] private string message = "To Be Or +Not To Be";
        
        private List<LetterDisplay> letterDisplays = new List<LetterDisplay>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        void Start()
        {
            Debug.Log("Message Manager Started");
            if (!messagePanel)
            {
                Debug.LogWarning("Message panel is not assigned in the inspector.");
                return;
            }
            if (!letterImagePrefab)
            {
                Debug.LogWarning("Letter image prefab is not assigned in the inspector.");
                return;
            }
            if (letterSprites == null || letterSprites.Length == 0)
            {
                Debug.LogWarning("No letter sprites assigned in the inspector.");
                return;
            }

            CreateMessageLetters();
        }

        private void CreateMessageLetters()
        {
            // clear existing letters
            foreach (Transform child in messagePanel)
            {
                Destroy(child.gameObject);
            }

            var letterCount = 0;
            var lineCount = 0;
            foreach (var letterChar in message.ToUpper())
            {
                Debug.Log("letterChar: " + letterChar);
                if (letterChar == '+')
                {
                    // Move to next line
                    letterCount = 0;
                    lineCount++;
                    continue;
                }
                
                var letterIndex = 0;
                if (letterChar != ' ')
                {
                    letterIndex = letterChar - 'A' + 1;
                }
                
                var letterSprite = letterSprites[letterIndex];


                if (!letterSprite)
                {
                    Debug.LogWarning("No sprite found for letter: " + letterChar);
                    continue;
                }
                
                CreateLetterImage(letterChar, letterSprite, lineCount, letterCount);
                
                letterCount++;
            }
            
        }

        private void CreateLetterImage(char letterChar, Sprite letterSprite, int lineCount, int letterCount)
        {
            var letterImage = Instantiate(letterImagePrefab, messagePanel);
            letterImage.name = "LetterImage_" + letterChar;
            var letterDisplay = letterImage.GetComponent<LetterDisplay>();
            letterDisplay.Initialise(letterChar, letterSprite);


            RectTransform rt = letterImage.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(letterCount * (rt.sizeDelta.x + distanceBetweenLetters),
                0 - lineCount * rt.sizeDelta.y);

            letterDisplays.Add(letterImage.GetComponent<LetterDisplay>());
        }
        

        /// <summary>
        /// Handle the letter selection and reveal the corresponding letters in the message.
        /// </summary>
        /// <param name="letter"></param> to be revealed, if present in the message
        public void HandleLetterSelection(char letter)
        {
            foreach (var letterDisplay in letterDisplays.Where(letterDisplay => letterDisplay.GetLetter() == letter))
            {
                letterDisplay.ShowLetter(letter);
            }
        }
    }
    
}