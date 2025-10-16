using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This class manages the camera's position to follow the player within specified vertical bounds.
    /// </summary>
    public class CamaraManager : MonoBehaviour
    {
        [SerializeField] private float maxCameraPosY = 10f;
        [SerializeField] private float minCameraPosY = 0f;
        
        public GameObject player;
        
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }


        private void LateUpdate()
        {
            var posY = Mathf.Clamp(player.transform.position.y, minCameraPosY, maxCameraPosY);
            var newCameraPosition = new Vector3( _camera.transform.position.x, posY, _camera.transform.position.z);
            _camera.transform.position = newCameraPosition;
        }
    }
}