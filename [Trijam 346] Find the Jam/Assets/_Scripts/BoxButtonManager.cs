using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class BoxButtonManager : MonoBehaviour
    {
        public static BoxButtonManager Instance;
        public GameObject boxButtonTextPrefab;
        [SerializeField] private int maxRowSize = 4;
        [SerializeField] private int maxColSize = 4;
        [SerializeField] private Vector2 offSet = new Vector2(-180, 180);
        [SerializeField] private int boxNumbers;
        public Sprite[] itemImages;
        public Sprite[] boxImages;
        
        
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
            RandomizeBoxes();
            Debug.Log("Creating Box Array");
            boxItems = new BoxItem[maxRowSize * maxColSize];
            
            for(int i = 0; i < boxItems.Length; i++)
            {
                Debug.Log("Creating Box Item for: " + jams[i]);
                boxItems[i] = new BoxItem(jams[i]);
            }
            boxNumbers = maxRowSize * maxColSize;
           
            
        }

        private void RandomizeBoxes()
        {
            for (int i = 0; i < jams.Length; i++)
            {
                // randomize jams array
                var temp = jams[i];
                var r = Random.Range(0, jams.Length);
                jams[i] = jams[r];
                jams[r] = temp;
                Debug.Log("Jam at index " + i + ": " + jams[i]);
            }
        }

        public void CreateBoxes()
        {
            boxNumbers = maxRowSize * maxColSize;
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
            boxText.name = "BoxButton_" + boxItems[boxId].GetJamsType() + "_" + ((boxId<10)?("0" + boxId):boxId);
            boxText.AddComponent<Box>().Initialize(boxItems[boxId], GetImageForJamsType(boxItems[boxId].GetJamsType()));
            
            // set images
            var images = boxText.GetComponentsInChildren<Image>();
            images[0].sprite = GetImageForJamsType(boxItems[boxId].GetJamsType()).sprite;
            
            images[1].sprite = boxImages[Random.Range(0,boxImages.Length)]; // box closed image
            
            
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
            if (GameManager.Instance.isPaused)
            {
                return;
            }
            
            Debug.Log("Box Button Clicked: " + boxId);
            var box = btn.GetComponent<Box>();
            box.OnShowBox();
            ScoreManager.Instance.AddOnePoint();
            GameManager.Instance.BoxOpened(boxId, box.name);
        }

        
        public void HideAllBoxes()
        {
            Debug.Log("Hiding All Boxes");
            foreach (Transform child in transform)
            {
                var box = child.GetComponent<Box>();
                if (box != null)
                {
                    var images = box.GetComponentsInChildren<Image>();
                    images[1].enabled = true;
                }
            }
        }
        
        public bool AreAllBoxesRemoved()
        {
            Debug.Log("AreAllBoxesRemoved Called. Remaining Boxes: " + boxNumbers);
            return boxNumbers <= 0;
        }
        
        public void RemoveBoxes(string boxName1, string boxName2)
        {
            Debug.Log("Removing Boxes: " + boxName1 + " and " + boxName2);
            var box1 = transform.Find(boxName1);
            var box2 = transform.Find(boxName2);
            if (box1 != null)
            {
                Destroy(box1.gameObject);
            }
            if (box2 != null)
            {
                Destroy(box2.gameObject);
            }
            boxNumbers -=2;
        }
    }
}