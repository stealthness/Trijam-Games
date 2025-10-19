using System.Collections;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class Windy : MonoBehaviour
    {
        public Transform[] points;
        public GameObject windyObject;
        
        
        [SerializeField] private float speed = 5;
        [SerializeField] private int pointIndex;
        [SerializeField] private float waitTime = 2f;
        
        
        private void Start()
        {
            pointIndex = 0;
            StartCoroutine("Wait");
        }


        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitTime);
            StartCoroutine("WMoveToPoint");
        }
        
        private IEnumerator WMoveToPoint()
        {

            while (true)
            {
                Transform targetPoint = points[pointIndex];
                while (Vector2.Distance(windyObject.transform.position, targetPoint.position) > 0.1f)
                {
                    
                    windyObject.transform.position = Vector3.MoveTowards(windyObject.transform.position, 
                        targetPoint.position, speed * Time.deltaTime);
                    yield return null;
                }

                pointIndex = (pointIndex + 1) % points.Length;
                yield return new WaitForSeconds(waitTime);
            }
        }

    }
}