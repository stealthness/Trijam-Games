using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    
    [RequireComponent(typeof(PlayerController))]
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        private PlayerController _pc;
        
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;
        [SerializeField] private bool isInvincible = false;
        
        
        private void Awake()
        {
            _pc = GetComponent<PlayerController>();
            currentHealth = maxHealth;
        }

        /// <summary>
        /// The amount of damage to apply to the player.
        /// </summary>
        /// <param name="amount">the amount of damage to taken</param>
        public void TakeDamage(int amount)
        {
            // If the player is invincible, do not apply damage.
            if (isInvincible)
            {
                return;
            }
            
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Player Dead");
            }
            Debug.Log("Player Take Damage -> current health" + currentHealth);
        }

        /// <summary>
        /// Returns if the player is in a damageable state.
        /// ie if the player has been shield then they would be invincible and not damageable.
        /// </summary>
        /// <returns>true if the player can be damage, false otherwise</returns>
        public bool IsDamageable()
        {
            return !isInvincible;
        }
    }
}
