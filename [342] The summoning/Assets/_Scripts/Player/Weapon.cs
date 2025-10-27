using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Animator _swordAnimator;
        private BoxCollider2D _swordCollider;
        public LayerMask layerMask;

        private void Awake()
        {
            _swordAnimator = GetComponent<Animator>();
            _swordCollider = GetComponent<BoxCollider2D>();
            _audioSource = GetComponent<AudioSource>();
        }


        public void OnAttack()
        {
            _swordAnimator.SetTrigger("Attack");
            _audioSource.Play();
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
            // > generated code
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            if (box == null) return;

            Vector2 size = box.size;
            Vector2 offset = box.offset;
            Transform t = transform;
            Vector2 halfSize = size * 0.5f;

            Vector2[] localPoints = new Vector2[]
            {
                offset + new Vector2(-halfSize.x, -halfSize.y),
                offset + new Vector2(-halfSize.x,  halfSize.y),
                offset + new Vector2( halfSize.x,  halfSize.y),
                offset + new Vector2( halfSize.x, -halfSize.y)
            };

            Gizmos.color = Color.red;
            for (int i = 0; i < 4; i++)
            {
                Vector2 p1 = t.TransformPoint(localPoints[i]);
                Vector2 p2 = t.TransformPoint(localPoints[(i + 1) % 4]);
                Gizmos.DrawLine(p1, p2);
            }
            // < generated code
        }
    }
}