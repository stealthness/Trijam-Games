using System;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent (typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float flapForce = 5f;
        [SerializeField] private bool canFlap = true;
        [SerializeField] private float delayFlapCooldownTimer = 0.3f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void OnJump()
        {
            if (!canFlap) return;
            
            
            _rigidbody2D.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
            Debug.Log("Player Jumped");
            canFlap = false;
            Invoke(nameof(DelayFlapCooldown), delayFlapCooldownTimer);
        }
        
        private void DelayFlapCooldown()
        {
            canFlap = true;
        }
    }
}