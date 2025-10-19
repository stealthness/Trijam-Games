using System;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerWeaponController : MonoBehaviour
    {
        public static PlayerWeaponController Instance;
        public AudioClip _daggersPickup;

        private AudioSource _audioSource;
        
        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PickUpWeapon(WeaponTypes weaponType)
        {
            switch (weaponType)
            {
                case WeaponTypes.Daggers:
                    Debug.Log("Player picked up Daggers");
                    _audioSource.PlayOneShot(_daggersPickup);
                    // Add logic to equip daggers
                    break;
                default:
                    Debug.Log("Unknown weapon type");
                    break;
            }
        }
    }


    public enum WeaponTypes
    {
        Daggers
    }
}