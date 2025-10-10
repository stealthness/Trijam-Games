using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;
        
        public AudioClip jumpSound;
        public AudioClip deathSound;
        public AudioClip laughSound;
        
        [SerializeField] private float flapForce = 5f;
        [SerializeField] private bool canFlap = true;
        [SerializeField] private float delayFlapCooldownTimer = 0.3f;
        
        private bool _isDead = false;
        private bool isFrozen = false;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Invoke(nameof(Laugh), Random.Range(5f, 10f));
        }

        public void OnJump()
        {
            if (!canFlap || _isDead || isFrozen ) return;
            
            _audioSource.PlayOneShot(jumpSound);
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
                _audioSource.PlayOneShot(deathSound);
                Die();
            }
            
            if (other.CompareTag("FlyingMonkey"))
            {
                Debug.Log("Player Hit FlyingMonkey");
                Die();
            }
        }
        
        private void Die()
        {
            CancelInvoke();
            _isDead = true;
            Debug.Log("Player Died");
            _spriteRenderer.color = Color.red;
            GameManager.Instance.GameOver();
        }

        private void Update()
        {
            
            if (_isDead)
            {
            }
            
            // dead player move off screen
            if (transform.position.x < -10f)
            {
                SceneManager.LoadScene("MenuScene");
            }

            if (isFrozen)
            {
                return;
            }
            
            
            if (transform.position.y > 8f)
            {
                Debug.Log("Freeze Player");
                Freeze();
            }
            
            
        }

        private void Freeze()
        {
            _spriteRenderer.color = Color.lightSkyBlue;
            isFrozen = true;
            Invoke(nameof(UnFreeze), 3f);
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.gravityScale = 0.1f;
        }

        private void UnFreeze()
        {
            
            _rigidbody2D.gravityScale = 0.5f;
            _spriteRenderer.color = Color.white;
            isFrozen = false;
        }

        private void Laugh()
        {
            if (_isDead) return;

            if (isFrozen)
            {
                Invoke(nameof(Laugh), Random.Range(2f, 4f));
            }
            else
            {
               _audioSource.PlayOneShot(laughSound);
               Invoke(nameof(Laugh), Random.Range(5f, 10f)); 
            }

        }
    }
}