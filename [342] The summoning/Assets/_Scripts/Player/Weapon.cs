using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        
        private Animator _swordAnimator;
        private BoxCollider2D _swordCollider;
        public LayerMask layerMask;

        private void Awake()
        {
            _swordAnimator = GetComponent<Animator>();
            _swordCollider = GetComponent<BoxCollider2D>();
        }


        public void OnAttack()
        {
            _swordAnimator.SetTrigger("Attack");
            if (_swordCollider.isActiveAndEnabled)
            {
                CheckCollision();
            }
            
        }

        private void CheckCollision()
        {
            var box = GetComponent<BoxCollider2D>();
            var hit = Physics2D.OverlapBox(box.bounds.center, box.bounds.size, 0, layerMask);
            if (!hit)
            {
                Debug.Log("Weapon hit nothing.");
                return;
            }
            Debug.Log(hit.name);
            
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Weapon: Weapon hit Enemy!");
                Destroy(hit.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            Gizmos.DrawWireCube(transform.position, box.size);
        }
    }
}