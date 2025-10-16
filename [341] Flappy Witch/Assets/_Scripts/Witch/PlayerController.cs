using System.Collections;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Scripts.Witch
{
    /// <summary>
    /// This class handles the player's (Witch's) movement, input, and interactions.
    /// </summary>
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
        
        [SerializeField] private float offCameraXpos = -10f;
        [SerializeField] private float maxFlyHeight = 10f;
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
        
        private Coroutine _laughCoroutine;

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
            _laughCoroutine = StartCoroutine(LaughRoutine());
        }
        
        private void Update()
        {
            
            // when the dead player has moved offscreen
            if (transform.position.x < offCameraXpos)
            {
                SceneManager.LoadScene("MenuScene");
            }

            if (_isFrozen)
            {
                return;
            }
            
            
            if (transform.position.y > maxFlyHeight)
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
            canFlap = false;
            Invoke(nameof(DelayFlapCooldown), delayFlapCooldownTimer);
        }
        
        // Event methods
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                Debug.Log("Player Hit Ground");
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                _rigidbody2D.linearVelocityX = -GameManager.Instance.gameSpeed;
                if (!_isDead)
                {
                    Die();
                }
            }

            
            if (_isDead) return;
            
            if (other.CompareTag("BrokenTree"))
            {
                Debug.Log("Player Hit BrokenTree");
                _audioSource.PlayOneShot(deathSound);
                Die();
            }
            
            if (other.CompareTag("FlyingMonkey") && !_isFrozen)
            {
                Debug.Log("Player Hit FlyingMonkey");
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                _rigidbody2D.linearVelocityX = other.GetComponent<Rigidbody2D>().linearVelocityX;
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
            _rigidbody2D.linearVelocityY = 0f;
            _rigidbody2D.gravityScale = 0.1f;
            StartCoroutine(UnFreeze());
        }

        private IEnumerator UnFreeze()
        {
            yield return new WaitForSeconds(laughFrozenTime);
            
            _animator.speed = _savedAnimatorSpeed;
            frozenBlockEffect.SetActive(false);
            _rigidbody2D.gravityScale = 0.5f;
            _isFrozen = false;
        }

        /// <summary>
        /// Witch laughs at random intervals
        /// </summary>
        private IEnumerator LaughRoutine()
        {
            // Initial delay before the first laugh
            yield return new WaitForSeconds(Random.Range(nextLaughMinTime, nextLaughMaxTime));

            while (!_isDead)
            {
                if (!_isFrozen)
                {
                    _audioSource.PlayOneShot(laughSound);
                }
                var min = Mathf.Min(nextLaughMinTime, nextLaughMaxTime);
                var max = Mathf.Max(nextLaughMinTime, nextLaughMaxTime);
                yield return new WaitForSeconds(Random.Range(min, max));
            }
            
        }
    }
}