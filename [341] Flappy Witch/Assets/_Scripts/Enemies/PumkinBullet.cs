using _Scripts.Witch;
using UnityEngine;

namespace _Scripts.Enemies
{
    /// <summary>
    /// A simple projectile class that applies an initial force to a Rigidbody2D component to simulate a thrown pumpkin.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PumkinBullet : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody2D;
        [SerializeField] public float throwForce = 7f;
        [SerializeField] private float angleVariation = 10f; 

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Start()
        {
            var playerPos = FindAnyObjectByType<PlayerController>().gameObject.transform.position;
            var direction = RandomiseDirection((playerPos - transform.position).normalized);
            var force = Random.Range(throwForce, throwForce + 6f);
            
            _rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
        }
        
        private Vector2 RandomiseDirection(Vector2 direction)
        {
            var newX = direction.x + Random.Range(-0.2f, 0.2f);
            var newY = direction.y + Random.Range(-0.2f, 0.2f);
            return new Vector2(newX, newY).normalized;
            
        }
        
        
    }
}