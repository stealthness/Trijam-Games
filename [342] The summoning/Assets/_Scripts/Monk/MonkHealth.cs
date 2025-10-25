using System;
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Monk
{
    
    public class MonkHealth : Health
    {

        public SpriteRenderer bloodSpriteRenderer;
        
        public Sprite deadMonkSprite;
        public Sprite hurtMonkSprite;
        public Sprite nearDeathMonkSprite;



        public override void TakeDamage(int damage)
        {
            var extraDamage = damage + 15;
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
        }
        
        
        protected override void Die()
        {
            Debug.Log("MonkHealth: Monk has died!");
            GetComponent<SpriteRenderer>().sprite = deadMonkSprite;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}