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

        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
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
            var hits = GetHits();
            if (hits == null || hits.Length == 0)
            {
                return;
            }

            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    AttackPlayer();
                }

                if (hit.transform.CompareTag("Monk"))
                {
                    Debug.Log("Monk hit");
                    AttackMonk();
                }
            }
        }

        private void AttackMonk()
        {
            OnMonkHit?.Invoke(damageAmount);
            gameObject.SetActive(false);
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

        protected virtual Collider2D[] GetHits()
        {
            var boxCenter = _collider2D.bounds.center;
            var boxSize = _collider2D.bounds.size;

            return Physics2D.OverlapBoxAll(
                boxCenter,
                boxSize,
                0f,
                hitLayers
            );

            // return Physics2D.BoxCast(
            //     boxCenter, 
            //     boxSize,
            //     0f,
            //     Vector2.zero,
            //     0f,
            //     hitLayers);
        }
    }
}