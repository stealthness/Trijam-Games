using System;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Vector2 _direction;
        [SerializeField] private float enemySpeed = 2f;


        private void Update()
        {
            if (transform.position.x > 0)
            {
                _direction = Vector2.left;
            }
            else
            {
                _direction = Vector2.right;
            }
            
            checkCollision();
        }

        private void checkCollision()
        {
            var box = GetComponent<BoxCollider2D>();
            var hit = Physics2D.OverlapBox(box.bounds.center, box.bounds.size, 0);
            if (hit != null && hit.CompareTag("Monk"))
            {
                Debug.Log("Enemy hit Monk!");
                Destroy(gameObject);
            }
        }


        private void LateUpdate()
        {
            transform.Translate(_direction * (Time.deltaTime * enemySpeed));
        }
    }
}