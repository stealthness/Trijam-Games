using System;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class Base2DCollision : MonoBehaviour
    {
        
        public static event Action<int> OnPlayerHit;
        public static event Action<int> OnMonkHit;
        
        public LayerMask hitLayers;
        
        [SerializeField] protected int damageAmount = 10;
        [SerializeField] protected bool attackOnCooldown = false;
        [SerializeField] private float attackCooldownTimer = 1f;
        private BoxCollider2D _collider2D;
        private Base2DMovement _movement;

        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _movement = GetComponent<Base2DMovement>();
        }

        private void Update()
        {
            CheckForCollisions();
        }
        
        
        
        private void OnDrawGizmos()
        {
            if (!_collider2D)
            {
                return;
            }
                
            
            _collider2D = GetComponent<BoxCollider2D>();
            
            Gizmos.color = Color.red;
            var boxCenter = _collider2D.bounds.center;
            var boxSize = _collider2D.bounds.size;
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
        
        private void CheckForCollisions()
        {
            RaycastHit2D hits = GetHits();
            if (!hits)
            {
                return;
            }
            
            if (hits.transform.CompareTag("Player"))
            {
                Debug.Log("Player hit");
                AttackPlayer();
            }

            if (hits.transform.CompareTag("Monk"))
            {
                Debug.Log("Monk hit");
                AttackMonk();
            }
        }

        private void AttackMonk()
        {
            OnMonkHit?.Invoke(damageAmount);
            Destroy(gameObject);
        }


        private void AttackPlayer()
        {
            if (attackOnCooldown)
            {
                return;
            }
                
            
            OnPlayerHit?.Invoke(damageAmount);
            attackOnCooldown = true;
            Invoke(nameof(AttackCooldownOver), attackCooldownTimer);
        }
        
        protected void AttackCooldownOver()
        {
            attackOnCooldown = false;
        }

        protected virtual RaycastHit2D GetHits()
        {
            Vector3 boxCenter = _collider2D.bounds.center;
            Vector3 boxSize = _collider2D.bounds.extents;

            return Physics2D.BoxCast(boxCenter, boxSize, 0f, _movement._direction, 0.1f, hitLayers);
        }
    }
}