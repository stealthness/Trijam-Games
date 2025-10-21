using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Collectables
{
    /// <summary>
    /// This class handles the collection of daggers by the player.
    /// </summary>
    public class Daggers : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            PlayerWeaponController.Instance.PickUpWeapon(WeaponTypes.Daggers);
            gameObject.SetActive(false);
        }
    }
}