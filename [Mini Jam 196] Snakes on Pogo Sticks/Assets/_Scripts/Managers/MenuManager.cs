using UnityEngine;

namespace _Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
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

        public void ShowMainMenu()
        {
            Debug.Log("Main Menu Shown");
        }
    }
}