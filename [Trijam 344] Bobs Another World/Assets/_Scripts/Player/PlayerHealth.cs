using System;
using System.Collections;
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private AudioSource _audioSource;
        private SpriteRenderer _spriteRenderer;
        
        
        public static event Action<int> OnHealthChanged;
        public static event Action OnPlayerDeath;

        public AudioClip playerHurt;
        
        
        [SerializeField] int playerMaxLives = 3;
        [SerializeField] int playerCurrentLives = 3;
        private bool _isInvunerable = false;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            playerCurrentLives = playerMaxLives;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(playerCurrentLives);
        }


        public static void OnPlayerDamaged()
        {
            
        }

        private void OnEnable()
        {
            Dangerous.OnPlayerDamaged += HandlePlayerDamaged;
            
        }

        private void OnDisable()
        {
            Dangerous.OnPlayerDamaged -= HandlePlayerDamaged;
        }


        private void HandlePlayerDamaged(int damage)
        {
            TakeDamage(damage);
        }
        
        private void TakeDamage(int damage)
        {
            if (_isInvunerable) return;
            
            Debug.Log("PH: Player is Damaged by " +damage);
            playerCurrentLives--;
            _audioSource.PlayOneShot(playerHurt);
            OnHealthChanged?.Invoke(playerCurrentLives);
            if (playerCurrentLives <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
            StartCoroutine(DamageImmunityCoroutine());
        }

        private IEnumerator DamageImmunityCoroutine()
        {
            _isInvunerable = true;
            _spriteRenderer.color = Color.red;
            
            yield return new WaitForSeconds(1f);

            _isInvunerable = false;
            _spriteRenderer.color = Color.white;
        }
    }
}