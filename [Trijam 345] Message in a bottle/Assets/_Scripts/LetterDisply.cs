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
    }
}