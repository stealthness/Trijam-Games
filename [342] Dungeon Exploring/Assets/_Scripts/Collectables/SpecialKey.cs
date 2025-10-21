using System;
using _Scripts.Enemies;
using UnityEngine;

namespace _Scripts.Collectables
{
    public class SpecialKey : MonoBehaviour
    {

        public GameObject SpecialDoor;
        public GameObject SpecialKeyDoor;


        private void Awake()
        {
            GetComponentInChildren<Collider2D>().enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            SpecialDoor.GetComponent<DoorScript>().OpenSpecialDoor();
            Destroy(gameObject);
        }

        public void ShowKey()
        {
            GetComponentInChildren<Collider2D>().enabled = true;
            SpecialKeyDoor.SetActive(true);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var box = GetComponent<Collider2D>();
            if (box != null)
                Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.bounds.size);
            
        }
    }
}