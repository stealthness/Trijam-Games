using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// Is a base class for moving objects in the game world. It handles horizontal movement
    /// </summary>
    public class BaseMovement : MonoBehaviour
    {
        protected float WorldMovementSpeed;
        protected const float ResetPositionX = -10f;
        protected const float StartPositionX = 20f;


        private void Awake()
        {
            WorldMovementSpeed = 2f;
        }
        
        protected virtual void Update()
        {
            transform.Translate(Vector3.left * (WorldMovementSpeed * Time.deltaTime));
             
            if (transform.position.x < ResetPositionX)
            {
                ResetPosition();
            }
        }       
        
        /// <summary>
        /// Resets the position of the object to the starting X position while maintaining its
        /// current Y and Z coordinates.
        /// </summary>
        protected virtual void ResetPosition()
        {
            transform.position = new Vector3(StartPositionX, transform.position.y, transform.position.z);
        }
        
        
    }

}