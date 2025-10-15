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
            var posY = Mathf.Clamp(player.transform.position.y, 0, 10f);
            var newCameraPosition = new Vector3( _camera.transform.position.x, posY, _camera.transform.position.z);
            _camera.transform.position = newCameraPosition;

        }
    }
}