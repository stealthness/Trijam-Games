using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(fileName = "PersonCard", menuName = "Person Card", order = 0)]
    public class PersonCardSO : ScriptableObject
    {
        public Sprite img;
        public string name;
        public bool isChosenOne;
        
    }
}