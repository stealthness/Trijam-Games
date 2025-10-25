using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour
    {

        private Vector2 _dir;
        [SerializeField] private float playerSpeed = 3f;


        public void OnMove(InputValue value)
        {
            var moveInput = value.Get<Vector2>();
            _dir = new Vector2(moveInput.x, 0);
            CheckDirection();
        }

        private void CheckDirection()
        {
            transform.localScale = _dir.x switch
            {
                > 0 => new Vector3(1, 1, 1),
                < 0 => new Vector3(-1, 1, 1),
                _ => transform.localScale
            };
        }


        private void LateUpdate()
        {
            transform.Translate(_dir * (Time.deltaTime * playerSpeed));
        }
    }
}
