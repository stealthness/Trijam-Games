using System;
using System.Collections;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Snakes
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private float speed = 3f;
        [SerializeField] private bool moveMade = false;
        [SerializeField] private float playerMoveDistance = 1.5f;
        [SerializeField] private float playerTimeToMove = 0.5f;
        private bool _isMoving = false;
        private Vector3 _previousPosition;


        private void Update()
        {
            if (GameManager.Instance.gameTurn != TurnType.EnemyTurn) return;

            if (_isMoving) return;
            
            
            SelectDirection();
            _previousPosition = transform.position;
            StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));
        }

        private void SelectDirection()
        {
            System.Random rand = new System.Random();
            var dir = rand.Next(0, 4);
            direction = dir switch
            {
                0 => Vector2.up,
                1 => Vector2.down,
                2 => Vector2.left,
                3 => Vector2.right,
                _ => direction
            };
        }


        private IEnumerator MoveByDirection(float distance, float duration)
        {
            _isMoving = true;
            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + (Vector3)direction.normalized * distance;

            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                transform.position = Vector3.Lerp(startPos, targetPos, t);
                yield return null;
            }

            transform.position = targetPos;
            _isMoving = false;
            GameManager.Instance.PlayerTurn();
        }



    }
}