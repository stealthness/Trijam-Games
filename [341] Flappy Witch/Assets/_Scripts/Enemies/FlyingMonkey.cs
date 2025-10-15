using _Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Enemies
{
    public class FlyingMonkey : BaseMovement
    {
        
        
        [SerializeField] private float minYStartPos = 1f;
        [SerializeField] private float maxYStartPos = 10f;

        private void Start()
        {
            _worldMovementSpeed = 4f;
        }

        protected override void ResetPosition()
        {
            var startY = Random.Range(minYStartPos, maxYStartPos);
            var startX = Random.Range(_startPositionX - 4f, _startPositionX + 4f);
            transform.position = new Vector3(startX, startY, transform.position.z);
        }
        
    }
}