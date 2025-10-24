using System;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class Base2DMovement : MonoBehaviour
    {
        public LayerMask hitLayers;
        private Vector2 _direction = Vector2.up;
        [SerializeField] private float moveSpeed = 1f;

        private Collider2D _collider2D;


        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            CheckForCollisions();
            Debug.DrawRay(_collider2D.bounds.center,_direction * (_collider2D.bounds.extents.y + 0.1f), Color.red);
            
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
            }
        }

        protected virtual RaycastHit2D GetHits()
        {
                        Vector3 boxCenter = _collider2D.bounds.center;
                        Vector3 boxSize = _collider2D.bounds.extents;

                        return Physics2D.BoxCast(boxCenter, boxSize, 0f, _direction, 0.1f, hitLayers);
        }
        

        private void LateUpdate()
        {
            transform.Translate(_direction * (moveSpeed * Time.deltaTime), Space.World);
        }
    }
}