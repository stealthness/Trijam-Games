using _Scripts.Enemies;
using UnityEngine;

namespace a
{
    public class SmallEnemy : Base2DMovement
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }

            if (collision.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
            
            if (collision.CompareTag("Boundary"))
            {
                Destroy(gameObject);
            }
            
            if (collision.CompareTag("Monk"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}