using System;
using System.Collections;
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Enemies.Monk
{
    public class Monk : WaypointFollower
    {

        private new void Start()
        {
            base.Start();
            speed = 3f;
            waitTime = 0f;
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
                CheckDirection(targetPoint.position.x);
                yield return new WaitForSeconds(waitTime);
            }
        }

        private void CheckDirection(float dir)
        {
            enemyObject.transform.localScale = dir > transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}