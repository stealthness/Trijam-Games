using System;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionsController : MonoBehaviour
    {
        
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
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
            }
        }

        private void Burn()
        {
            _playerController.DisableControl();
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}