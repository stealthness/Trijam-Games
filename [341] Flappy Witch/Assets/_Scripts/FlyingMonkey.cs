using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class FlyingMonkey : BaseMovement
    {
        
        
        [SerializeField] private float minYStartPos = 1f;
        [SerializeField] private float maxYStartPos = 4f;

        private void Start()
        {
            _worldMovementSpeed = 4f;
        }

        protected override void ResetPosition()
        {
            var startY = Random.Range(minYStartPos, maxYStartPos);
            transform.position = new Vector3(_startPositionX, startY, transform.position.z);
        }
        
    }
}