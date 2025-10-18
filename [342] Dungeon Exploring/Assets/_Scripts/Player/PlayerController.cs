using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    //[RequireComponent(typeof(PlayerAnimator))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        //private PlayerAnimator _playerAnimator;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private Vector2 direction;
        private bool playerIsDiabled = false;
        private const float Tol = 0.01f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //_playerAnimator = GetComponent<PlayerAnimator>();
            _sr = GetComponent<SpriteRenderer>();
        }
        

        /// <summary>
        /// OnMove is called when the player moves
        /// </summary>
        public void OnMove(InputValue value)
        {
            
            if (playerIsDiabled) return;

                direction = value.Get<Vector2>();
                ChangeDirection();
                //_playerAnimator.PlayAnimation(PlayerAnimation.Moving);

        }

        private void LateUpdate()
        {
            if (playerIsDiabled)
            {
                _rb.linearVelocity = Vector2.zero;
            }
            else
            {
                 _rb.linearVelocity = speed * new Vector2(direction.x, direction.y);
            }
            
           
        }

        /// <summary>
        ///  Change the direction of the sprite as per the direction of the player
        /// </summary>
        private void ChangeDirection()
        {
            _sr.flipX = direction.x switch
            {
                < -Tol => true,
                > Tol => false,
                //else => _sr.flipX // If the direction is zero, keep the current flip state
                _ => _sr.flipX
            };
        }

        public void DisableControl()
        {
            Debug.Log("DisableControl");
            playerIsDiabled = true;
        }

        public void OnRestartGame()
        {
            GameManager.Instance.RestartGame();
        }
        
        

    }
}