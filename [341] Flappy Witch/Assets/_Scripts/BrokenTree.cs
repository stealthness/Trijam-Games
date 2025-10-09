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
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Hit BrokenTree");
                Time.timeScale = 0;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("BrokenTree"))
            {
                Debug.Log("Player Hit BrokenTree");
                Time.timeScale = 0;
            }
        }
    }
}