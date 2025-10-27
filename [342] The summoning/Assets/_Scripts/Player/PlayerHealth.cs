using _Scripts.Managers;
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
            _grunts.PlayRandomGrunt();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameUIManager.Instance.UpdatePlayerHealth(currentHealth);
                Die();
            }
            GameUIManager.Instance.UpdatePlayerHealth(currentHealth);
        }

        private void Die()
        {
            GameManager.Instance.GameOver("You have died, and summoning has failed!");
            Time.timeScale = 0;
        }
    }
}