using System;
using _Scripts.Managers;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class EyeCollisionDetection : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        private void OnTriggerEnter2D(Collider2D other)
        {
            

            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
                Debug.Log("Eye hit by player  " + health);
                health -= 5;
                if (health <= 0)
                {
                    Debug.Log("Eye defeated");
                    GameManager.Instance.GameOverWon();
                }
                   
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerCollisionsController>().Melt();
            }
            
        }
    }
}