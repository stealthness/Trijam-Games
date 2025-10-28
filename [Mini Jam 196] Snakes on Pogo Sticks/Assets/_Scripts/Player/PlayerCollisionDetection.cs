using _Scripts.Core;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        public void CheckForCollisions()
        {

            var hits = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            foreach (var hit in hits)
            {
                if (hit.gameObject.CompareTag("Snake"))
                {
                    GetComponent<PlayerController>().Die("Player collided with Snake");
                }

                if (hit.gameObject.CompareTag("Coin"))
                {
                    Debug.Log("Player collided with Coin!");
                    WaveManager.Instance.CollectCoin(hit.gameObject);
                }
                
                if (hit.gameObject.CompareTag("PowerUp"))
                {
                    CollectPowerUp(hit.gameObject);
                }
            }
        }

        private void CollectPowerUp(GameObject powerUp)
        {
            Debug.Log("Player collided with PowerUp!");
            PlayerShield shield = powerUp.GetComponent<PlayerShield>();
            shield.ActivateShield();
            Destroy(powerUp);
        }


    }
}