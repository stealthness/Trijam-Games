using System;
using UnityEngine;

namespace _Scripts.Core
{
    public class Pumpkin : Collidable2D
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}