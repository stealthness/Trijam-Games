using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : MonoBehaviour
    {

        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float playerSpeed = 5f;
        [SerializeField] private Vector2 movementDirection;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        public void Move(Vector2 direction)
        {
            movementDirection = direction;
            
        }


        private void LateUpdate()
        {
            _rigidbody2D.linearVelocity = movementDirection * playerSpeed;
        }
    }
}