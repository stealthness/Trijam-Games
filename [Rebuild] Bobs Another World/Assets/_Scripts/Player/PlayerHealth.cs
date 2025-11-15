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
        
        
        private void Awake()
        {
            _pc = GetComponent<PlayerController>();
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Player Dead");
            }
            Debug.Log("Player Take Damage -> current health" + currentHealth);
        }

        public bool IsDamageable()
        {
            return true;
        }
    }
}
