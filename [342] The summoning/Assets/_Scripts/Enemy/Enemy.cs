using System;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Animator _animator;
        
        private Vector2 _direction;
        [SerializeField] private float enemySpeed = 2f;
        [SerializeField] private SmallEnemyState state;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            state = SmallEnemyState.Moving;
        }

        private void Update()
        {
            _direction = transform.position.x > 0 ? Vector2.left : Vector2.right;
            checkCollision();
        }

        private void checkCollision()
        {
            var box = GetComponent<BoxCollider2D>();
            var hits = Physics2D.OverlapBoxAll(box.bounds.center, box.bounds.size, 0);
            if (hits.Length == 0)
            {
                return;
            }

            foreach (var hit in hits)
            {
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
                    Explode();
                }
            }
        }

        private void Explode()
        {
            state = SmallEnemyState.Dying;
            _animator.SetTrigger("Explode");
        }


        private void LateUpdate()
        {
            switch (state)
            {
                case SmallEnemyState.Dying:
                    break;
                
                case SmallEnemyState.Moving:
                default:
                    transform.Translate(_direction * (Time.deltaTime * enemySpeed));
                    break;
            }
        }
    }


    enum SmallEnemyState
    {
        Idle,
        Moving,
        Charging,
        Attacking,
        Dying
    }
}