using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class DoorScript : MonoBehaviour
    {
        public GameObject openDoor;
        public GameObject closedDoor;

        private void Awake()
        {

        }


        private void Start()
        {
            closedDoor.SetActive(true);
        }


        public void CheckAndOpenDoor()
        {
            Debug.Log("CheckAndOpenDoor");
            if (GameUIManager.Instance.HasAvailableKey())
            {
                openDoor.SetActive(true);
                closedDoor.SetActive(false);
                GameUIManager.Instance.UseKey();
            }
        }
    }
}