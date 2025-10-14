using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Core
{
    public class CamaraManager : MonoBehaviour
    {
        public GameObject player;
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
            var playerObj = GameObject.FindGameObjectWithTag("Player");
        }


        private void LateUpdate()
        {
            var newCameraPosition = new Vector3( _camera.transform.position.x, player.transform.position.y, _camera.transform.position.z);
            _camera.transform.position = newCameraPosition;

        }
    }
}