using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public void OnMove(Vector3 direction)
        {
            transform.Translate(direction * Time.deltaTime * 5f);
        }
    }
}