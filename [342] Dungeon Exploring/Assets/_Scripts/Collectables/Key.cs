using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Collectables
{
    /// <summary>
    /// This class handles the collection of keys by the player.
    /// </summary>
    public class Key : MonoBehaviour
    {
        public AudioSource keyPickupSound;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            GameUIManager.Instance.AddKey();
            keyPickupSound.Play();
            gameObject.SetActive(false);
        }
    }
}