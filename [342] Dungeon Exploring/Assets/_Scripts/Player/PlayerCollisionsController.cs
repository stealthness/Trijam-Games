using _Scripts.Enemies;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisionsController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var tag = other.gameObject.tag;
            switch (tag)
            {
                case "Fire":
                    Debug.Log("Player hit fire!");
                    Burn();
                    break;
                case "Enemy":
                    Debug.Log("Player hit enemy!");
                    Melt();
                    break;
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Door"))
            {
                var door = other.gameObject.GetComponentInParent<DoorScript>();
                door.CheckAndOpenDoor();
            }
        }

        private void Melt()
        {
            _playerController.DisableControl();
            _animator.SetTrigger("Melt");
            float length = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            Invoke(nameof(GameOver), length);
            
        }

        private void GameOver()
        {
            GameManager.Instance.GameOver();
        }

        private void Burn()
        {
            _playerController.DisableControl();
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}