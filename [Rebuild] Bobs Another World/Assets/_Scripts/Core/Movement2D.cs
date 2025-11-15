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
        [SerializeField] private Vector2 dir;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocityX = dir.x * (Speed * Time.fixedDeltaTime);
        }
    }
}