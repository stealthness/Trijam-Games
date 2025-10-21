using _Scripts.Collectables;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class EyeCollisionDetection : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        public GameObject EyeBoss;
        public GameObject DeadEyeBoss;
        private void OnTriggerEnter2D(Collider2D other)
        {
            

            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
                Debug.Log("Eye hit by player  " + health);
                health -= 5;
                if (health <= 0)
                {
                    Debug.Log("Eye defeated");
                    FindAnyObjectByType<SpecialKey>().ShowKey();
                    EyeBoss.SetActive(false);
                    Instantiate(DeadEyeBoss, transform.position, Quaternion.identity);
                    //GameManager.Instance.GameOverWon();
                }
                   
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerCollisionsController>().Melt();
            }
            
        }
    }
}