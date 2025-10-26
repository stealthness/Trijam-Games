using System;
using System.Collections;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private float speed = 3f;
        [SerializeField] private bool moveMade = false;
        [SerializeField] private float playerMoveDistance = 1.25f;
        [SerializeField] private float playerTimeToMove = 0.5f;
        private bool _isMoving = false;
        private Vector3 _previousPosition;


        private void Update()
        {
            if (GameManager.Instance.gameTurn != TurnType.PlayerTurn) return;
            
            if (moveMade && !_isMoving)
            {
                // don't start if no direction
                if (direction == Vector2.zero)
                {
                    moveMade = false;
                    return;
                }

                moveMade = false;
                _previousPosition = transform.position;
                StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));
            }
        }


        public void OnMove(InputValue value)
        {
            if (GameManager.Instance.gameTurn != TurnType.PlayerTurn) return;


            Vector2 inputDirection = value.Get<Vector2>();
            if (inputDirection.x == 0)
            {
                direction = Vector2.zero;
            }

            if (Mathf.Abs(inputDirection.y) > Mathf.Abs(inputDirection.x))
            {
                direction = inputDirection.y > 0 ? Vector2.up : Vector2.down;
                moveMade = true;

            }
            else if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
            {
                direction = inputDirection.x > 0 ? Vector2.right : Vector2.left;
                moveMade = true;
            }
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
            GameManager.Instance.NextTurn();
        }


    }
}