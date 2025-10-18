using System;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionsController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var tag = other.gameObject.tag;
            switch (tag)
            {
                case "Fire":
                    Debug.Log("Player hit fire!");
                    Burn();
                    break;
                case "Enemy":
                    Debug.Log("Player hit enemy!");
                    Melt();
                    break;
            }
        }

        private void Melt()
        {
            _playerController.DisableControl();
            _animator.SetTrigger("Melt");
        }

        private void Burn()
        {
            _playerController.DisableControl();
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}