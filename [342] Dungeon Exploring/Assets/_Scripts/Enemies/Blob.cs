using System;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class Blob : MonoBehaviour
    {
        public GameObject blobIdle;
        public GameObject blobExplode;
        public GameObject blobAttack;
        public GameObject rightCloud;
        public GameObject leftCloud;

        public AudioSource audioSource;
        
        private GameObject _activeBlobState;
        
         private void Start()
         {
             SetBlobState(BlobState.Idle);
             InvokeRepeating(nameof(BlobAttack), 5f, 5f);
         }       

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

        private void OnDrawGizmos()
        {
            var box = GetComponent<BoxCollider2D>();
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.size);
        }



        private void ExplodeBlob()
        {
            SetBlobState(BlobState.Exploding);
            audioSource.Play();
            var animationLength = GetCurrentAnimationClipLength();
            Debug.Log("Animation Length: " + animationLength);
            Destroy(gameObject, animationLength);
        }

        
        private void BlobAttack()
        {
            SetBlobState(BlobState.Attacking);
            var animationLength = GetCurrentAnimationClipLength();
            Debug.Log("Attack Animation Length: " + animationLength);
            Invoke(nameof(ResetToIdle), animationLength);
            leftCloud.SetActive(true);
            rightCloud.SetActive(true);
        }

        private float GetCurrentAnimationClipLength(int layer = 0)
        {
            var clips = _activeBlobState.GetComponent<Animator>().GetCurrentAnimatorClipInfo(layer);
            if (clips != null && clips.Length > 0)
            {
                var clip = clips[0].clip;
                if (clip != null)
                    return clip.length;
            }
            return 0f;
        }
        
        private void ResetToIdle()
        {
            SetBlobState(BlobState.Idle);
        }
        
        
        private void SetBlobState(BlobState state)
        {
            blobIdle.SetActive(state == BlobState.Idle);
            blobExplode.SetActive(state == BlobState.Exploding);
            blobAttack.SetActive(state == BlobState.Attacking);

            if (state != BlobState.Attacking){
                leftCloud.SetActive(false);
                rightCloud.SetActive(false);
            }
            
            _activeBlobState = state switch
            {
                BlobState.Idle => blobIdle,
                BlobState.Exploding => blobExplode,
                BlobState.Attacking => blobAttack,
                _ => blobIdle
            };
        }
    }

    internal enum BlobState
    {
        Idle,
        Exploding,
        Attacking
    }
}