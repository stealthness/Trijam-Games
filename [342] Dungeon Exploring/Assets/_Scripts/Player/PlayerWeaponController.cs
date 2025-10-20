using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerWeaponController : MonoBehaviour
    {
        public static PlayerWeaponController Instance;
        public AudioClip daggersPickup;

        private AudioSource _audioSource;
        [SerializeField] private WeaponTypes playerWeapon = WeaponTypes.None;
        private bool _onCooldown = false;
        public GameObject daggersPrefab;

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
                    _audioSource.PlayOneShot(daggersPickup);
                    playerWeapon = WeaponTypes.Daggers;
                    break;
                default:
                    Debug.Log("Unknown weapon type");
                    break;
            }
        }


        public void OnAttack()
        {
            if (playerWeapon == WeaponTypes.None)
            {
                Debug.Log("Player picked up No weapon yet");
                return;
            }

            if (_onCooldown)
            {
                Debug.Log("Player Weapon Cooldown");
                return;
            }
            
            Debug.Log("Player shoots with " + playerWeapon);
            switch (playerWeapon)
            {
                case WeaponTypes.Daggers:
                    StartCoroutine(ShootDaggers());
                    break;
                default:
                    Debug.Log("Unknown weapon type");
                    break;
            }
        }

        private IEnumerator ShootDaggers()
        {
            _onCooldown = true;
            CreateDaggers();
            yield return new WaitForSeconds(0.1f);
            _onCooldown = false;
        }

        private void CreateDaggers()
        {
            var daggers = Instantiate(daggersPrefab, transform.position, transform.rotation);
            if (GetComponent<SpriteRenderer>().flipX)
            {
                daggers.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * 10f;
            }
            else
            {
                daggers.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * 10f;
            }
            
            daggers.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            StartCoroutine(DestroyDaggersAfterTime(daggers));
        }

        private IEnumerator DestroyDaggersAfterTime(GameObject daggers)
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(daggers);
        }
    }


    public enum WeaponTypes
    {
        Daggers, None
    }
}