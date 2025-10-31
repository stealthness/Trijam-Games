using UnityEngine;

namespace _Scripts.EnemySnakes
{
    public class SnakeMovement2D : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float enemySnakeSpeed = 5f;
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
            _rigidbody2D.linearVelocity = movementDirection * enemySnakeSpeed;
        }
    }
}