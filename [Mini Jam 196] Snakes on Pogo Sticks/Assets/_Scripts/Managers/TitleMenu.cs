using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class TitleMenu : MonoBehaviour
    {
        public void OnStartGAmeButton()
        {
            SceneManager.LoadScene("TurnGameScene");
        }
    }
}