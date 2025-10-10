using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class MenuScript : MonoBehaviour
    {
        public void OnStartGameClick()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}