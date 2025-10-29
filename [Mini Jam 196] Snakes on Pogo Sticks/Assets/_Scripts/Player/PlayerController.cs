
using System;
using System.Collections;
using _Scripts.Board;
using _Scripts.Core;
using _Scripts.Managers;
using _Scripts.Snakes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Sprite deadSprite;
        
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private float speed = 3f;
        [SerializeField] private bool moveMade = false;
        [SerializeField] private float playerMoveDistance = 1.5f;
        [SerializeField] private float playerTimeToMove = 0.5f;
        [Tooltip("Starting Row Position (1-indexed), Starting at Bottom, followed by Columns position [1-indexed], starting at Left")]
        [SerializeField] private Vector2Int startPos = Vector2Int.one;
        [SerializeField] private Vector2Int currentPos = Vector2Int.zero;
        
        //private BoardPosition _position;
        private bool _isMoving = false;
        private bool _isDead = false;
        
        private AudioSource _audioSource;
        private PlayerShield _playerShield;
        private PlayerCollisionDetection _collisionDetection;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerShield = GetComponent<PlayerShield>();
            _collisionDetection = GetComponent<PlayerCollisionDetection>();
        }


        private void Start()
        {
            //_position = BoardPosition.CreateBoardPosition(startPos.x, startPos.y);
            var x = (startPos.x - 1) * 1.5f;
            var y = (startPos.y - 1) * 1.5f;
            currentPos = startPos;
            transform.position = new Vector3(x, y, 0);
        }


        private void Update()
        {
            if (_isDead || _isMoving) return;
            
            _collisionDetection.CheckForCollisions();
            
            if (GameManager.Instance.gameTurn != TurnType.PlayerTurn) return;
            
            if (moveMade && !_isMoving)
            {
                // don't start if no direction
                if (direction == Vector2.zero)
                {
                    moveMade = false;
                    return;
                }
                StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));
            }
        }
        
        public void OnMove(InputValue value)
        {
            if (moveMade || _isMoving)
            {
                return;
            }
            
            if (GameManager.Instance == null) return;
            
            if (GameManager.Instance.gameTurn != TurnType.PlayerTurn) return;


            
            var inputDirection = value.Get<Vector2>();
            if (inputDirection.x == 0 || inputDirection.sqrMagnitude < 0.1f)
            {
                direction = Vector2.zero;
            }

            moveMade = true;

            if (!CheckForValidMove(inputDirection))
            {
                Debug.Log("Invalid move");
                direction = Vector2.zero;
                moveMade = false;
                return;
            }
            
            MakeMove(inputDirection);
        }

        private void MakeMove(Vector2 dir)
        {
            
            if (!moveMade) return;
            
            _playerShield.Tick();
            // replace later with event handler
            SnakeController[] snakes = FindObjectsByType<SnakeController>(FindObjectsSortMode.None);
            foreach (var snake in snakes)
            {
                snake.Tick();
            }
            
            
            if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                if (dir.y > 0)
                {
                    direction = Vector2.up;
                    currentPos.y++;
                }
                else
                {
                    currentPos.y--;
                    direction = Vector2.down;
                }
                moveMade = true;
            }
            else if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    direction = Vector2.right;
                    currentPos.x++;
                }
                else
                {
                    direction = Vector2.left;
                    currentPos.x--;
                }
                    
                moveMade = true;
            }
            
            //_position = BoardPosition.CreateBoardPosition(currentPos.x, currentPos.y);
        }

        private bool CheckForValidMove(Vector2 inputDirection)
        {
            if (Mathf.Abs(inputDirection.y) > Mathf.Abs(inputDirection.x))
            {
                if (inputDirection.y > 0)
                {
                    return BoardPosition.IsValidPosition(currentPos.x , currentPos.y+ 1);
                }
                else
                {
                    return BoardPosition.IsValidPosition(currentPos.x , currentPos.y- 1);
                }

            }
            else // (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
            {
                if (inputDirection.x > 0)
                {
                    return BoardPosition.IsValidPosition(currentPos.x + 1, currentPos.y);
                }
                else
                {
                    return  BoardPosition.IsValidPosition(currentPos.x - 1, currentPos.y);
                }
            }
        }

        private IEnumerator MoveByDirection(float distance, float duration)
        {
            _isMoving = true;
            var startPos = transform.position;
            var targetPos = startPos + (Vector3)direction.normalized * distance;

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
            moveMade = false;
            direction = Vector2.zero;
            GameManager.Instance.EnemyTurn();
        }
        public void Die(string msg)
        {
            Debug.Log(msg);
            _audioSource.Play();
            GetComponent<SpriteRenderer>().sprite = deadSprite;
            _isDead = true;
            GameManager.Instance.GameOver();
        }

        public void ResetPosition()
        {
            var x = (startPos.x - 1) * 1.5f;
            var y = (startPos.y - 1) * 1.5f;
            currentPos = startPos;
            transform.position = new Vector3(x, y, 0);
        }
    }
}