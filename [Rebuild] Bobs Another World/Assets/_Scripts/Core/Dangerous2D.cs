using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Dangerous2D : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Collider2D _col;
        
        List<IDamageable> _damageables;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.bodyType = RigidbodyType2D.Static;
            _col = GetComponent<Collider2D>();
            _col.isTrigger = true;
            _damageables = new List<IDamageable>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the other object has a component that implements IDamageable
            if (other.TryGetComponent(out IDamageable damageable) && damageable.IsDamageable())
            {
                damageable.TakeDamage(10);
                if (!_damageables.Contains(damageable))
                {
                    _damageables.Add(damageable);
                    StartCoroutine(ApplyTickDamage(damageable));
                }
            }
        }
        
        /*private void OnTriggerStay2D(Collider2D other)
        {
            // Check if the other object has a component that implements IDamageable
            if (other.TryGetComponent(out IDamageable damageable) && damageable.IsDamageable())
            {
                
                
            }
        }*/

        private IEnumerator ApplyTickDamage(IDamageable damageable)
        {
            while (_damageables.Count > 0 && _damageables.Contains(damageable))
            {
                damageable.TakeDamage(5);
                yield return new WaitForSeconds(1f);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            // Remove the damageable from the list when it exits the trigger
            if (other.TryGetComponent(out IDamageable damageable))
            {
                _damageables.Remove(damageable);
                StopCoroutine(ApplyTickDamage(damageable));
            }
        }
    }
}