using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Enemies
{
    
    public class DoorScript : MonoBehaviour
    {
        public GameObject openDoor;
        public GameObject closedDoor;

        [SerializeField] private bool isSpecial;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
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
                OpenDoor();
                GameUIManager.Instance.UseKey();
            }
        }


        public void OpenSpecialDoor()
        {
            if (!isSpecial)
            {
                Debug.Log("OpenSpecialDoor is not special");
            }
            
            OpenDoor();
        }

        private void OpenDoor()
        {
            openDoor.SetActive(true);
            closedDoor.SetActive(false);
            _audioSource.Play();
        }
    }
}