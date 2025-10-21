using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Interactable
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private bool isExitPortal = false;

        public Transform PortalTargetLocation;


        private void Awake()
        {
            if (!isExitPortal && !PortalTargetLocation)
            {
                Debug.LogError("PortalTargetLocation is not assigned for portal: " + gameObject.name);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (isExitPortal)
            {
                GameManager.Instance.GameOverWon();
            }
            else
            {
                other.transform.position = PortalTargetLocation.position;
                FindAnyObjectByType<Camera>().transform.position = new Vector3(PortalTargetLocation.position.x, PortalTargetLocation.position.y, -10f);
            }

            
        }
    }
}