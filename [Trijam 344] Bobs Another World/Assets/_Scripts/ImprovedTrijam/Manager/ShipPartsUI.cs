using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ImprovedTrijam.Manager
{
    public class ShipPartsUI : MonoBehaviour
    {
        
        public static ShipPartsUI Instance;
        
        public GameObject[] shipParts;
        [SerializeField] private int shipPartsFound = 0;


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
            shipPartsFound = 0;
            foreach (var shipPart in shipParts)
            {
                    shipPart.GetComponent<Image>().color = Color.gray2;
                
            }
        }


        public void ShipPartsFound()
        {
            if (shipPartsFound >= shipParts.Length)
            {
                Debug.Log("All ship parts found");
                return;
            }
            
            shipParts[shipPartsFound].GetComponent<Image>().color = Color.white;
            shipPartsFound++;
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (shipPartsFound < shipParts.Length) return;
            
            GameManager.Instance.GameWon();
        }
    }
}