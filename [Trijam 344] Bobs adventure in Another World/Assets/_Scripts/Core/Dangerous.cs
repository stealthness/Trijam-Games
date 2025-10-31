using System;
using UnityEngine;

namespace _Scripts.Core
{

    public class Dangerous : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerCollision = other.GetComponent<Player.PlayerCollision>();
                if (playerCollision != null)
                {
                    playerCollision.DamagePlayer();
                }
            }
        }
    }
}