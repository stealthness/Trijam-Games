using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This class handles 2D movement for a GameObject.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement2D : MonoBehaviour
    {
        protected float Speed  = 3f;
        
        private Rigidbody2D _rb;
        [SerializeField] internal Vector2 dir;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            _rb.linearVelocityX = dir.x * Speed;
            Debug.Log(_rb.linearVelocity);
        }
        
        protected internal virtual void Jump(float jumpForce, Vector2 jumpDir)
        {
            _rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
        }
    }
}