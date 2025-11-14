using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class BoxButtonManager : MonoBehaviour
    {
        public static BoxButtonManager Instance;
        public GameObject boxButtonTextPrefab;
        [SerializeField] private int maxRowSize = 4;
        [SerializeField] private int maxColSize = 4;
        [SerializeField] private Vector2 offSet = new Vector2(-180, 180);
        public Sprite[] itemImages;
        public Image strawberryImage;
        
        private BoxItem[] boxItems;
        private JamsType[] jams = new JamsType[]{
            
            JamsType.Empty, JamsType.Empty,
            JamsType.Strawberry, JamsType.Strawberry,
            JamsType.Blackberry, JamsType.Blackberry,
            JamsType.Eyeballs, JamsType.Eyeballs, 
            JamsType.Blueberry, JamsType.Blueberry,
            JamsType.Slime, JamsType.Slime,
            JamsType.Pineapple, JamsType.Pineapple,
            JamsType.Broken,JamsType.Broken,
        };

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }


        private void Start()
        {
            CreateBoxArray();
        }

        private void CreateBoxArray()
        {
            Debug.Log("Creating Box Array");
            boxItems = new BoxItem[maxRowSize * maxColSize];
            for (int i = 0; i < boxItems.Length; i = i + 2)
            {
                Debug.Log("Creating Box Item for: " + jams[i]);
                boxItems[i] = new BoxItem(jams[i]);
                boxItems[i + 1] = new BoxItem(jams[i]);
            }
        }

        public void CreateBoxes()
        {
            Debug.Log("Boxes Created");
            // Add logic to create boxes
            
            for (int row = 0; row < maxRowSize; row++)
            {
                for (int col = 0; col < maxColSize; col++)
                {
                    int boxId = row * maxColSize + col;
                    Vector2 anchorPoint = new Vector2(col * 120, -row * 120) + offSet;
                    CreateBoxButton(boxId, anchorPoint);
                }
            }

        }

        private void CreateBoxButton(int boxId, Vector2 anchorPoint)
        {
            var boxText = Instantiate(boxButtonTextPrefab, transform);
            boxText.name = "BoxButton_" + boxText;
            boxText.AddComponent<Box>().Initialize(boxItems[boxId], GetImageForJamsType(boxItems[boxId].GetJamsType()));
            
            // set images
            var images = boxText.GetComponentsInChildren<Image>();
            images[0].sprite = GetImageForJamsType(boxItems[boxId].GetJamsType()).sprite;
            
            
            // Set button listener
            var btn = boxText.GetComponent<Button>();
            btn.onClick.AddListener(() => OnBoxButtonClicked(btn, boxId));
            
            // Set position
            var rect1 = boxText.GetComponent<RectTransform>();
            rect1.anchoredPosition = anchorPoint;
        }

        private Image GetImageForJamsType(JamsType jam)
        {

            int index = (int)jam;
            var imgObj = new GameObject(jam.ToString());
            var img = imgObj.AddComponent<Image>();
            img.sprite = itemImages[index];
            return img;
  
        }

        private void OnBoxButtonClicked(Button btn, int boxId)
        {
            Debug.Log("Box Button Clicked: " + boxId);
            btn.GetComponent<Box>().OnShowBox();
        }
    }
}