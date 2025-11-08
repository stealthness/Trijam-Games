using UnityEngine;
using UnityEngine.UI;


namespace _Scripts
{
    public class LetterDisplay : MonoBehaviour
    {
        private char _letter;
        
        public void SetLetter(char c)
        {
            _letter = c;
        }
        
        public void ShowLetter(char c)
        {
            Debug.Log("c: " + c + " _letter: " + _letter);
            
            if (_letter != c)
                return;



            GetComponentsInChildren<Image>()[0].enabled = true;
            GetComponentsInChildren<Image>()[1].enabled = false;
        }

        public char GetLetter()
        {
            return _letter;
        }

        public void Initialise(char letterChar, Sprite letterSprite)
        {
            _letter = letterChar;
            var images = GetComponentsInChildren<Image>();
            images[0].sprite = letterSprite; // Letter image
            images[0].enabled = false; // Hide letter initially
            images[1].enabled = true; // Shows Blank tile
        }
    }
}