using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private RandomGrunts _grunts;
        
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth =100;

        private void Awake()
        {
            _grunts = GetComponent<RandomGrunts>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"PlayerHealth: Player took {damage} damage. Current health: {currentHealth}");
            _grunts.PlayRandomGrunt();

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