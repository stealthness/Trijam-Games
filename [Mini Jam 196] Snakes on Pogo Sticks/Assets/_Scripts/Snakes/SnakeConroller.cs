
using System.Collections;
using System.Collections.Generic;
using _Scripts.Board;
using _Scripts.Core;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Snakes
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private bool isRed = false;
        [SerializeField] private Vector2 previousDirection = Vector2.up;
        [SerializeField] private float speed = 3f;
        [SerializeField] private bool moveMade = false;
        [SerializeField] private float playerMoveDistance = 1.5f;
        [SerializeField] private float playerTimeToMove = 0.5f;
        [SerializeField] private int startRow = 1;
        [SerializeField] private int startCol = 1;
        [SerializeField] private int curRow = 1;
        [SerializeField] private int curCol = 1;
        [SerializeField] private int stunDurationTicks = 5;
        [SerializeField] private int ticksStunned = 0;
        
        private bool _isMoving = false;
        private BoardPosition _position;


        public void Start()
        {
            _position = BoardPosition.CreateBoardPosition(startRow, startCol);
            curRow = _position.Row;
            curCol = _position.Col;
            var anim = GetComponent<Animator>();
            var randomStart = Random.Range(0f, 1f);
            anim.Play("Green-Snake-Pogoing_Clip", -1, randomStart);
        }


        private void Update()
        {
            if (GameManager.Instance.gameTurn != TurnType.EnemyTurn) return;

            if (_isMoving) return;
            
            if (ticksStunned > 0)
            {
                Debug.Log("Snake is still stunned : " + ticksStunned);
                return;
            }
            
            SelectDirection();
            StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));

            /*if (isRed && direction == previousDirection)
            {
                Debug.Log("Red Snake: second move in same direction");
                SelectDirection();
                StartCoroutine(MoveByDirection(playerMoveDistance, playerTimeToMove));
            }*/
            // updating for debugging
            curRow = _position.Row;
            curCol = _position.Col;
        }

        private void SelectDirection()
        {
            var possibleDirections = new List<Vector2>{Vector2.up, Vector2.down, Vector2.left, Vector2.right};
            
            var previousDirection = -direction; // prevent going back
            possibleDirections.Remove(previousDirection);
            
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

            if (possibleDirections.Contains(direction))
            {
                possibleDirections.Add(direction); // double chance to continue in same direction
            }
            // choose random direction from possible
            var randomDirection = possibleDirections[UnityEngine.Random.Range(0, possibleDirections.Count)];
            previousDirection = direction;
            direction = randomDirection;
            

        }


        private IEnumerator MoveByDirection(float distance, float duration)
        {

            
            _position.MovePosition(direction);
            
            _isMoving = true;
            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + (Vector3)direction.normalized * distance;
            
            if (direction == previousDirection && Random.Range(0f, 1f) > 0.4f && BoardPosition.IsValidPosition(_position.Row + (int)direction.y, _position.Col + (int)direction.x))
            {
                _position.MovePosition(direction);
                targetPos += (Vector3)direction.normalized * distance;
            }
            
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

        public void Tick()
        {
            if (ticksStunned > 0)
            {
                ticksStunned--;            
                if (ticksStunned == 0)
               {
                   GetComponent<BoxCollider2D>().enabled = true;
                   GetComponent<SpriteRenderer>().color = Color.white;
                   Debug.Log("Snake recovered from stun!");
               }
            }

        }

        public void Stun()
        {
            Debug.Log("Snake stunned!");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.brown;
            ticksStunned = stunDurationTicks;
        }
    }
}