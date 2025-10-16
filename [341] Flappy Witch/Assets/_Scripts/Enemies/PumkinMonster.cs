using System.Collections;
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.Enemies
{
    public class PumkinMonster : BaseMovement
    {

        private bool hasThrown = false;
        public GameObject pumpkinPrefab;
        [SerializeField] private float pumkinForce = 5f;


        protected override void Update()
        {
            base.Update();
            
            if (!hasThrown && transform.position.x < 5f)
            {
                hasThrown = true;
                var bullet = Instantiate(pumpkinPrefab, transform.position, Quaternion.identity);
                StartCoroutine(ThrowAgainAfterDelay(8f));
            }
        }

        private IEnumerator ThrowAgainAfterDelay(float f)
        {
            yield return new WaitForSeconds(f);
            var bullet = Instantiate(pumpkinPrefab, transform.position, Quaternion.identity);
        }

        protected override void ResetPosition()
        {
            base.ResetPosition();
            hasThrown = false;
        }
    }
}