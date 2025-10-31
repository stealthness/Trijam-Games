using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : Movement2DPlatformer
    {
        [SerializeField] private float jumpCooldown = 0.5f;
        [SerializeField] private int maxJumps = 2;
        [SerializeField] private int currentJumps = 2;
        
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            jumpForce = 5f;
        }
        
        
        private void Start()
        {
            currentJumps = maxJumps;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        
        public void SetMoveDirection(Vector2 inputVector)
        {
            if (inputVector.sqrMagnitude < 0.1f)
            {
                moveDirection = Vector2.zero;
                return;
            }
            
            moveDirection = new Vector2(inputVector.x, inputVector.y);
            
            if (moveDirection.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (moveDirection.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        
        
        public override void Jump()
        {
            if (currentJumps <= 0) return;
            
            currentJumps--;
            _rigidbody2D.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
        }

        

        public void ResetNumberOfJumps()
        {
            currentJumps = maxJumps;
        }


        public void Stop()
        {
            _rigidbody2D.linearVelocityX = 0;
        }
    }
}
