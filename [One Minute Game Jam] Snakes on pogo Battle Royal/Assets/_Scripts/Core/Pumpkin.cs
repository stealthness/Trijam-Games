using System;
using _Scripts.Manager;
using UnityEngine;

namespace _Scripts.Core
{
    public class Pumpkin : Collidable2D
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PumpkinSpawner.Instance.DestroyPumpkin(this.gameObject);
            }
        }
    }
}