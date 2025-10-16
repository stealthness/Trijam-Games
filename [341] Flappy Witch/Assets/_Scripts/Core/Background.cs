using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Core
{
    /// <summary>
    /// Controls the background scrolling effect. Assumptions: Background sprites are arranged side by side.
    /// The width of each background sprite is larger than twice the camera width, all are the same size.
    /// The default scroll direction is left.
    /// </summary>
    public class Background : MonoBehaviour
    {
        /// <summary>
        /// The direction in which the background scrolls.
        /// </summary>
        [SerializeField] private Vector3 scrollDirection = Vector2.left;
        /// <summary>
        /// array of background sprites to be scrolled.
        /// </summary>
        public GameObject[] backgroundSprites;
        /// <summary>
        /// The ratio of the background scroll speed to the game speed.
        /// </summary>
        [SerializeField] private float backgroundScrollSpeedRatio = 2f;
        /// <summary>
        /// The width of the background sprites.
        /// </summary>
        [SerializeField] private float backgroundWidth = 35f;
        
        /// <summary>
        /// The current game scroll speed, derived from the GameManager's game speed.
        /// </summary>
        private float _backgroundScrollSpeed;

        private void Start()
        {
            backgroundWidth = backgroundSprites[0].GetComponent<SpriteRenderer>().bounds.size.x;
            _backgroundScrollSpeed = GameManager.Instance.gameSpeed / backgroundScrollSpeedRatio;;
        }

        private void Update()
        {
            foreach (var sprite in backgroundSprites)
            {
                sprite.transform.position += scrollDirection * (_backgroundScrollSpeed * Time.deltaTime);
                if (sprite.transform.position.x <= -backgroundWidth)
                {
                    sprite.transform.position += Vector3.right * (2 * backgroundWidth);
                }
            }
        }
    }
}