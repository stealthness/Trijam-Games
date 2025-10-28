using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerShield : MonoBehaviour
    {
        
        public bool isShieldActive = false;
        
        [SerializeField] private int turnsPerShield = 3;
        [SerializeField] private int remainingShieldTurns = 0;
        public SpriteRenderer _spriteRenderer;
        

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
            Debug.Log("Shield activated");
            isShieldActive = true;
            _spriteRenderer.enabled = true;
            remainingShieldTurns = turnsPerShield;
        }

        public void DeactivateShield()
        {
            Debug.Log("Shield deactivated");
            isShieldActive = false;
            _spriteRenderer.enabled = false;
        }
    }
}