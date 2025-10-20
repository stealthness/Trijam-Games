using System;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class Blob : MonoBehaviour
    {
        public GameObject blobIdle;
        public GameObject blobExplode;

        public AudioSource audioSource;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Blob hit by player  " + other.gameObject.name);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
                Debug.Log("Blob hit by player");
                ExplodeBlob();
            }
            
            if (other.CompareTag("Player"))
            {
                Debug.Log("Blob hit the player");
                ExplodeBlob();
                
            }
        }

        private void ExplodeBlob()
        {
            blobExplode.SetActive(true);
            blobIdle.SetActive(false);
            audioSource.Play();
            var animationLength = GetCurrentAnimationClipLength();
            Debug.Log("Animation Length: " + animationLength);
            Destroy(gameObject, animationLength);
        }


        private float GetCurrentAnimationClipLength(int layer = 0)
        {
            var clips = blobExplode.GetComponent<Animator>().GetCurrentAnimatorClipInfo(layer);
            if (clips != null && clips.Length > 0)
            {
                var clip = clips[0].clip;
                if (clip != null)
                    return clip.length;
            }
            return 0f;
        }
    }
}