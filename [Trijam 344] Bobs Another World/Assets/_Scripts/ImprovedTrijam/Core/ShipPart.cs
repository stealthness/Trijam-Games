using _Scripts.ImprovedTrijam.Manager;
using UnityEngine;

namespace _Scripts.ImprovedTrijam.Core
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
                ShipPartsUI.Instance.ShipPartsFound();
            }
        }
		


	}
}