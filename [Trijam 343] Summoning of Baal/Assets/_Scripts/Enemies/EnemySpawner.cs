
using UnityEngine;

namespace _Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        
        public GameObject smallEnemyPrefab;
        [Tooltip("Starting Top Left, Top Right, Bottom Right, Bottom Left")]
        public Transform[] spawnPointsLimits;


        private void Start()
        {
            SpawnEnemy();
    
        }

        private void SpawnEnemy()
        {
            InvokeRepeating(nameof(CreateWave), 5f, 10f);
        }
        
        
        private void CreateWave()
        {
            for (var i = 0; i < 5; i++)
            {
                SpawnDirection dir = GetRandomDirection();
                var randomPos = GetRandomStartPosition(dir);
                var smallEnemy = Instantiate(smallEnemyPrefab, randomPos, Quaternion.identity);
                smallEnemy.GetComponent<Base2DMovement>().SetMovementDirection(GetDirectionVector(dir));
            }
            
        }

        private Vector3 GetRandomStartPosition(SpawnDirection spawnDirection)
        {
            float ranX = 0;
            float ranY = 0;
            switch (spawnDirection)
            {
                case SpawnDirection.Left:
                    ranX = Random.Range(10f, 12f);
                    ranY = Random.Range(-10f, 10f);
                    break;
                case SpawnDirection.Top:
                    ranX = Random.Range(-5f,5f);
                    ranY = Random.Range(spawnPointsLimits[2].transform.position.y, spawnPointsLimits[2].transform.position.y + 2f);
                    break;
                case SpawnDirection.Right:
                    ranX = Random.Range(-10f,12f);
                    ranY = Random.Range(-5f, 5f);
                    break;
                case SpawnDirection.Bottom:
                    ranX = Random.Range(-5f,5f);
                    ranY = Random.Range(10f, 12f);
                    break;
                default:
                    ranX = 0;
                    ranY = 0;
                    break;
                    
                    
            }
            
            return new Vector3(ranX, ranY, 0);
        }
        
        
        private static Vector2 GetDirectionVector(SpawnDirection direction)
        {
            switch (direction)
            {
                case SpawnDirection.Left:
                    return Vector2.left;
                case SpawnDirection.Top:
                    return Vector2.up;
                case SpawnDirection.Right:
                    return Vector2.right;
                case SpawnDirection.Bottom:
                    return Vector2.down;
                default:
                    return Vector2.zero;
            }
        }
        
        private static SpawnDirection GetRandomDirection()
        {
            var directions = System.Enum.GetValues(typeof(SpawnDirection));
            return (SpawnDirection)directions.GetValue(Random.Range(0, directions.Length));
        }
        
    }
    
    
    enum SpawnDirection
    {
        Left,
        Top,
        Right,
        Bottom
        

    }
}