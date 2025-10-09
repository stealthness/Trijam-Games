using System;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent (typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float jumpForce;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void OnJump()
        {
            
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Player Jumped");
        }
    }
}