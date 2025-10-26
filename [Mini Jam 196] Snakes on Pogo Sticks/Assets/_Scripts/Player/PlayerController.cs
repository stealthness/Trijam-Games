using System;
using System.Collections;
using _Scripts.Core;
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
        [SerializeField] private float playerMoveDistance = 1.5f;
        [SerializeField] private float playerTimeToMove = 0.5f;
        private bool _isMoving = false;
        private Vector3 _previousPosition;


        private void Update()
        {
            CheckForCollisions();
            
            if (GameManager.Instance.gameTurn != TurnType.PlayerTurn) return;
            
            if (moveMade && !_isMoving)
            {
                // don't start if no direction
                if (direction == Vector2.zero)
                {
                    moveMade = false;
                    return;
                }

                _previousPosition = transform.position;
                StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));
            }
        }

        private void CheckForCollisions()
        {
            if (_isMoving) return;

            var hits = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            foreach (var hit in hits)
            {
                if (hit.gameObject.CompareTag("Snake"))
                {
                    Debug.Log("Player collided with Snake!");
                    GameManager.Instance.GameOver();
                   
                }

                if (hit.gameObject.CompareTag("Coin"))
                {
                    Debug.Log("Player collided with Coin!");
                    WaveManager.Instance.CollectCoin(hit.gameObject);
                }
                
                if (hit.gameObject.CompareTag("PowerUp"))
                {
                    Debug.Log("Player collided with PowerUp!");
                    Destroy(hit.gameObject);
                }
            }
        }


        public void OnMove(InputValue value)
        {
            if (GameManager.Instance.gameTurn != TurnType.PlayerTurn) return;

            if (moveMade && !_isMoving) return;


            Vector2 inputDirection = value.Get<Vector2>();
            if (inputDirection.x == 0 || inputDirection.sqrMagnitude < 0.1f)
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
            Debug.Log("Player moving " + direction);
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
            GameManager.Instance.EnemyTurn();
            _isMoving = false;
            moveMade = false;
            direction = Vector2.zero;
        }


    }
}