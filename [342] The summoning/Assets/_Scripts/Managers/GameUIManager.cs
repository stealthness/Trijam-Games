using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameUIManager : MonoBehaviour
    {
        public TextMeshProUGUI PlayerHealthText;
        public TextMeshProUGUI MonksHealthText;
        
        
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