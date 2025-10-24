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

        public GameObject bloodPoolSmall;
        public GameObject bloodPoolMedium;
        public Sprite deadMonkSprite;

        private void Start()
        {
            health = maxHealth;
        }
        


        public void TakeDamage(int damageAmount)
        {
            Debug.Log("Monk took damage");
            health -= damageAmount;

            if (health <= 30)
            {
                bloodPoolSmall.SetActive(true);
            }
            if (health <= 15)
            {
                bloodPoolMedium.SetActive(true);
            }

            if (health <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = deadMonkSprite;
                MonkHasDied();
            }
        }

        private void MonkHasDied()
        {
            Debug.Log("Monk has died");
            GetComponent<SpriteRenderer>().sprite = deadMonkSprite;
            GetComponent<Collider2D>().enabled = false;
            
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