using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Collectables
{
    public class Key : MonoBehaviour
    {
        public AudioSource keyPickupSound;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Here you can add code to update the player's coin count
                Debug.Log("Coin collected!");
                GameUIManager.Instance.AddKey();
                keyPickupSound.Play();
                gameObject.SetActive(false);
            }
        }
    }
}