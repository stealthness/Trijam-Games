using _Scripts;
using UnityEngine;
using UnityEngine.UI;

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
    }


}
