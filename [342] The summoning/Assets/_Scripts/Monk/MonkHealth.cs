using System;
using _Scripts.Core;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Monk
{
    
    public class MonkHealth : Health
    {

        public static int monksHealth;

        public SpriteRenderer bloodSpriteRenderer;
        
        public Sprite deadMonkSprite;
        public Sprite hurtMonkSprite;
        public Sprite nearDeathMonkSprite;

        private void Start()
        {
            monksHealth = 400;
            
        }


        public override void TakeDamage(int damage)
        {
            MonkHealth.monksHealth -= damage;
            
            var extraDamage = damage;
            base.TakeDamage(extraDamage);
            if (currentHealth is <= 70 and > 30)
            {
                Debug.Log("MonkHealth: Monk is hurt!");
                bloodSpriteRenderer.sprite = hurtMonkSprite;
            }
            else if (currentHealth is <= 30 and > 0)
            {
                Debug.Log("MonkHealth: Monk is near death!");
                bloodSpriteRenderer.sprite = nearDeathMonkSprite;
            }

            if (monksHealth <= 0)
            {
                Debug.Log("MonkHealth: All Monks are dead!");
                GameManager.Instance.GameOver("All Monks have been defeated!");
            }
        }
        
        
        protected override void Die()
        {
            Debug.Log("MonkHealth: Monk has died!");
            GetComponent<SpriteRenderer>().sprite = deadMonkSprite;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}