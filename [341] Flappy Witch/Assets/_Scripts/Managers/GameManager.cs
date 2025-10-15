using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public float gameSpeed = 2f;

        public static GameManager Instance;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Start()
        {
            Debug.Log("GameManager Start");
        }


        public void GameOver()
        {
            Debug.Log("Game Over!");
            StartCoroutine(DelayedMenuLoad());
        }

        private IEnumerator DelayedMenuLoad()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("MenuScene");
        }


        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
