using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement2D : Movement2DPlatformer
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        [SerializeField] private float jumpCooldown = 0.5f;
        [SerializeField] private int maxJumps = 2;
        [SerializeField] private int currentJumps = 2;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            jumpForce = 5f;
        }
        
        
        private void Start()
        {
            currentJumps = maxJumps;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
        
        
        public void SetMoveDirection(Vector2 inputVector)
        {
            if (inputVector.sqrMagnitude < 0.1f)
            {
                moveDirection = Vector2.zero;
                _animator.SetBool(IsMoving, false);
                return;
            }
            _animator.SetBool(IsMoving, true);
            moveDirection = new Vector2(inputVector.x, inputVector.y);
            
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
