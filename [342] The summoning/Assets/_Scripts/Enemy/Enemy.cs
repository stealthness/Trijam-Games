using _Scripts.Managers;
using _Scripts.Monk;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Animator _animator;
        
        private Vector2 _direction;
        [SerializeField] private float enemySpeed = 2f;
        [SerializeField] private SmallEnemyState state;
        [SerializeField] private int damageAmount = 5;

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
            if (state == SmallEnemyState.Dying)
            {
                return;
            }
            
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
                    hit.GetComponent<MonkHealth>().TakeDamage(damageAmount);
                    GetComponent<SpriteRenderer>().color = Color.darkViolet;
                    Explode();
                    Destroy(gameObject);
                }

                if (hit.CompareTag("Player"))
                {
                    Debug.Log("Enemy: Enemy hit Player!");
                    hit.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                    GetComponent<SpriteRenderer>().color = Color.red;
                    Explode();
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
            GetComponent<BoxCollider2D>().enabled = false;
            EnemySpawnManager.EnemyCount--;
            Destroy(gameObject, 1f);
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