using System.Collections;
using _Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

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
        public Transform playerTransform;
        
        public void TriggerShake(Vector2 eyePosition)
        {

            if (CheckPlayerDistanceToTarget())
            {
                //var shakeMagnitudeByDistance = 1 - Vector3.Distance(transform.position, eyePosition) / maxPlayerDistanceDropoff;
                StartCoroutine(Shake(shakeDuration, shakeMagnitude));
                
            }
        }

        private bool CheckPlayerDistanceToTarget()
        {
            var playerDistance = Vector3.Distance(playerTransform.position, floatEyeTransform.position);
            return playerDistance <= maxPlayerDistanceDropoff;
        }


        private IEnumerator Shake(float duration, float magnitude)
        {
            var originalPos = transform.localPosition;
            var elapsed = 0.0f;
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