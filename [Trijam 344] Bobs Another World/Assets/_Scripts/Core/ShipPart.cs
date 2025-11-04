using System;
using _Scripts.Manager;
using UnityEngine;

namespace _Scripts.Core
{
    public class ShipPart : MonoBehaviour
   	{

		public static int totalShipParts = 5;


        private void Start()
        {
            totalShipParts = 5;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Ship Part Collected");
                gameObject.SetActive(false);
				totalShipParts--;
				CheckWinCondition();
            }
        }
		

		private void CheckWinCondition()
        {
            if (totalShipParts <= 0)
            {
                Debug.Log("All Ship Parts Collected! You Win!");
                // Implement win condition logic here (e.g., load next level, show win screen, etc.)
                GameManager.Instance.GameWon();
            }
        }
	}
}