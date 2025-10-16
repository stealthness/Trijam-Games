using _Scripts.Core;
using _Scripts.Witch;
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
                var playerPos = FindAnyObjectByType<PlayerController>().gameObject.transform.position;
                var direction = (playerPos - transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * pumkinForce, ForceMode2D.Impulse);
            }
        }

        protected override void ResetPosition()
        {
            base.ResetPosition();
            hasThrown = false;
        }
    }
}