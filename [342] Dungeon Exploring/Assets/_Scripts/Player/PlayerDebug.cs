using UnityEngine;

namespace _Scripts.Player
{
    /// <summary>
    /// This class is used for debugging player-related features.
    /// </summary>
    public class PlayerDebug : MonoBehaviour
    {
        public Collider2D playerCollider;
        
        private void OnDrawGizmos()
        {
            if (!playerCollider) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(playerCollider.bounds.center, playerCollider.bounds.size);
        }
    }
}