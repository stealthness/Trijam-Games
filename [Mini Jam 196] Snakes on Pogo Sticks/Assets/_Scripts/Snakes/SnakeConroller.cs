using System;
using UnityEngine;

namespace _Scripts.Snakes
{
    public class SnakeController : MonoBehaviour
    {
        
        [SerializeField] private Vector2 direction = Vector2.up;
        [SerializeField] private float speed = 3f;

        private void Update()
        {
            if (transform.position.y > 7f)
            {
                var randomX = UnityEngine.Random.Range(-1f, 1f);
                var downY = -1f;
                direction = new Vector2(randomX, downY).normalized;
            }
            
            if (transform.position.y < 0.2f)
            {
                var randomX = UnityEngine.Random.Range(-1f, 1f);
                var upY = 1f;
                direction = new Vector2(randomX, upY).normalized;
            }
        }


        private void LateUpdate()
        {
            transform.Translate(direction * (speed * Time.deltaTime), Space.World);
        }
    }
}