using UnityEngine;
using _Scripts.Core;

namespace _Scripts.Enemy
{
    public class Cat : Movement2DPlatformer
    {
       
		[SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private Transform leftPoint;
        [SerializeField] private Transform rightPoint;

        private bool movingRight = true;

        private void Update()
        {
            Patrol();
        }

        private void Patrol()
        {
            if (movingRight)
            {
                moveDirection = Vector2.right;
                _rigidbody2D.linearVelocity = new Vector2(patrolSpeed, _rigidbody2D.linearVelocity.y);

                if (transform.position.x >= rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                moveDirection = Vector2.left;
                _rigidbody2D.linearVelocity = new Vector2(-patrolSpeed, _rigidbody2D.linearVelocity.y);

                if (transform.position.x <= leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
        }
    }
}