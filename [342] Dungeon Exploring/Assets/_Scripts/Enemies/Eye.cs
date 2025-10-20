using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Enemies
{
    public class Eye : WaypointFollower
    {
        public GameObject bulletPrefab;

        [SerializeField] private float shootTime;
        
        
        private void Awake()
        {
            speed = 3f;
            waitTime = 3f;
            if (waypoints.Length == 0)
            {
                Debug.LogError("No points assigned for Eye movement.");
            }
        }

        protected new virtual void Start()
        {
            base.Start();
        }
        

        private IEnumerator ShootTimeRoutine()
        {
            var numberOfWaves = 3;
            
            while (numberOfWaves > 0)
            {
                ShootCircle();
                yield return new WaitForSeconds(1f);
                numberOfWaves--;
            }
        }
        

        private void ShootCircle()
        {
            for (var i = 0; i < 10; i++){
                var bullet = Instantiate(bulletPrefab, enemyObject.transform.position, Quaternion.identity);
                var randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = randomDirection * 5f;
            }
        }

        protected new IEnumerator MoveToPoint()
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
                StartCoroutine(ShootTimeRoutine());
                yield return new WaitForSeconds(shootTime);
                if (!gameObject.activeSelf)
                {
                    yield break;
                }
            }
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}