using System.Collections;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// WaypointFollower moves an enemy GameObject along a series of waypoints in a loop.
    /// </summary>
    public class WaypointFollower : MonoBehaviour
    {
        [Tooltip("Array of waypoints for the enemy to follow.")]
        public Transform[] waypoints;
        [Tooltip("The enemy GameObject that will move along the waypoints.")]
        public GameObject enemyObject;
        
        [Tooltip("Movement speed of the enemy.")]
        [SerializeField] protected float speed = 5f;
        [Tooltip("Current index of the waypoint the enemy is moving towards.")]
        [SerializeField] protected int pointIndex;
        [Tooltip("Time to wait at each waypoint.")]
        [SerializeField] protected float waitTime = 1f;

        private void Awake()
        {
            if (waypoints.Length == 0)
            {
                Debug.LogError("No points assigned for enemy movement.");
            }
        }


        protected void Start()
        {
            StartCoroutine(nameof(MoveToPoint));
        }
        

        protected IEnumerator MoveToPoint()
        {
            while (true)
            {
                var targetPoint = waypoints[pointIndex];
                while (Vector2.Distance(enemyObject.transform.position, targetPoint.position) > 0.1f)
                {
                    
                    enemyObject.transform.position = Vector3.MoveTowards(enemyObject.transform.position, 
                        targetPoint.position, speed * Time.deltaTime);
                    yield return null;
                }

                pointIndex = (pointIndex + 1) % waypoints.Length;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}