using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Collectables
{
    /// <summary>
    /// This class handles the collection of coins by the player.
    /// </summary>
    public class Coin : MonoBehaviour
    {
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            CoinCollectorManager.Instance.AddCoin();
            gameObject.SetActive(false);
        }
    }
}