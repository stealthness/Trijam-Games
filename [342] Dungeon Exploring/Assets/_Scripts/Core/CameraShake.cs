using System.Collections;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// This class handles camera shake effects.
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        public CameraFollow cameraFollow;
        
        [SerializeField] private float shakeDuration = 0.3f;
        [SerializeField] private float shakeMagnitude = 0.5f;
        [SerializeField] private float maxPlayerDistanceDropoff = 14f;

        public Transform floatEyeTransform;
        public Transform PlayerTransform;
        
        public void TriggerShake(Vector2 eyePosition)
        {

            if (CheckPlayerDistanceToTarget())
            {
                var distanceToPlayer = Vector3.Distance(PlayerTransform.position, floatEyeTransform.position);
                var shakeMaginitude = 1 - Vector3.Distance(transform.position, eyePosition) / maxPlayerDistanceDropoff;
                Debug.Log("Applying camera shake with magnitude: " + shakeMaginitude + " at distance: " + distanceToPlayer);
                StartCoroutine(Shake(shakeDuration, shakeMaginitude));

            }
            else
            {
                Debug.Log("Player too far from shake target, no shake applied.");
            }


        }

        private bool CheckPlayerDistanceToTarget()
        {
            var playerDistance = Vector3.Distance(PlayerTransform.position, floatEyeTransform.position);
            return playerDistance <= maxPlayerDistanceDropoff;
        }


        public IEnumerator Shake(float duration, float magnitude)
        {
            Debug.Log("Shaking");
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0.0f;
            cameraFollow.IsPaused = true;
            while (elapsed < duration)
            {
                
                var randomDirection = Random.insideUnitCircle.normalized * magnitude;

                transform.localPosition = originalPos + new Vector3(randomDirection.x, randomDirection.y, 0f);

                elapsed += Time.deltaTime;
                yield return null;
            }
            cameraFollow.IsPaused = false;
            transform.localPosition = originalPos;
        }
    }
}