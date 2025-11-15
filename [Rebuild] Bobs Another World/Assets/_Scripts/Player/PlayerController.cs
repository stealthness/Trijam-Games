
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement2D))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement2D _pm;

        private void Awake()
        {
            _pm = GetComponent<PlayerMovement2D>();
        }


        public void OnMove(InputValue value)
        {
            Vector2 inputVector = value.Get<Vector2>();
            _pm.dir = inputVector;
        }


        public void OnJump(InputValue value)
        {
            _pm.Jump(5f, Vector2.up);
        }
    }
}