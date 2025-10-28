using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerShield : MonoBehaviour
    {
        
        public bool isShieldActive = false;
        
        [SerializeField] private int turnsPerShield = 3;
        [SerializeField] private int remainingShieldTurns = 0;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Tick()
        {
            remainingShieldTurns--;
            if (remainingShieldTurns <= 0)
            {
                DeactivateShield();
            }
        }
        
        
        public void ActivateShield()
        {
            isShieldActive = true;
            _spriteRenderer.enabled = true;
            remainingShieldTurns = turnsPerShield;
        }

        public void DeactivateShield()
        {
            isShieldActive = false;
            _spriteRenderer.enabled = false;
        }
    }
}