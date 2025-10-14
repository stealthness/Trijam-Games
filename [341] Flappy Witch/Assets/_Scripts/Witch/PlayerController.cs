using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        private static readonly int DieAnimationClip = Animator.StringToHash("Die");
        
        public AudioClip jumpSound;
        public AudioClip deathSound;
        public AudioClip laughSound;
        public GameObject frozenBlockEffect;
        
        [SerializeField] private float flapForce = 5f;
        [SerializeField] private bool canFlap = true;
        [SerializeField] private float delayFlapCooldownTimer = 0.3f;
        [SerializeField] private float nextLaughMinTime = 5f;
        [SerializeField] private float nextLaughMaxTime = 10f;
        [SerializeField] private float laughFrozenTime = 3f;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private AudioSource _audioSource;
        private Animator _animator;
        
        private bool _isDead = false;
        private bool _isFrozen = false;
        private float _savedAnimatorSpeed;

        // Inherit from MonoBehaviour
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _savedAnimatorSpeed = _animator.speed;
        }

        private void Start()
        {
            Invoke(nameof(Laugh), Random.Range(5f, 10f));
        }
        
        private void Update()
        {
            
            // when the dead player has moved offscreen
            if (transform.position.x < -10f)
            {
                SceneManager.LoadScene("MenuScene");
            }

            if (_isFrozen)
            {
                return;
            }
            
            
            if (transform.position.y > 8f)
            {
                Debug.Log("Freeze Player");
                Freeze();
            }
        }
        
        // Input Systems methods 
        public void OnJump()
        {
            if (!canFlap || _isDead || _isFrozen ) return;
            
            _audioSource.PlayOneShot(jumpSound);
            _rigidbody2D.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
            Debug.Log("Player Jumped");
            canFlap = false;
            Invoke(nameof(DelayFlapCooldown), delayFlapCooldownTimer);
        }
        
        // Event methods
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                Debug.Log("Player Hit Ground");
                Die();
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                _rigidbody2D.linearVelocityX = GameManager.Instance.gameSpeed;
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
        
        // New methods
        
        private void DelayFlapCooldown()
        {
            canFlap = true;
        }
        

        
        private void Die()
        {
            Debug.Log("Player Died");
            
            CancelInvoke();
            _isDead = true;
            _animator.SetTrigger(DieAnimationClip);
            GameManager.Instance.GameOver();
        }


        /// <summary>
        /// If the player flies too high, they get frozen for 3 seconds
        /// </summary>
        private void Freeze()
        {
            _savedAnimatorSpeed = _animator.speed;
            _animator.speed = 0f; 
            
            _spriteRenderer.color = Color.lightSkyBlue;
            _isFrozen = true;
            frozenBlockEffect.SetActive(true);
            Invoke(nameof(UnFreeze), laughFrozenTime);
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.gravityScale = 0.1f;
        }

        private void UnFreeze()
        {
            _animator.speed = _savedAnimatorSpeed;
            frozenBlockEffect.SetActive(false);
            _rigidbody2D.gravityScale = 0.5f;
            _isFrozen = false;
        }

        /// <summary>
        /// Witch laughs at random intervals
        /// </summary>
        private void Laugh()
        {
            if (_isDead) return;

            if (!_isFrozen)
            {
               _audioSource.PlayOneShot(laughSound);
            }
            Invoke(nameof(Laugh), Random.Range(nextLaughMaxTime, nextLaughMaxTime)); 
        }
    }
}