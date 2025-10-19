using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Collectables
{
    public class Daggers : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Here you can add code to update the player's coin count
                Debug.Log("Daggers collected!");
                PlayerWeaponController.Instance.PickUpWeapon(WeaponTypes.Daggers);
                gameObject.SetActive(false);
            }
        }
    }
}