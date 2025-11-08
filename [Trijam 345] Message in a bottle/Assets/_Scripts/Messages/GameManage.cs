using UnityEngine;

namespace _Scripts.Messages
{
    public class GameManager : MonoBehaviour
    {

    
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


    }
}
