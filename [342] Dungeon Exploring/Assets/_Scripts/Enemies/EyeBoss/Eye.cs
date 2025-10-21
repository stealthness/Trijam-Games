using System;
using System.Collections;
using _Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Enemies
{
    public class Eye : WaypointFollower
    {
        public GameObject bulletPrefab;
        public CameraShake cameraShake;

        [SerializeField] private float shootTime;
        [SerializeField] private float bitSize = 1f;
        [SerializeField] private Vector2 eyeOffset = new Vector2(0,1f);
        [SerializeField] private int maxTentaclesWaves = 3;
        [SerializeField] private int startNumberOfTentacles = 10;
        [SerializeField] private int tentacleIncreasePerWave = 4;

        public AudioClip shotSound;
        
        public AudioSource audioSource;

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
            var numberOfWaves = maxTentaclesWaves;
            
            while (numberOfWaves > 0)
            {
                var numberOfTentacles = startNumberOfTentacles + (maxTentaclesWaves - numberOfWaves) * tentacleIncreasePerWave;
                ShootCircle(numberOfTentacles);
                yield return new WaitForSeconds(1f);
                numberOfWaves--;
            }
        }
        

        private void ShootCircle(int numberOfTentacles)
        {
            audioSource.PlayOneShot(shotSound);
            cameraShake.TriggerShake(transform.position);
            
            for (var i = 0; i < numberOfTentacles; i++){
                var randomDirection = Random.insideUnitCircle.normalized;
                var abit = new Vector3(randomDirection.x, randomDirection.y, 0) * bitSize + new Vector3(eyeOffset.x, eyeOffset.y, 0);
                var tentacle = Instantiate(bulletPrefab, enemyObject.transform.position + abit, Quaternion.identity);
                tentacle.GetComponent<EyeTentacle>().SetDirection(randomDirection);
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