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
        
        [SerializeField] private int initialDamage = 10;
        [SerializeField] private int tickDamage = 5;
        [SerializeField] private float tickInterval = 1f;
        
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
                damageable.TakeDamage(initialDamage);
                if (!_damageables.Contains(damageable))
                {
                    _damageables.Add(damageable);
                    // Start a coroutine to apply tick damage
                    StartCoroutine(ApplyTickDamage(damageable));
                }
            }
        }
        


        private IEnumerator ApplyTickDamage(IDamageable damageable)
        {
            while (_damageables.Count > 0 && _damageables.Contains(damageable))
            {
                yield return new WaitForSeconds(tickInterval);
                // Apply tick damage at the end of the interval
                damageable.TakeDamage(tickDamage);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            // Remove the damageable from the list when it exits the trigger
            if (other.TryGetComponent(out IDamageable damageable))
            {
                _damageables.Remove(damageable);
                // Stop the coroutine for this damageable
                StopCoroutine(ApplyTickDamage(damageable));
            }
        }
    }
}