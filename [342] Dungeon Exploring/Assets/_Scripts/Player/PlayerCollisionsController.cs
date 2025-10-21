using System;
using _Scripts.Enemies;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(AudioSource))]
    public class PlayerCollisionsController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController _playerController;
        private AudioSource _audioSource;
        
        public AudioClip deathSound;
        
        private bool _isDead;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _isDead = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDead) return;
            
            switch (other.gameObject.tag)
            {
                case "Fire":
                    Debug.Log("Player hit fire!");
                    Burn();
                    break;
                case "Enemy":
                case "Bullet":
                    Debug.Log("Player hit enemy!");
                    Melt();
                    break;
            }
        }

        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_isDead) return;
            
            Debug.Log(">" + other.gameObject.tag);
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                Melt();
            }
            
            if (other.gameObject.CompareTag("Door"))
            {
                var door = other.gameObject.GetComponentInParent<DoorScript>();
                door.CheckAndOpenDoor();
            }
        }

        public void Melt()
        {
            if (_isDead) return;
            
            _isDead = true;
            GetComponent<Collider2D>().enabled = false;
            _playerController.DisableControl();
            _animator.SetTrigger("Melt");
            _audioSource.PlayOneShot(deathSound);
            float length = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            Invoke(nameof(GameOver), length);
            
        }

        private void GameOver()
        {
            GameManager.Instance.GameOver();
        }

        private void Burn()
        {
            _playerController.DisableControl();
            GetComponent<SpriteRenderer>().color = Color.red;
        }

    }
}