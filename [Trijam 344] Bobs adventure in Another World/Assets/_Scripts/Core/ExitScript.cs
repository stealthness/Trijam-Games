using UnityEngine;

namespace _Scripts
{
    public class ExitScript : MonoBehaviour
    {
        public GameObject PreviousLevel;
        public GameObject NextLevel;
        public GameObject SpawnPoint;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                
                if (NextLevel != null)
                {
                    other.transform.position = SpawnPoint.transform.position;
                    NextLevel.SetActive(true);
                }

                if (PreviousLevel != null)
                {
                    PreviousLevel.SetActive(false);
                }
            }
        }
    }
}