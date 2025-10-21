
using UnityEngine;

namespace _Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class EyeTentacle : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private Animator _animator;
        
        private const float MinSqrVelocity = 0.01f;
        private const float DirectionalThreshold = 0.3f;
        
        [SerializeField] private float maxTentacleSpeed = 6.5f;
        [SerializeField] private float minTentacleSpeed = 3.5f;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _collider2D.isTrigger = true;
            if (_rigidbody2D.linearVelocity.sqrMagnitude < MinSqrVelocity)
            {
                Debug.Log("Bullet has no velocity, default animation played.");
                _animator.Play("Crawl-Left");
            }
            else
            {

                // Copoilot suggestion to determine direction based on velocity (with modification by me)
                
                var vel = _rigidbody2D.linearVelocity.normalized;
                var absX = Mathf.Abs(vel.x);
                var absY = Mathf.Abs(vel.y);
                
                // Determine signs (-1, 0, 1)
                int dx = absX > DirectionalThreshold ? (int)Mathf.Sign(vel.x) : 0;
                int dy = absY > DirectionalThreshold ? (int)Mathf.Sign(vel.y) : 0;
                
                // Choose trigger based on 8-way direction
                if (dx == 0 && dy == 1) _animator.SetTrigger("Up");
                else if (dx == 0 && dy == -1) _animator.SetTrigger("Up"); // down
                else if (dx == 1 && dy == 0) _animator.SetTrigger("Left"); // right
                else if (dx == -1 && dy == 0) _animator.SetTrigger("Left"); // left
                else if (dx == 1 && dy == 1) _animator.SetTrigger("DiagonalUp"); // up-right
                else if (dx == -1 && dy == 1) _animator.SetTrigger("DiagonalDown"); // up-left
                else if (dx == 1 && dy == -1) _animator.SetTrigger("DiagonalDown"); // down-right
                else if (dx == -1 && dy == -1) _animator.SetTrigger("DiagonalUp"); // down-left
                else _animator.SetTrigger("Left"); // fallback
            }
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

        public void SetDirection(Vector2 randomDirection)
        {
            _rigidbody2D.linearVelocity = randomDirection * Random.Range(minTentacleSpeed, maxTentacleSpeed);
        }
    }
}