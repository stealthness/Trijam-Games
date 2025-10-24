using System;
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