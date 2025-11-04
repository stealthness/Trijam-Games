using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Core
{

    public class Dangerous : MonoBehaviour
    {
        public static Action<int> OnPlayerDamaged;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerDamaged?.Invoke(1);
            }
        }
        
        
    }
}