using System;
using _Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    
    public class Land : MonoBehaviour
    {
        public float speed = 3f;
        private void OnEnable()
        {
            PlayerController.OnHorizontalMove += MoveLand;
        }
        
        private void OnDisable()
        {
            PlayerController.OnHorizontalMove -= MoveLand;
        }

        private void MoveLand(float value)
        {
            transform.Translate(Vector3.right * value * Time.deltaTime);
        }
        
    }
}