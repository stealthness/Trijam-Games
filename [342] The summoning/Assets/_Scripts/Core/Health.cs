using UnityEngine;

namespace _Scripts.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected int maxHealth = 100;
        [SerializeField] public int currentHealth =100;
        
        private void Start()
        {
            currentHealth = maxHealth;
        }
        
        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"Health: took {damage} damage. Current health: {currentHealth}");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        protected virtual void Die()
        {
            Debug.Log("Health: Object has died!");
        }
    }
}