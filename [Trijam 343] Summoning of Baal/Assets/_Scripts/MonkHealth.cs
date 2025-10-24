using System;
using _Scripts.Enemies;
using a;
using UnityEngine;

namespace _Scripts
{
    public class MonkHealth : MonoBehaviour
    {
        [SerializeField] private int health = 50;
        [SerializeField] private int maxHealth = 50;

        private void Start()
        {
            health = maxHealth;
        }

        private void OnEnable()
        {
            Base2DCollision.OnMonkHit += TakeDamage;
        }

        private void OnDisable()
        {
            Base2DCollision.OnMonkHit -= TakeDamage;
        }


        public void TakeDamage(int damageAmount)
        {
            Debug.Log("Monk took damage");
            health -= damageAmount;

            if (health <= 0)
            {
                MonkHasDied();
            }
        }

        private void MonkHasDied()
        {
            Debug.Log("Monk has died");
            Destroy(gameObject);
        }


        private void OnDrawGizmos()
        {
            BoxCollider2D _collider2D = GetComponent<BoxCollider2D>();
         
            if (!_collider2D)
            {
                return;
            }
                
            
            _collider2D = GetComponent<BoxCollider2D>();
            
            Gizmos.color = Color.red;
            var boxCenter = _collider2D.bounds.center;
            var boxSize = _collider2D.bounds.size;
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
    }
}