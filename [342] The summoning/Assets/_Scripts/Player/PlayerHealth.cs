using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth =100;
        
        private void Start()
        {
            currentHealth = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"PlayerHealth: Player took {damage} damage. Current health: {currentHealth}");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("PlayerHealth: Player has died!");
            Time.timeScale = 0;
        }
    }
}