
using System.Collections;
using _Scripts.Board;
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
        [SerializeField] private int startRow = 1;
        [SerializeField] private int startCol = 1;
        [SerializeField] private int currentRow = 0;
        [SerializeField] private int currentCol = 0;
        private BoardPosition _position;
        private bool _isMoving = false;
        private Vector3 _previousPosition;


        private void Start()
        {
            _position = BoardPosition.CreateBoardPosition(startRow, startCol);
            var x = (startCol - 1) * 1.5f;
            var y = (startRow - 1) * 1.5f;
            currentRow = startRow;
            currentCol = startCol;

            transform.position = new Vector3(x, y, 0);
        }


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

            
            var inputDirection = value.Get<Vector2>();
            if (inputDirection.x == 0 || inputDirection.sqrMagnitude < 0.1f)
            {
                direction = Vector2.zero;
            }

            if (!CheckForValidMove(inputDirection))
            {
                Debug.Log("Invalid move");
                direction = Vector2.zero;
                return;
            }
            MakeMove(inputDirection);
        }

        private void MakeMove(Vector2 dir)
        {
            if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                if (dir.y > 0)
                {
                    direction = Vector2.up;
                    currentRow += 1;
                }
                else
                {
                    currentRow -= 1;
                    direction = Vector2.down;
                }
                moveMade = true;
            }
            else if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    direction = Vector2.right;
                    currentCol += 1;
                }
                else
                {
                    direction = Vector2.left;
                    currentCol -= 1;
                }
                    
                moveMade = true;
            }
            
            _position = BoardPosition.CreateBoardPosition(currentRow, currentCol);
        }

        private bool CheckForValidMove(Vector2 inputDirection)
        {
            if (Mathf.Abs(inputDirection.y) > Mathf.Abs(inputDirection.x))
            {
                if (inputDirection.y > 0)
                {
                    return BoardPosition.IsValidPosition(_position.Row + 1, _position.Col);
                }
                else
                {
                    return BoardPosition.IsValidPosition(_position.Row - 1, _position.Col);
                }

            }
            else // (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
            {
                if (inputDirection.x > 0)
                {
                    return BoardPosition.IsValidPosition(_position.Row, _position.Col + 1);
                }
                else
                {
                    return  BoardPosition.IsValidPosition(_position.Row, _position.Col - 1);
                }
            }
        }

        private IEnumerator MoveByDirection(float distance, float duration)
        {
            Debug.Log("Player moving " + direction);
            _isMoving = true;
            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + (Vector3)direction.normalized * distance;
            
            UpdateBoardPosition();

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

        private void UpdateBoardPosition()
        {
            _position.SetPosition(currentRow, currentCol);
            Debug.Log("Player Board Position updated to: Row " + _position.Row + ", Col " + _position.Col);
        }
    }
}