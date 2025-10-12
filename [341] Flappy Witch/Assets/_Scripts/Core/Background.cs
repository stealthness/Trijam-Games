using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// Controls the background scrolling effect.
    /// </summary>
    public class Background : MonoBehaviour
    {
        public GameObject[] backgroundSprites;
        
        [SerializeField] private float backgroundScrollSpeedRation = 2f;
        [SerializeField] private float backgroundWidth = 35f;

        private void Update()
        {
            
            foreach (var sprite in backgroundSprites)
            {
                var backgroundScrollSpeed = GameManager.Instance.gameSpeed / backgroundScrollSpeedRation;
                
                sprite.transform.position += Vector3.left * (backgroundScrollSpeed * Time.deltaTime);
                if (sprite.transform.position.x <= -backgroundWidth)
                {
                    sprite.transform.position += Vector3.right * (2 * backgroundWidth);
                }
            }
        }
    }
}