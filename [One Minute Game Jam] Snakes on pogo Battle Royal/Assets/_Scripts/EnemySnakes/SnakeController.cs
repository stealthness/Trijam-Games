using System;
using _Scripts.Manager;
using UnityEngine;

namespace _Scripts.EnemySnakes
{
    [RequireComponent(typeof(SnakeMovement2D))]
    public class SnakeController : MonoBehaviour
    {
        private SnakeMovement2D _snakeMovement2D;

        private void Awake()
        {
            _snakeMovement2D = GetComponent<SnakeMovement2D>();
        }

        private void Update()
        {
            Invoke(nameof(FindNearestPumpkin), 1f);
        }

        private void FindNearestPumpkin()
        {
            var nearestPumpkinPos = PumpkinSpawner.Instance.GetNearestPumpkin(transform.position);

            Vector2 directionToPumpkin = (nearestPumpkinPos - transform.position).normalized;
            _snakeMovement2D.Move(directionToPumpkin);

        }
    }
}