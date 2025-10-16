using _Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Enemies
{
    
    /// <summary>
    /// Controls the movement and positioning of a flying monkey enemy in the game.
    /// </summary>
    public class FlyingMonkey : BaseMovement
    {
        
        
        [SerializeField] private float minYStartPos = 2f;
        [SerializeField] private float maxYStartPos = 10f;
        [SerializeField] private float variationFromStartPosX = 4f;

        private void Start()
        {
            WorldMovementSpeed = 4f;
        }

        /// <summary>
        /// Overrides the ResetPosition method to set a random starting position for the flying monkey.
        /// The Y position is randomized between minYStartPos and maxYStartPos, and the
        /// X position is randomized around StartPositionX.
        /// </summary>
        protected override void ResetPosition()
        {
            var startY = Random.Range(minYStartPos, maxYStartPos);
            var startX = Random.Range(StartPositionX - variationFromStartPosX, StartPositionX + variationFromStartPosX);
            transform.position = new Vector3(startX, startY, transform.position.z);
        }
        
    }
}