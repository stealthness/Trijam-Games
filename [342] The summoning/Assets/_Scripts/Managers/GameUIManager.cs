using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameUIManager : MonoBehaviour
    {
        public static GameUIManager Instance;
        
        public TextMeshProUGUI PlayerHealthText;
        public TextMeshProUGUI MonksHealthText;
        
        
        private void Awake()
        {
            if(!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void UpdatePlayerHealth(int health)
        {
            PlayerHealthText.text = $"{health}";
        }
        
        public void UpdateMonksHealth(int health)
        {
            MonksHealthText.text = $"{health}";
        }
    }
}