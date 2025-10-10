using System;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        
        
        [SerializeField] private float flapForce = 5f;
        [SerializeField] private bool canFlap = true;
        [SerializeField] private float delayFlapCooldownTimer = 0.3f;
        
        private bool _isDead = false;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnJump()
        {
            if (!canFlap || _isDead ) return;
            
            
            _rigidbody2D.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
            Debug.Log("Player Jumped");
            canFlap = false;
            Invoke(nameof(DelayFlapCooldown), delayFlapCooldownTimer);
        }
        
        private void DelayFlapCooldown()
        {
            canFlap = true;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                Debug.Log("Player Hit Ground");
                Die();
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                _rigidbody2D.linearVelocityX = -2f;
            }   
            
            if (_isDead) return;
            
            if (other.CompareTag("BrokenTree"))
            {
                Debug.Log("Player Hit BrokenTree");
                Die();
            }
        }
        
        private void Die()
        {
            _isDead = true;
            Debug.Log("Player Died");
            _spriteRenderer.color = Color.red;
            GameManager.Instance.GameOver();
        }   
    }
}