using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    /// <summary>
    /// PlayerController handles player input
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        
        public static event Action<float> OnHorizontalMove;
        
        private const float Tolerance = 0000.1f;

        public void OnMove(InputValue value)
        {
            if (value == null) return;

            var inputVector = value.Get<Vector2>();
            if (Mathf.Abs(inputVector.x) < Tolerance)
            {
                return;
            }

            if (inputVector.x < 0)
            {
                // Cannot move backwards only forwards
                Debug.Log("Moving Left: " + inputVector.x);
                OnHorizontalMove?.Invoke(0);
            }
            else
            {
                Debug.Log("Moving Right " + inputVector.x);
                OnHorizontalMove?.Invoke(inputVector.x);
            }
            
            
        }

        public void OnJump(InputValue value)
        {
            if (value == null) return;
            
            if (value.isPressed)
            {
                Debug.Log("Jumping");
            }
        }
    }
}