using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {

        public  Weapon weapon;
        
        private Vector2 _dir;
        [SerializeField] private float playerSpeed = 5f;
        

        public void OnMove(InputValue value)
        {
            var moveInput = value.Get<Vector2>();
            _dir = new Vector2(moveInput.x, 0);
            CheckDirection();
        }
        
        public void OnAttack()
        {
            Debug.Log("Attack!");
            weapon.OnAttack();
            
        }
        

        private void CheckDirection()
        {
            transform.localScale = _dir.x switch
            {
                > 0 => new Vector3(1, 1, 1),
                < 0 => new Vector3(-1, 1, 1),
                _ => transform.localScale
            };
        }


        private void LateUpdate()
        {
            transform.Translate(_dir * (Time.deltaTime * playerSpeed));
            if (transform.position.x > 14f)
            {
                transform.position = new Vector3(14f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -14f)
            {
                transform.position = new Vector3(-14f, transform.position.y, transform.position.z);
            }
        }
    }
}
