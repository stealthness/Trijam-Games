using UnityEngine;

namespace _Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PumkinBullet : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        
        public float throwForce = 5f;
        public Vector2 throwDirection = new Vector2(-1, 1).normalized;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            _rigidbody2D.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
        
        
    }
}