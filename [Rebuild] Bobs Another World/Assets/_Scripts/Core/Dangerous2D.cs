using System;
using _Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Core
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Dangerous2D : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Collider2D _col;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.bodyType = RigidbodyType2D.Static;
            _col = GetComponent<Collider2D>();
            _col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth ph))
            {
                ph.TakeDamage(10);
            }
        }
    }
}