using System;
using UnityEngine;

namespace _Scripts
{
    public class Background : MonoBehaviour
    {
        public GameObject[] backgroundSprites;

        private void Update()
        {
            foreach (GameObject sprite in backgroundSprites)
            {
                sprite.transform.position += Vector3.left * (GameManager.Instance.gameSpeed * Time.deltaTime);
                if (sprite.transform.position.x <= -35f)
                {
                    sprite.transform.position += Vector3.right * 70f;
                }
            }
        }
    }
}