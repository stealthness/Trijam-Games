using System;
using _Scripts.Enemies;
using a;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        
        
        [SerializeField] private int health = 100;
        [SerializeField] private int maxHealth = 100;


        private void Start()
        {
            health = maxHealth;
        }

        private void OnEnable()
        {
            Base2DCollision.OnPlayerHit += TakeDamage;
        }

        private void OnDisable()
        {
            Base2DCollision.OnPlayerHit -= TakeDamage;
        }


        public void TakeDamage(int damageAmount)
        {
            
            health -= damageAmount;
            Debug.Log("Player took damage");
            
            if (damageAmount <= 0)
            {
                health = 0;
                PlayerHasDied();
            }
        }

        private void PlayerHasDied()
        {
            Time.timeScale = 0;
            Debug.Log("Player has died");
        }
    }
}