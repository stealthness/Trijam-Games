using _Scripts.Enemies;
using UnityEngine;

namespace _Scripts.Collectables
{
    
    /// <summary>
    /// Handles the special key that opens a special door in the game.
    /// </summary>
    public class SpecialKey : MonoBehaviour
    {
        public GameObject specialDoor;
        public GameObject specialKeyObject;


        private void Awake()
        {
            GetComponentInChildren<Collider2D>().enabled = false;
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            specialDoor.GetComponent<DoorScript>().OpenSpecialDoor();
            Destroy(gameObject);
        }

        public void ShowKey()
        {
            GetComponentInChildren<Collider2D>().enabled = true;
            specialKeyObject.SetActive(true);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var box = GetComponent<Collider2D>();
            if (!box)
                Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.bounds.size);
            
        }
    }
}