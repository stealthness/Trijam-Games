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
            if (!hit)
            {
                return;
            }
            
            
            
            if (hit.CompareTag("Monk"))
            {
                Debug.Log("Enemy: Enemy hit Monk!");
                Destroy(gameObject);
            }

            if (hit.CompareTag("Player"))
            {
                Debug.Log("Enemy: Enemy hit Player!");
            }
            
            if (hit.CompareTag("Weapon"))
            {
                Debug.Log("Enemy: Hit by weapon!");
            }
        }


        private void LateUpdate()
        {
            transform.Translate(_direction * (Time.deltaTime * enemySpeed));
        }
    }
}