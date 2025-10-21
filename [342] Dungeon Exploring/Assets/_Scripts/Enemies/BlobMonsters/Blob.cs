using UnityEngine;

namespace _Scripts.Enemies.BlobMonsters
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
        
        [Tooltip("If true, the attack delay will be randomized between 0 and 5 seconds. otherwise, attacks will be" +
                 "syncronised at fixed at 5 seconds delay.")]
        [SerializeField] private bool randomizeAttackDelay = true;
        
         private void Start()
         {
             SetBlobState(BlobState.Idle);
             var attackDelay = (randomizeAttackDelay)?UnityEngine.Random.Range(0f, 5f):5f;
             InvokeRepeating(nameof(BlobAttack), attackDelay, 5f);
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
            Gizmos.color = (box.enabled)? Color.red:Color.green;
            Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.size);
        }



        private void ExplodeBlob()
        {
            SetBlobState(BlobState.Exploding);
            // Disable all 2D colliders on this GameObject and its children
            foreach (var col in GetComponentsInChildren<Collider2D>())
                col.enabled = false;
            audioSource.Play();
            var animationLength = GetCurrentAnimationClipLength();
            Debug.Log("Animation Length: " + animationLength);
            Destroy(gameObject, animationLength);
        }

        
        private void BlobAttack()
        {
            SetBlobState(BlobState.Attacking);
            var animationLength = GetCurrentAnimationClipLength();
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