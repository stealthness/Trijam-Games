using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement2D))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement2D _playerMovement2D;

        private void Awake()
        {
            _playerMovement2D = GetComponent<PlayerMovement2D>();
        }

        public void OnMove(InputValue value)
        {
            Vector2 inputVector = value.Get<Vector2>();
            if (inputVector.sqrMagnitude < 0.1f)
            {
                inputVector = Vector2.zero;
            }
            _playerMovement2D.Move(inputVector.normalized);
        }
    }
}