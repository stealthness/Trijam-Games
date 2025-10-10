using System;
using UnityEngine;

namespace _Scripts
{
    public class BrokenTree : MonoBehaviour
    {
        private float _worldMovementSpeed;

        private void Awake()
        {
            _worldMovementSpeed = 2f;
        }



        private void Update()
        {
            transform.Translate(Vector3.left * (_worldMovementSpeed * Time.deltaTime));
            
            if (transform.position.x < -10f)
            {
                transform.position = new Vector3(10f, transform.position.y, transform.position.z);
            }
        }

    }
}