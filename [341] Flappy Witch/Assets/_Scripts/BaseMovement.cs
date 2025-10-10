using UnityEngine;

namespace _Scripts
{
    public class BaseMovement : MonoBehaviour
    {
        protected float _worldMovementSpeed;
        protected float _resetPositionX = -10f;
        protected float _startPositionX = 20f;
        
      
        private void Awake()
        {
            _worldMovementSpeed = 2f;
        }

        
        
        private void Update()
        {
            transform.Translate(Vector3.left * (_worldMovementSpeed * Time.deltaTime));
             
            if (transform.position.x < _resetPositionX)
            {
                ResetPosition();
            }
        }       
        
        protected virtual void ResetPosition()
        {
            transform.position = new Vector3(_startPositionX, transform.position.y, transform.position.z);
        }
        
        
    }

}