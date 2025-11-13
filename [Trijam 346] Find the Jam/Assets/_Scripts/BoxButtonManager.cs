using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class BoxButtonManager : MonoBehaviour
    {
        public static BoxButtonManager Instance;
        public GameObject boxButtonTextPrefab;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }
        
        
        public void CreateBoxes()
        {
            Debug.Log("Boxes Created");
            // Add logic to create boxes
            
            
            
            CreateBoxButton(1, Vector2.zero);
            
            
            CreateBoxButton(2, new Vector2(120, 120));
            
            


        }

        private void CreateBoxButton(int boxId, Vector2 anchorPoint)
        {
            var boxText = Instantiate(boxButtonTextPrefab, transform);
            boxText.name = "BoxButton_" + boxText;
            var btn = boxText.GetComponent<Button>();
            btn.onClick.AddListener(() => OnBoxButtonClicked(btn, boxId));
            var rect1 = boxText.GetComponent<RectTransform>();
            rect1.anchoredPosition = anchorPoint;
        }

        private void OnBoxButtonClicked(Button btn, int boxId)
        {
            // Logic when a box button is clicked
            Debug.Log("Box Button Clicked: " + boxId);
        }
    }
}