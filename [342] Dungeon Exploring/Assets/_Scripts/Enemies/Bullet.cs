
using UnityEngine;

namespace _Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Collider2D coll;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
        }

        private void Start()
        {
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            coll.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Here you can add code to deal damage to the player
                Debug.Log("Player hit by bullet!");
                // For example: other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                Destroy(gameObject);
            }
            
            if (other.CompareTag("Wall"))
            {
                // Destroy the bullet when it hits an obstacle
                Destroy(gameObject);
            }
        }
    }
}