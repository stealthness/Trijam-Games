using UnityEngine;

namespace _Scripts.Enemies
{
    public class Base2DMovement : MonoBehaviour
    {
        private Vector2 _direction = Vector2.up;
        [SerializeField] private float moveSpeed = 1f;


        private void LateUpdate()
        {
            transform.Translate(_direction * (moveSpeed * Time.deltaTime), Space.World);
        }
    }
}