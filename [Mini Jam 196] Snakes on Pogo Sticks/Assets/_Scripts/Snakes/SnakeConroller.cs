
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Snakes
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private float speed = 3f;
        [SerializeField] private bool moveMade = false;
        [SerializeField] private float playerMoveDistance = 1.5f;
        [SerializeField] private float playerTimeToMove = 0.5f;
        [SerializeField] private int startRow = 1;
        [SerializeField] private int startCol = 1;
        private bool _isMoving = false;
        private BoardPosition _position;


        public void Start()
        {
            _position = BoardPosition.CreateBoardPosition(startRow, startCol);
        }


        private void Update()
        {
            if (GameManager.Instance.gameTurn != TurnType.EnemyTurn) return;

            if (_isMoving) return;
            
            
            SelectDirection();
            StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));
        }

        private void SelectDirection()
        {
            var possibleDirections = new List<Vector2>{Vector2.up, Vector2.down, Vector2.left, Vector2.right};
            
            switch (_position.Row)
            {
                case 1:
                    possibleDirections.Remove(Vector2.down);
                    break;
                case 8:
                    possibleDirections.Remove(Vector2.up);
                    break;
            }

            switch (_position.Col)
            {
                case 1:
                    possibleDirections.Remove(Vector2.left);
                    break;
                case 8:
                    possibleDirections.Remove(Vector2.right);
                    break;
            }
            var randomDirection = possibleDirections[UnityEngine.Random.Range(0, possibleDirections.Count)];
            foreach (var v in possibleDirections)
            {
                Debug.Log(v);
            }
            Debug.Log("Snake: random direction" + randomDirection + ", from position: " + _position.Row );
            direction = randomDirection;

        }


        private IEnumerator MoveByDirection(float distance, float duration)
        {
            _position.MovePosition(direction);
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