using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {

        private Vector2 _moveInput;
        [SerializeField] private float playerSpeed = 3f;
        
        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }


        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
            CheckDirectionForSpriteFlip();
        }

        private void CheckDirectionForSpriteFlip()
        {
            if (_moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }


        public void LateUpdate()
        {
            transform.Translate(_moveInput * (playerSpeed * Time.deltaTime), Space.World);
        }


        public void OnDrawGizmos()
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider2D>();
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + (Vector3)_collider.offset, _collider.size);
        }
    }
}
