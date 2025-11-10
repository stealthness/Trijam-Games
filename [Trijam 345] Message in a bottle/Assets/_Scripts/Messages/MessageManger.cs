using System.Collections.Generic;
using System.Linq;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace _Scripts.Messages
{
    public class MessageManager : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip successAudioClip;
        public AudioClip failureAudioClip;
        
        public static MessageManager Instance;
        [Tooltip("Reference to the Message Database ScriptableObject.")]
        public MessageDatabase messageDatabase;
        [Tooltip("Panel where the message letters will be displayed.")]
        public RectTransform messagePanel;
        [Tooltip("Prefab for the letter image UI element.")]
        public GameObject letterImagePrefab;
        [Tooltip("Array of letter sprites from A-Z and a blank sprite at index 0.")]
        public Sprite[] letterSprites;
        
        [SerializeField] private float distanceBetweenLetters = 0.0f;
        [SerializeField] private float distanceBetweenLines = 0.0f;
        [Tooltip("Use '+' to indicate line breaks in the message.")]
        [SerializeField] private string message = "To Be Or+Not To Be";
        
        private readonly List<LetterDisplay> _letterDisplays = new List<LetterDisplay>();

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
            _audioSource = GetComponent<AudioSource>();
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

            message = messageDatabase.GetRandomMessage();
            CreateMessageLetters();
        }

        /// <summary>
        /// Creates the message letters based on the current message.
        /// </summary>
        private void CreateMessageLetters()
        {
            Debug.Log("message length: " + message.Replace('+', ' ').Length);
            if (_letterDisplays.Count > 0)
            {
                ClearOldLetters();
            }
            Debug.Log("_letterDisplays count after clear: " + _letterDisplays.Count);
            

            var letterCount = 0;
            var lineCount = 0;
            foreach (var letterChar in message.ToUpper())
            {
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

        private void ClearOldLetters()
        {
            _letterDisplays.Clear();
            _letterDisplays.Capacity = 0;
            
            if (messagePanel == null) return;

            var letterObjects = messagePanel
                .GetComponentsInChildren<Transform>(true)
                .Where(t => t.gameObject.name.StartsWith("LetterImage_"))
                .Select(t => t.gameObject)
                .ToList();

            foreach (var go in letterObjects)
            {
                Destroy(go);
            }
            
            // clear existing letters
            foreach (var buttoneLetter in messagePanel.GetComponentsInChildren<Button>())
            {
                Destroy(buttoneLetter.gameObject);
            }
        }

        private void CreateLetterImage(char letterChar, Sprite letterSprite, int lineCount, int letterCount)
        {
             
            var letterImage = Instantiate(letterImagePrefab, messagePanel);
            letterImage.name = "LetterImage_" + letterChar;
            var letterDisplay = letterImage.GetComponent<LetterDisplay>();
            letterDisplay.Initialise(letterChar, letterSprite);


            var rt = letterImage.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(letterCount * (rt.sizeDelta.x + distanceBetweenLetters),
                0 - lineCount * (rt.sizeDelta.y + distanceBetweenLines));
            if (letterChar == ' ')
            {
                letterImage.SetActive(false);
            }
            

            _letterDisplays.Add(letterImage.GetComponent<LetterDisplay>());
        }
        

        /// <summary>
        /// Handle the letter selection and reveal the corresponding letters in the message.
        /// </summary>
        /// <param name="letter"></param> to be revealed, if present in the message
        public bool HandleLetterSelection(char letter)
        {
            bool foundLetterInMessage = false;
            foreach (var letterDisplay in _letterDisplays)
            {
                if (!letterDisplay)    
                {
                    Debug.LogWarning("LetterDisplay component missing on a letter image.");
                    continue;
                }

                if (letterDisplay.GetLetter() != letter)
                {
                    continue;
                }
                
                foundLetterInMessage = true;
                letterDisplay.ShowLetter(letter);
            }
            
            if (!foundLetterInMessage)
            {
                Debug.Log("Letter " + letter + " not found in the message.");
                ScoreManager.Instance.DecreaseMessageScore();
                if (ScoreManager.Instance.IsMessageScoreDepleted())
                {
                    MusicManager.Instance.ToggleMusic(false);
                    Debug.Log("Message score depleted.");
                    GameManager.Instance.HandleGameFail();
                    return false;
                }
                _audioSource.PlayOneShot(failureAudioClip);
            }
            else
            {
                Debug.Log("Letter " + letter + " found in the message.");
                _audioSource.PlayOneShot(successAudioClip);
            }
            
            // foreach (var letterDisplay in _letterDisplays.Where(letterDisplay => letterDisplay.GetLetter() == letter))
            // {
            //     letterDisplay.ShowLetter(letter);
            // }
            return foundLetterInMessage;
        }

        public void ResetMessage()
        {
            ClearOldLetters();
            message = messageDatabase.GetRandomMessage();
            CreateMessageLetters();
        }

        public bool IsMessageComplete()
        {
            foreach (var letterDisplay in _letterDisplays)
            {
                if (letterDisplay.GetLetter() == ' ')
                {
                    continue;
                }
                
                if (letterDisplay.isRevealed() == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
    
}