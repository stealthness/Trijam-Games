using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class Box : MonoBehaviour
    {
        private Image _boxContentImage;
        private Image _selectedBoxImage;
        
        BoxItem boxItem;
        public Image boxContentsImage;

        private void Awake()
        {

        }

        public void Initialize(BoxItem item, Image boxContentsImage)
        {
            boxItem = item;
            this.boxContentsImage = boxContentsImage;
            var images = GetComponentsInChildren<Image>();
            images[0] = boxContentsImage;
            
        }
        
        public bool IsSameAs(Box otherBox)
        {
            return boxItem.isEqualTo(otherBox.boxItem);
        }
        
        public void OnShowBox()
        {
            // Logic to show box
            if (boxItem == null)
            {
                Debug.LogError("BoxItem is not initialized.");
                return;
            }
            
            var images = GetComponentsInChildren<Image>();
            images[1].enabled = false;
            
            Debug.Log("Showing box with item: " + boxItem.GetJamsType());
        }
    }
}