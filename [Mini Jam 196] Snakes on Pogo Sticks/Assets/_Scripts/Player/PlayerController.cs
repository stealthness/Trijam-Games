using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private float speed = 5f;
        
        

        public void OnMove(InputValue value)
        {
            Vector2 inputDirection = value.Get<Vector2>();
            if (inputDirection.x == 0)
            {
                direction = Vector2.zero;
            }
            else if (inputDirection.x < 0)
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }
            
            
        }

        private void LateUpdate()
        {
            transform.Translate(direction * (speed * Time.deltaTime), Space.World);
        }
    }
}