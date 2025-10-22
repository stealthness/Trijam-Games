using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerSkin : MonoBehaviour
    {
        public Sprite[] playerSkins;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private int selectedSkinIndex = 0;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void Start()
        {
            ApplySelectedSkin(selectedSkinIndex);
        }

        private void ApplySelectedSkin(int skinIndex)
        {
            if (skinIndex >= 0 && skinIndex < playerSkins.Length)
            {
                _spriteRenderer.sprite = playerSkins[skinIndex];
            }
            else
            {
                Debug.LogWarning("Selected skin index is out of range.");
            }
        }
    }
}