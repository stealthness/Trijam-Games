using System.Collections;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class WaypointFollower : MonoBehaviour
    {
        public Transform[] waypoints;
        public GameObject enemyObject;
        
        
        [SerializeField] protected float speed;
        [SerializeField] protected int pointIndex;
        [SerializeField] protected float waitTime;

        private void Awake()
        {
            speed = 5f;
            waitTime = 1f;
            if (waypoints.Length == 0)
            {
                Debug.LogError("No points assigned for enemy movement.");
            }
        }


        protected void Start()
        {
            pointIndex = 0;
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